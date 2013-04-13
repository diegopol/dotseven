using System;
using System.IO;
using System.Net;
using System.Security;
using System.Text;

namespace PeldarAutoAssignment.Core
{
    /// <summary>
    /// Implementación de Páginas de Peldar
    /// </summary>
    public class PeldarServicePage : HtmlPage, IPeldarPage
    {
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="baseUrl">Url base del sitio</param>
        public PeldarServicePage(string baseUrl)
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl + "/";
            
            base.BaseUrl = baseUrl;
            base.Timeout = 10000;
        }

        /// <summary>
        /// Autenticarse
        /// </summary>
        /// <param name="loginUserName">Nombre de Usuario para Autenticación</param>
        /// <param name="loginPassword">Contraseña de usuario para Autenticación</param>
        /// <returns>Retorna La Cookie de autenticación (si se autenticó correctamente), sinó Null</returns>
        public Cookie Authenticate(string loginUserName, string loginPassword)
        {
            if(loginUserName == string.Empty)
                throw new ArgumentException("Debe indicar el UserName");

            if (loginPassword == string.Empty)
                throw new ArgumentException("Debe indicar la Password");

            if (base.BaseUrl == string.Empty)
                throw new ArgumentException("Falta indicar la BaseUrl del Servicio");

            var anonymousHtmlLoginPage = GetAnonymousLoginPage();

            var postArguments = new DictionaryExt<string, string>
                                   {
                                       {"__EVENTTARGET", ""},
                                       {"__EVENTARGUMENT", ""},
                                       {
                                           "__VIEWSTATE",
                                           base.GetHiddenInput("__VIEWSTATE", anonymousHtmlLoginPage)
                                       },
                                       {
                                           "__EVENTVALIDATION",
                                           base.GetHiddenInput("__EVENTVALIDATION", anonymousHtmlLoginPage)
                                       },
                                       {"usr", loginUserName},
                                       {"pass", loginPassword},
                                       {"Button2", "Entrar"}
                                   };

            var bytesToSend = Encoding.UTF8.GetBytes(postArguments.ExtractParameters());

            var requestPost = (HttpWebRequest)WebRequest.Create(this.BaseUrl + "default.aspx");
            requestPost.ContentType = "application/x-www-form-urlencoded";
            requestPost.Method = "POST";
            requestPost.KeepAlive = true;
            requestPost.Timeout = base.Timeout;
            requestPost.AllowAutoRedirect = false;
            requestPost.ContentLength = bytesToSend.Length;

            using (var requestStream = requestPost.GetRequestStream())
            {
                requestStream.Write(bytesToSend, 0, bytesToSend.Length); //Push it out there
            }

            Cookie authenticationCookie = null;
            using (var responsePost = (HttpWebResponse) requestPost.GetResponse())
            {
                StreamReader responseReader = null;
                string htmlResponse = string.Empty;
                using (var responseStream = responsePost.GetResponseStream())
                {
                    if(responseStream == null)
                        throw new Exception("No se pudo obtener respuesta del servidor");
                    responseReader = new StreamReader(responseStream, Encoding.UTF8);

                    htmlResponse = responseReader.ReadToEnd();
                }
                if (htmlResponse.Contains("Acceso Denegado"))
                    throw new SecurityException("Nombre o Usuario inválido");
                if (!htmlResponse.Contains("REP_camion.aspx"))
                    throw new SecurityException("No se pudo autenticar");


                //Crear Cookie
                if (responsePost.Headers["Set-Cookie"] != null)
                {
                    var cookieData = responsePost.Headers["Set-Cookie"];
                    var cookieName = cookieData.Split(';')[0].Split('=')[0];
                    var cookieValue = cookieData.Split(';')[0].Split('=')[1];
                    authenticationCookie = new Cookie(cookieName, cookieValue, "/")
                                               {
                                                   HttpOnly = true,
                                                   Expires = DateTime.Now.AddDays(1),
                                                   Domain = "transporte.peldar.com"
                                               };
                }
            }
            return authenticationCookie;
        }

