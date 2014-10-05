using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pescador.Support.Exceptions
{
    public class ConnectionException : PescadorException
    {
        /// <summary>
        /// Constructor básico
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error que se registrará en los archivos de Log</param>
        public ConnectionException(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
            
        }
        /// <summary>
        /// Constructor avanzado
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error que se registrará en los archivos de Log</param>
        /// <param name="innerException">Excepcion a anexar</param>
        public ConnectionException(int errorCode, string errorMessage, Exception innerException)
            : base(errorCode, errorMessage, innerException)
        {

        }
    }
}
