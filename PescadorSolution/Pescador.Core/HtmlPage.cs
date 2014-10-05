using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;
using Pescador.Core.Interfaces;
using Pescador.Support.Exceptions;


namespace Pescador.Core
{
    /// <summary/>
    /// Clase Base HTML
    // </summary>
    public abstract class HtmlPage : IHtmlPage
    {
        /// <summary>
        /// Contenedor de Cookies
        /// </summary>
        internal CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Información utilizado para el manejo de errores
        /// </summary>
        public List<LogInfo> Trace { get; set; }

        /// <summary>
        /// Ubicación del .exe ejecutable
        /// </summary>
        public string RuntimePath { get; set; }

        /// <summary>
        /// Logear información importante ante cualquier error
        /// </summary>
        /// <param name="title">Nombre del Mensaje a guardar</param>
        /// <param name="message">Mensaje a guardar</param>
        public void TraceInfo(string title, string message)
        {
            var l = new LogInfo()
                        {
                            Title = title,
                            Value = message
                        };
            this.Trace.Add(l);

        }

        /// <summary>
        /// Timeout en milisegundos, utilizado para las operaciones de Request y Response
        /// </summary>
        public int Timeout { get; set; }
        
        /// <summary>
        /// Url Base del sistema Peldar
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Obtener el Value de un elemento tipo Hidden Input
        /// </summary>
        /// <param name="elementId">Id del Elemento HTML</param>
        /// <param name="fullHtml">Contenido HTML sobre el cual se buscará</param>
        /// <returns>Retorna el Value del Input</returns>
        protected string GetHiddenInput(string elementId, string fullHtml)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(fullHtml);

                var elementToFind = string.Format("//*[contains(@id, '{0}')]", elementId);
                var hINputHidden = htmlDocument.DocumentNode.SelectSingleNode(elementToFind);

                //Hacemos una verificación, si falla debemos generar una Excepción custom
                if (hINputHidden == null)
                    throw new ParserException(11, fullHtml, "Elemento No encontrado: " + elementToFind);

                //Hacemos una verificación, si falla debemos generar una Excepción custom
                if (hINputHidden.Attributes["value"] == null)
                    throw new ParserException(12, fullHtml, "Atributo Value no encontrado: " + elementToFind);

