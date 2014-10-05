using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using Pescador.Core.Interfaces;
using Pescador.Support.Exceptions;
using Pescador.Core.Database;
using System.Globalization;

namespace Pescador.Core
{
    /// <summary>
    /// Implementación de páginas de Peldar
    /// </summary>
    public class PeldarServicePage : HtmlPage, IPeldarPage
    {
        /// <summary>
        /// Contenido HTML de la última página de Ofertas de camiones
        /// </summary>
        protected string LastOffersPageContent { get; set; }

        /// <summary>
        /// Enumeración de los diferentes estados de conexión
        /// </summary>
        public enum EConnectionStatus
        {
            OnLine,
            Offline,
            Error
        }
        
        /// <summary>
        /// Delegado a utilizar para disparar eventos
        /// </summary>
        /// <param name="connStatus">Estado de la conexión</param>
        /// <param name="message">Mensaje a informar</param>
        /// <param name="subMessage">Sub mensaje a informar</param>
        public delegate void NotificationDelegate(EConnectionStatus connStatus, string message, string subMessage);

        /// <summary>
        /// Evento para disparar notificaciones de cambios de estados de conexión
        /// </summary>
        public event NotificationDelegate OnStatusChanged;

        /// <summary>
        /// Obtener la Url de la página de Login.
        /// </summary>
        private string LoginPageUrl {
            get { return this.BaseUrl + "default.aspx"; }
        }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="baseUrl">Url base del sitio</param>
        public PeldarServicePage(string baseUrl)
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl + "/";
            
            base.BaseUrl = baseUrl;

            //Por defecto asignamos 10 segundos de Timeout
            base.Timeout = 10000;
            this.Trace = new List<LogInfo>();
        }

        /// <summary>
        /// Autenticarse
        /// </summary>
        /// <param name="loginUserName">Nombre de Usuario para Autenticación</param>
        /// <param name="loginPassword">Contraseña de usuario para Autenticación</param>
        /// <returns>Retorna La Cookie de autenticación (si se autenticó correctamente), sinó Null</returns>
        public bool Authenticate(string loginUserName, string loginPassword)
        {
            try
            {
                #region Obtener el Html de la página de Login (usuario anonimo)
                var anonymousHtmlLoginPage = GetLoginHtmlPage(loginUserName, loginPassword); 
                #endregion

                #region Realizamos el Post sobre la página de Login para autenticarse
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

                
                base.TraceInfo("Post Página Login - Parámetros", postArguments.ExtractParameters());
                string htmlResponse = base.HttpPost("default.aspx", postArguments, "http://transporte.peldar.com:8080/default.aspx");
                base.TraceInfo("Post Página Login - Resultado", htmlResponse);

                if (htmlResponse.Contains("Acceso Denegado"))
                    throw new SecurityException(47, "Nombre o Usuario inválido") { TraceContent = htmlResponse };
                if (!htmlResponse.Contains("REP_camion.aspx"))
                    throw new SecurityException(48, "No se pudo autenticar") { TraceContent = htmlResponse };

                #endregion
                
                return true;
            }
            catch (Exception ex)
            {
                //Si encontramos un error, generamos una nueva excepción Custom
                var newEx = new SecurityException(40,
                                                  "Error no controlado al intentar Autenticare con el usuario: " +
                                                  loginUserName, ex);
                throw newEx;
            }
        }

        /// <summary>
        /// Obtener el Html de la pagina de login para los usuarios anonónimos (no autenticados) 
        /// </summary>
        /// <param name="loginUserName"></param>
        /// <param name="loginPassword"></param>
        /// <returns>Html de la página</returns>
        protected string GetLoginHtmlPage(string loginUserName, string loginPassword)
        {
            if (string.IsNullOrEmpty(loginUserName))
                throw new SecurityException(41, "Para autenticarse debe indicar el UserName primero.");

            if (loginPassword == string.Empty)
                throw new SecurityException(42, "Para autenticarse debe indicar la Contraseña primero.");

            if (base.BaseUrl == string.Empty)
                throw new SecurityException(43, "Para autenticarse debe indicar el Dominio primero.");

            var anonymousHtmlLoginPage = GetAnonymousLoginPage();
            return anonymousHtmlLoginPage;
        }
        
