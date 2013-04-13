using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;


namespace PeldarAutoAssignment.Core
{
    /// <summary>
    /// Clase Base HTML
    /// </summary>
    public abstract class HtmlPage : IHtmlPage
    {
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
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(fullHtml);

            var hINputHidden = htmlDocument.DocumentNode.SelectSingleNode(string.Format("//*[contains(@id, '{0}')]", elementId));
            if (hINputHidden == null)
                throw new HtmlWebException("Elemento No encontrado");

            return hINputHidden.Attributes["value"].Value;

        }

        /// <summary>
        /// Obtener la lista de Ofertas de Viajes a partir de un elemento HTML
        /// </summary>
        /// <param name="fullHtml">Contenido HTML sobre el cual se buscará</param>
        /// <returns>Lista de Ofertas de Viajes</returns>
        protected OffersCollection GetOffersTable(string fullHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(fullHtml);

            if (fullHtml.Contains("No se encuentra camiones")) //Esto es porque NO hay oferta de camiones.
                return new OffersCollection();

            var hTable = htmlDocument.DocumentNode.SelectSingleNode("//*[contains(@id, 'Fcamion')]");
            if (hTable == null)
                throw  new HtmlWebException("Tabla de Camiones no encontrada");

            var hTrCollection = hTable.SelectNodes("tr");
            if(hTrCollection == null)
                throw new HtmlWebException("Rows de Camiones no encontrada");
            

            //Eliminamos el TR que corresponde al header
            if(hTrCollection.Count > 0)
                hTrCollection.Remove(0);

            var offerPorterings = new OffersCollection
                                      {
                                          Deals = new List<OfferPortering>()
                                      };
            foreach (var hTr in hTrCollection)
            {
                var hTdCollection = hTr.SelectNodes("td");

                var of = new OfferPortering
                             {
                                 Id = hTdCollection[0].InnerText.Trim(),
                                 ElementId = hTdCollection[0].SelectSingleNode(".//span").Attributes["id"].Value,
                                 CityDestination = hTdCollection[4].InnerText.Trim()
                             };

                offerPorterings.Deals.Add(of);
            }

            return offerPorterings;
        }

        /// <summary>
        /// Realiza un HTTP POST a una página y devuelve
        /// </summary>
        /// <param name="cookieContainer">Contenedor Cookie</param>
        /// <param name="relativeUrl">Url Relativa de la página a la cual se realizará el POST</param>
        /// <param name="hiddenValues">Valores a incluir en el POST</param>
        /// <returns>Retorna el HTML resultado del POST</returns>
        protected string HttpPost(CookieContainer cookieContainer, string relativeUrl,  DictionaryExt<string, string> hiddenValues)
        {
            //var req3 = (HttpWebRequest)WebRequest.Create(this.BaseUrl + "paquete_admon/forma/REP_camion.aspx");
            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Substring(1);

            var request = (HttpWebRequest)WebRequest.Create(this.BaseUrl + relativeUrl);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.Timeout = this.Timeout;
            request.CookieContainer = cookieContainer;

            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            var bytesToSend = Encoding.UTF8.GetBytes(hiddenValues.ExtractParameters());
            request.ContentLength = bytesToSend.Length;

            string htmlContent;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytesToSend, 0, bytesToSend.Length); //Push it out there

                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    var responseReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    // ReSharper restore AssignNullToNotNullAttribute
                    htmlContent = responseReader.ReadToEnd();
                }
            }

            return htmlContent;
        }
    }
}