                return hINputHidden.Attributes["value"].Value;
            }
            catch (Exception ex)
            {
                //Si encontramos un error, generamos una nueva excepción Custom
                throw new ParserException(10, fullHtml, "Error no controlado al intentar de buscar el ElementID: " + elementId, ex);
            }
        }

        /// <summary>
        /// Obtener la lista de Ofertas de Viajes a partir de un elemento HTML
        /// </summary>
        /// <param name="fullHtml">Contenido HTML sobre el cual se buscará</param>
        /// <returns>Lista de Ofertas de Viajes</returns>
        protected OffersCollection GetOffersTable(string fullHtml)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(fullHtml);

                if (fullHtml.Contains("No se encuentra camiones")) //Esto es porque NO hay oferta de camiones.
                    return new OffersCollection();

                var hTable = htmlDocument.DocumentNode.SelectSingleNode("//*[contains(@id, 'Fcamion')]");

                if (hTable == null)
                    throw new ParserException(21, fullHtml, "Tabla de Camiones no encontrada");

                var hTrCollection = hTable.SelectNodes("tr");
                if (hTrCollection == null)
                    throw new ParserException(22, fullHtml, "Filas de Camiones no encontrada");

                //Eliminamos el primer TR que corresponde al header
                if (hTrCollection.Count > 0)
                    hTrCollection.Remove(0);

                //Eliminamos el segundo TR que corresponde al header
                if (hTrCollection.Count > 0)
                    hTrCollection.Remove(0);

                //Eliminamos el último TR que corresponde al paginador
                if (hTrCollection.Count > 0)
                    hTrCollection.Remove(hTrCollection.Count - 1);

                var offerPorterings = new OffersCollection
                {
                    Deals = new List<OfferPortering>()
                };

                foreach (var hTr in hTrCollection)
                {
                    try
                    {
                        var hTdCollection = hTr.SelectNodes("td");

                        var of = new OfferPortering
                                     {
                                         Id = hTdCollection[0].InnerText.Trim(),
                                         ElementId = hTdCollection[0].SelectSingleNode(".//span").Attributes["id"].Value,
                                         CityDestination = hTdCollection[4].InnerText.Trim(),
                                         ReservationButtonID =
                                             hTdCollection[7].SelectSingleNode(".//input").Attributes["name"].Value
                                     };
                        offerPorterings.Deals.Add(of);
                    }
                    catch (Exception)
                    {
                    }
                }

                return offerPorterings;
            }
            catch (Exception ex)
            {   
                //Si encontramos un error, generamos una nueva excepción Custom
                throw new ParserException(20, fullHtml, "Error no controlado al intentar de parsear la tabla de camiones.", ex);
            }
        }

        /// <summary>
        /// Realiza un HTTP POST a una página y devuelve
        /// </summary>
        /// <param name="cookieContainer">Contenedor Cookie</param>
        /// <param name="relativeUrl">Url Relativa de la página a la cual se realizará el POST</param>
        /// <param name="hiddenValues">Valores a incluir en el POST</param>
        /// <returns>Retorna el HTML resultado del POST</returns>
        protected string HttpPost(string relativeUrl,  DictionaryExt<string, string> hiddenValues)
        {
            return HttpPost(relativeUrl, hiddenValues, "");
        }

        /// <summary>
        /// Realiza un HTTP POST a una página y devuelve el Body del HTML
        /// </summary>
        /// <param name="relativeUrl">Url Relativa de la página a la cual se realizará el POST</param>
        /// <param name="hiddenValues">Valores a incluir en el POST</param>
        /// <param name="urlReferrer">Url de la página que estaría haciendo el POST</param>
        /// <returns>Retorna el HTML resultado del POST</returns>
        protected string HttpPost(string relativeUrl, DictionaryExt<string, string> hiddenValues, string urlReferrer)
        {
            try
            {
                if (relativeUrl.StartsWith("/"))
                    relativeUrl = relativeUrl.Substring(1);

                var request = (HttpWebRequest)WebRequest.Create(this.BaseUrl + relativeUrl);
                ConfigureRequest(request, HttpVerbEnum.Post);
                if (urlReferrer != string.Empty)
                    request.Referer = urlReferrer;

                //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
                var bytesToSend = Encoding.UTF8.GetBytes(hiddenValues.ExtractParameters());
                request.ContentLength = bytesToSend.Length;

                string htmlContent;
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytesToSend, 0, bytesToSend.Length); //Push it out there

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        //Antes de hacer cualquier cosa, chequeamos si tenemos una cookie nueva.
                        ValidateCookie(response);

                        if (response.StatusCode == HttpStatusCode.Redirect)
                        {
                            //Vamos a hacer un HTTP GET a la nueva ubicación.
                            //La nueva ubicación la sacamos del Header.
                            string redirectTo = response.Headers["Location"];
                            //response.Close();
                            //requestStream.Close();

                            return this.HttpGet(redirectTo);
                        }
                        // ReSharper disable AssignNullToNotNullAttribute
                        var responseReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                        // ReSharper restore AssignNullToNotNullAttribute
                        htmlContent = responseReader.ReadToEnd();
                    }
                }
                return htmlContent;
            }
            catch (Exception ex)
            {
                //Si encontramos un error, generamos una nueva excepción Custom
                var newEx = new RequestException(30, "Error no controlado al intentar de realizar un HttpPost.", ex)
                {
                    PostData = hiddenValues.ExtractParameters(),
                    Url = this.BaseUrl + relativeUrl,
                    CookieContainer = this.CookieContainer
                };
                throw newEx;
            }
        }

        /// <summary>
        /// Realiza un HTTP GET a una página y devuelve el Body del HTML
        /// </summary>
        /// <param name="url">Url Relativa o Absulta de la página a la cual se realizará el GET</param>
        /// <returns>Retorna el HTML resultado del GET</returns>
        protected string HttpGet(string url)
        {
            //Verificamos si estamos usando una URL Relativa o Absulta
            if (url.StartsWith("/"))
                url = url.Substring(1);

            if (!url.Contains("http"))
                url = this.BaseUrl + url;

            var firstRequest = (HttpWebRequest)WebRequest.Create(url);
            ConfigureRequest(firstRequest, HttpVerbEnum.Get);

            using (var firstResponse = firstRequest.GetResponse())
            {
                using (var responseStream = firstResponse.GetResponseStream())
                {
                    if (responseStream == null)
                        throw new Exception("No se pudo obtener respuesta del servidor");

                    using (var firstResponseStream = new StreamReader(responseStream))
                    {
                        return firstResponseStream.ReadToEnd().Trim();
                    }
                }
            }
        }

        /// <summary>
        /// Configura todas la propiedades de un WebRequest
        /// </summary>
        /// <param name="request">instancia de un objeto WebRequest</param>
        /// <param name="httpVerb">Verbo a utilizar</param>
        private void ConfigureRequest(HttpWebRequest request, HttpVerbEnum httpVerb)
        {
            request.ContentType = "application/x-www-form-urlencoded";
            //request.AllowAutoRedirect = true;
            switch (httpVerb)
            {
                case HttpVerbEnum.Post:
                    request.Method = "POST";
                    break;
                case HttpVerbEnum.Get:
                    request.Method = "GET";
                    
                    break;
            }
            
            request.KeepAlive = true;
            
            request.ServicePoint.Expect100Continue = false;
            //request.Host = "transporte.peldar.com:8080";
            request.Timeout = this.Timeout;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Expect = "";

            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            if (request.Headers[HttpRequestHeader.Expect] != null)
                request.Headers.Remove(HttpRequestHeader.Expect);


            var sp = request.ServicePoint;
            var prop = sp.GetType().GetProperty("HttpBehaviour", BindingFlags.Instance | BindingFlags.NonPublic);
            prop.SetValue(sp, (byte)0, null);


            if (this.CookieContainer != null)
            {
                request.AllowAutoRedirect = true;
                request.CookieContainer = this.CookieContainer;
            }
            else
                request.AllowAutoRedirect = false;
        }
        
        /// <summary>
        /// Validar si tenemos que agregar la cookie al Contenedor o no.
        /// </summary>
        /// <param name="response"></param>
        private void ValidateCookie(HttpWebResponse response)
        {
            //Crear Cookie
            if (response.Headers["Set-Cookie"] != null)
            {
                var cookieData = response.Headers["Set-Cookie"];
                var cookieName = cookieData.Split(';')[0].Split('=')[0];
                var cookieValue = cookieData.Split(';')[0].Split('=')[1];
                //base.TraceInfo("Creación de Cookie", string.Format("cookieData={0},cookieName={1},cookieValue={2}", cookieData, cookieName, cookieValue));
                var authenticationCookie = new Cookie(cookieName, cookieValue, "/")
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(5),
                    Domain = "transporte.peldar.com"
                };
                this.CookieContainer = new CookieContainer();
                this.CookieContainer.Add(authenticationCookie);
            }
        }

        /// <summary>
        /// Enumeración de los tipos de Verbos HTTP que se pueden utilizar
        /// </summary>
        public enum HttpVerbEnum
        {
            Post,
            Get,
        }
            
    }
}