        /// <summary>
        /// Obtener lista de Ofertas de Viajes
        /// </summary>
        /// <returns>Lista de Ofertas</returns>
        public OffersCollection GetOffers()
        {
            try
            {
                if (string.IsNullOrEmpty(this.LastOffersPageContent))
                {
                    //Si NO estamos autenticados....nos vamos.
                    if (base.CookieContainer == null)
                        throw new SecurityException(51, "El usuario No está autenticado");

                    if (base.BaseUrl == string.Empty)
                        throw new SecurityException(52, "Falta indicar la BaseUrl del Servicio");


                    #region Abrimos primero "paquete_admon/forma/REP_camion.aspx" que contiene la lista de Ofertas (Paginado)

                    //Abrimos primero paquete_admon/forma/REP_camion.aspx
                    const string relativeUrl = "paquete_admon/forma/REP_camion.aspx";

                    //Hacemos primero un HTTP GET
                    base.TraceInfo("Get Página Ofertas Base - Parámetros", relativeUrl);
                    this.LastOffersPageContent = base.HttpGet(relativeUrl);
                    base.TraceInfo("Get Página Ofertas Base - Resultado", this.LastOffersPageContent);

                    #endregion
                }
                return GetQuickOffers();
            }
            catch (Exception ex)
            {
                var newEx = new RequestException(50,
                                                 "Error no controlado al intentar de Obtener la lista de Camiones.", ex);
                throw newEx;
            }
        }

        /// <summary>
        /// Obtener una lista de Ofertas de Viajes a partir de un HTML prviamente renderizado
        /// </summary>
        /// <returns></returns>
        private OffersCollection GetQuickOffers()
        {
            var retVal = new OffersCollection()
                             {
                                 Deals = null,
                                 HtmlContent = this.LastOffersPageContent
                             };

            //Si NO estamos autenticados....nos vamos.
            if (base.CookieContainer == null)
                throw new SecurityException(51, "El usuario No está autenticado");

            if (base.BaseUrl == string.Empty)
                throw new SecurityException(52, "Falta indicar la BaseUrl del Servicio");

            #region Presionamos sobre el Link que dice "Ver Camiones Disponibles"
            //Presionamos sobre el Link que dice "Ver Camiones Disponibles"
            var postArguments = new DictionaryExt<string, string>
                                {
                                    {"__EVENTTARGET", "LinkDisponible"},
                                    {"__EVENTARGUMENT", ""},
                                    {"__VIEWSTATE", base.GetHiddenInput("__VIEWSTATE", this.LastOffersPageContent)},
                                    {
                                        "__EVENTVALIDATION",
                                        base.GetHiddenInput("__EVENTVALIDATION", this.LastOffersPageContent)
                                    }
                                };

            base.TraceInfo("Post Link 'Ver Camiones Disponibles' - Parámetros", postArguments.ExtractParameters());
            const string relativeUrl = "paquete_admon/forma/REP_camion.aspx";
            var htmlContent = base.HttpPost(relativeUrl, postArguments);
            base.TraceInfo("Post Link 'Ver Camiones Disponibles' - Resultado", htmlContent);
            #endregion

            
            if (htmlContent.Contains("No se encuentra camiones")) //Esto es porque NO hay oferta de camiones.
                return retVal;


            #region Ahora que sabemos que aparece algún camión, entonces presionamos sobre el Link que dice "Ver Todos" para evitarnos el paginado
            //postArguments = new DictionaryExt<string, string>
            //                {
            //                    {"__EVENTTARGET", "LinkTodos"},
            //                    {"__EVENTARGUMENT", ""},
            //                    {"__VIEWSTATE", base.GetHiddenInput("__VIEWSTATE", htmlContent)},
            //                    {
            //                        "__EVENTVALIDATION",
            //                        base.GetHiddenInput("__EVENTVALIDATION", htmlContent)
            //                    }
            //                };

            //base.TraceInfo("Post Link 'Ver Todos' - Parámetros", postArguments.ExtractParameters());
            //htmlContent = base.HttpPost(relativeUrl, postArguments);
            //base.TraceInfo("Post Link 'Ver Todos' - Resultado", htmlContent);
            #endregion

            #region Obtenemos de manera parseada la lista de Ofertas de Viajes
            retVal = base.GetOffersTable(htmlContent);
            retVal.HtmlContent = htmlContent;
            #endregion
            
            return retVal;
        }

