using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Pescador.Support.Exceptions
{
    public class RequestException : PescadorException
    {
        /// <summary>
        /// Constructor Predeterminado
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error que se registrará en los archivos de Log</param>
        /// <param name="innerException">Excepcion a anexar</param>
        public RequestException(int errorCode, string errorMessage, Exception innerException)
            : base(errorCode, errorMessage, innerException)
        {
            //Inicializamos el Contenedor de Cookies
            this.CookieContainer = new CookieContainer();
        }
        /// <summary>
        /// Información del Post (parámetros)
        /// </summary>
        public string PostData { get; set; }
        /// <summary>
        /// Url del servicio
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Contenedor de Cookies
        /// </summary>
        public CookieContainer CookieContainer { get; set; }
    }
}