        /// <summary>
        /// Obtener lista de Ofertas de Viajes
        /// </summary>
        /// <param name="authenticationCookie">Cookie obtenido luego de haber autenticado</param>
        /// <returns>Lista de Ofertas</returns>
        public OffersCollection GetOffers(Cookie authenticationCookie)
        {
            //Si NO estamos autenticados....nos vamos.
            if (authenticationCookie == null)
                throw new SecurityException("No está autenticado");

            if (base.BaseUrl == string.Empty)
                throw new ArgumentException("Falta indicar la BaseUrl del Servicio");

            //Crear el Contenedor de Cookies
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(authenticationCookie);

            //Abrimos primero paquete_admon/forma/REP_camion.aspx
            const string relativeUrl = "paquete_admon/forma/REP_camion.aspx";

            //Hacemos primero un HTTP GET
            var firstRequest = (HttpWebRequest)WebRequest.Create(this.BaseUrl + relativeUrl);
            firstRequest.CookieContainer = cookieContainer;
            firstRequest.Timeout = base.Timeout;

            string htmlContent;
            try
            {
                using (var firstResponse = firstRequest.GetResponse())
                {
                    using (var responseStream = firstResponse.GetResponseStream())
                    {
                        if (responseStream == null)
                            throw new Exception("No se pudo obtener respuesta del servidor");

                        using (var firstResponseStream = new StreamReader(responseStream))
                        {
                            htmlContent = firstResponseStream.ReadToEnd().Trim();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw new WebException("Error al intentar acceder a 'REP_camion.aspx'", ex.Status);
            }


            //Presionamos sobre el Link que dice "Ver Camiones Disponibles"
            var postArguments = new DictionaryExt<string, string>
                                {
                                    {"__EVENTTARGET", "LinkDisponible"},
                                    {"__EVENTARGUMENT", ""},
                                    {"__VIEWSTATE", base.GetHiddenInput("__VIEWSTATE", htmlContent)},
                                    {
                                        "__EVENTVALIDATION",
                                        base.GetHiddenInput("__EVENTVALIDATION", htmlContent)
                                    }
                                };
            htmlContent = base.HttpPost(cookieContainer, relativeUrl, postArguments);

            //Ahora presionamos sobre el Link que dice "Ver Todos"
            postArguments = new DictionaryExt<string, string>
                            {
                                {"__EVENTTARGET", "LinkTodos"},
                                {"__EVENTARGUMENT", ""},
                                {"__VIEWSTATE", base.GetHiddenInput("__VIEWSTATE", htmlContent)},
                                {
                                    "__EVENTVALIDATION",
                                    base.GetHiddenInput("__EVENTVALIDATION", htmlContent)
                                }
                            };

            htmlContent = base.HttpPost(cookieContainer, relativeUrl, postArguments);

            //Obtenemos de manera parseada la lista de Ofertas de Viajes
            return base.GetOffersTable(htmlContent);
      
        }

        /// <summary>
        /// Obtener el contenido HTML de la página de Login para un usuario Anónimo (No autenticado)
        /// </summary>
        /// <returns>Html de la página de login</returns>
        private string GetAnonymousLoginPage()
        {
            var requestGet = WebRequest.Create(this.BaseUrl + "default.aspx");
            requestGet.Timeout = base.Timeout;

            string responseHtml;
            using (var responseGet = requestGet.GetResponse())
            {
                try
                {
                    StreamReader sr1;
                    using (var responseStream = responseGet.GetResponseStream())
                    {
                        if (responseStream == null)
                            throw new Exception("No se pudo obtener respuesta del servidor");

                        sr1 = new StreamReader(responseStream);
                    
                        responseHtml = sr1.ReadToEnd().Trim();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("No hay conexión al sitio de Peldar", ex);
                }
            }

            return responseHtml;
        }
    }
}
