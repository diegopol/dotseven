using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pescador.Support.Exceptions
{
    public abstract class PescadorException : Exception
    {
        /// <summary>
        /// Código interno de Error
        /// </summary>
        public int ErrorCode { get; private set; }

        protected PescadorException()
        {
            
        }
        /// <summary>
        /// Construtor basico
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error personalizado</param>
        protected PescadorException(int errorCode, string errorMessage) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Constructor Avanzado
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error personalizado</param>
        /// <param name="innerException">Excepcion a anexar</param>
        protected PescadorException(int errorCode, string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Contenido completo de la pagina HTML
        /// </summary>
        public string TraceContent { get; set; }
    }
}