        /// <summary>
        /// Consultar si hay ofertas, si alguna de ellas concinde con la lista de camiones disponibles, entonces Reservar
        /// </summary>
        /// <returns>Resultado de la consulta y reserva</returns>
        public ReservationResult CheckAndBook()
        {

            var result = new ReservationResult();
            var offers = new OffersCollection();

            result.ReservationDetails = new List<ReservationDetail>();
            result.AvailableOffers = new List<OfferPortering>();
            NotificationCenter(EConnectionStatus.OnLine, "En Linea", "Consultando Peldar...");

            //Comprobaremos si podemos Aplicar a alguna de las ofertas publicadas
            using (var db = new PescadorDBEntities())
            {
                //Traemos una lista de Camiones pendientes de asignación
                var availableTrucks = db.Trucks.Where(a => a.Status == "S").OrderByDescending(a => a.ID);
               
                //Si hay camiones disponibles, entonces hacemos la consulta a Peldar para obtener las ofertas.....
                if (availableTrucks.Any())
                {
                    var logTrucks = new StringBuilder();
                    foreach (var c in availableTrucks)
                    {
                        string des = c.Destinations.Aggregate(string.Empty, (current, de) => current + ", " + de.Destination1);
                        logTrucks.AppendLine(
                            string.Format("Placa:{0}, Conductor:{1}, Documento:{2}, Celular:{3},Destinos:{4}", c.Plate,
                                          c.DriveName, c.DriverDocumentNumber, c.DriverMobilePhone, des));
                    }
                    base.TraceInfo("Listado de Camiones", logTrucks.ToString());
                    //return result;
                    offers = this.GetOffers();

                    if (offers.Deals != null)
                    {
                        result.AvailableOffers = offers.Deals;
                        //Seteamos el Status indicando que Tenemos Ofertas publicadas
                        NotificationCenter(EConnectionStatus.OnLine, "En Linea", string.Format("{0} ofertas encontradas, validando si podemos aplicar a alguna....",offers.Deals.Count));
                    }
                    else
                    {
                        NotificationCenter(EConnectionStatus.OnLine, "En Linea", "No hay ofertas para los destinos publicados");
                        return result;
                    }
                }
                else
                {
                    NotificationCenter(EConnectionStatus.OnLine, "En Linea", "No hay camiones disponibles!!!");
                    return result;
                }



                //Recorremos los Camiones Disponibles que tenemos
                foreach (var availableTruck in availableTrucks)
                {


                    //Recorremos todas las Ofertas
                    foreach (var offer in offers.Deals)
                    {
                        //Por cáda camión recorremos los posibles destinos
                        foreach (
                            var dest in
                                availableTruck.Destinations.Where(
                                    dest => string.Compare(dest.Destination1, offer.CityDestination, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0))
                        {
                            //Realizaremos la Reserva para éste camión...
                            var player = new SoundPlayer
                            {
                                SoundLocation = @"c:\alarm.wav"
                            };

                            player.Play();
                            //Generamos un Beep de Sistema.
                            SystemSounds.Exclamation.Play();

                            //Seteamos el Status indicando que Tenemos Ofertas publicadas
                            NotificationCenter(EConnectionStatus.OnLine, "En Linea",
                                string.Format("(Aplicando a una oferta para el camión {0} para la ciudad de {1})",
                                                availableTruck.Plate, offer.CityDestination));

                            //Si el resultado fue positivo de la reserva, entonces marcaremos en la BD al camión como Asignado
                            if (this.ReserveTruck(offers, offer, availableTruck))
                            {
                                availableTruck.DateAssigned = DateTime.Now;
                                availableTruck.DestinationAssignedID = dest.ID;
                                availableTruck.Status = "A"; //A : Asignado
                                availableTruck.AssignationID = offer.Id;
                                db.SaveChanges();

                                result.ReservationDetails.Add(new ReservationDetail()
                                                                  {
                                                                      Offer = offer,
                                                                      Truck = availableTruck
                                                                  });

                                NotificationCenter(EConnectionStatus.OnLine, "En Linea",
                                                   string.Format(
                                                       "(Reserva realizada para el camión {0} para la ciudad de {1})",
                                                       availableTruck.Plate, offer.CityDestination));

                                return result;
                            }
                            else
                            {
                                //NotificationCenter(EConnectionStatus.Error, "En Linea",
                                //                   string.Format(
                                //                       "(No se pudo reservar el camión {0} para la ciudad de {1})",
                                //                       availableTruck.Plate, offer.CityDestination));
                                return result;
                            }
                        }
                    }
                }
            }

            if (result.HaveReservations)
                NotificationCenter(EConnectionStatus.OnLine, "En Linea", string.Format("Se realizaron {0} reservas satisfactoriamente", result.ReservationDetails.Count));
            else
                NotificationCenter(EConnectionStatus.OnLine, "En Linea", "No hay ofertas para los destinos publicados");

            return result;
        }

        /// <summary>
        /// Reservar un camión para una oferta concreta
        /// </summary>
        /// <param name="offerList">Lista de Oferta</param>
        /// <param name="offer">Oferta a reservar</param>
        /// <param name="availableTruck">Camión disponible</param>
        /// <returns></returns>
        protected bool ReserveTruck(OffersCollection offerList, OfferPortering offer, Truck availableTruck)
        {
            //Recorremos los posibles destinos de éste camión
            foreach (var dest in availableTruck.Destinations)
            {

                //Si el destino coincide con el destino de la oferta....
                if (string.Compare(dest.Destination1, offer.CityDestination, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
                {
                    #region Click en el Boton de "Cambiar" y hacemos un POST para ingresar a cambiar el estado del camión
                    var reservationButtonIDEncoded = System.Uri.EscapeDataString(offer.ReservationButtonID);

                    var truckOfferPostArguments = new DictionaryExt<string, string>
                                                      {
                                                          {"__EVENTTARGET", ""},
                                                          {"__EVENTARGUMENT", ""},
                                                        {
                                                            "__VIEWSTATE",
                                                            base.GetHiddenInput("__VIEWSTATE", offerList.HtmlContent)
                                                        },
                                                        {
                                                            "__EVENTVALIDATION",
                                                            base.GetHiddenInput("__EVENTVALIDATION", offerList.HtmlContent)
                                                        },
                                                          {reservationButtonIDEncoded,"Cambiar"}
                                                      };
                    var relativeUrl = "paquete_admon/forma/REP_camion.aspx";



                    base.TraceInfo("Post Link 'Change' - Parámetros", truckOfferPostArguments.ExtractParameters());
                    string newhtmlContent = base.HttpPost(relativeUrl, truckOfferPostArguments, "http://transporte.peldar.com:8080/paquete_admon/forma/REP_camion.aspx");
                    base.TraceInfo("Post Link 'Change' - Resultado", newhtmlContent); 
                    #endregion

                    #region Click en el Boton de "Reservar Camión" y hacemos un POST para enviar todos los datos del camión y conductor y aplicar la REserva correspondiente
                    //Hacemos un POST para ingresar a cargar los datos de la reserva del camión (Reservar)
                    var truckReservationFormData = new DictionaryExt<string, string>
                                                       {
                                                           {"__EVENTTARGET", ""},
                                                           {"__EVENTARGUMENT", ""},
                                                           {
                                                               "__VIEWSTATE",
                                                               base.GetHiddenInput("__VIEWSTATE", newhtmlContent)
                                                           },
                                                           {
                                                               "__EVENTVALIDATION",
                                                               base.GetHiddenInput("__EVENTVALIDATION", newhtmlContent)
                                                           },
                                                           {"Ftransportadora", "136"},
                                                           {"Aplaca", availableTruck.Plate},
                                                           {"Aconductor", availableTruck.DriveName},
                                                           {"Fcedula", availableTruck.DriverDocumentNumber},
                                                           {"celular", availableTruck.DriverMobilePhone},
                                                           {"Acomentario", ""},
                                                           {"FButtonReservar", "Reservar Camión"}
                                                       };

                    base.TraceInfo("Post Reservation Form - Parémetros", truckReservationFormData.ExtractParameters());
                    relativeUrl = "paquete_admon/forma/cambiar_estado.aspx";
                    string finalhtmlContent = "";// base.HttpPost(relativeUrl, truckReservationFormData, "http://transporte.peldar.com:8080/paquete_admon/forma/cambiar_estado.aspx");
                    base.TraceInfo("Post Reservation Form - Resultado", finalhtmlContent);

                    if (finalhtmlContent.Contains("El límite de reservas en el día ha sido alcanzado"))
                    {
                        NotificationCenter(EConnectionStatus.Error, "No se puede reservar", "El límite de reservas en el día ha sido alcanzado");
                        return false;
                    }

                    #endregion

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Obtener el contenido HTML de la página de Login para un usuario Anónimo (No autenticado)
        /// </summary>
        /// <returns>Html de la página de login</returns>
        protected string GetAnonymousLoginPage()
        {
            try
            {
                base.TraceInfo("Get Página de Login Anónima - Parámetros", this.LoginPageUrl);
                var responseHtml = base.HttpGet(this.LoginPageUrl);
                base.TraceInfo("Get Página de Login Anónima - Resultado", responseHtml);
                return responseHtml;
            }
            catch (Exception ex)
            {
                //Si encontramos un error, generamos una nueva excepción Custom
                var newEx = new RequestException(60,
                                                 "Error no controlado al intentar de Obtener la página de login para usuarios anónimos.", ex);
                throw newEx;
            }
        }

        /// <summary>
        /// Notificar de un nuevo Evento
        /// </summary>
        /// <param name="connStatus">Estado de la conexión</param>
        /// <param name="message">Mensaje a informar</param>
        /// <param name="subMessage">Mensaje adicional</param>
        protected void NotificationCenter(EConnectionStatus connStatus, string message, string subMessage)
        {
            if (this.OnStatusChanged != null)
            {
                this.OnStatusChanged(connStatus, message, subMessage);
            }
        }

    }
}
