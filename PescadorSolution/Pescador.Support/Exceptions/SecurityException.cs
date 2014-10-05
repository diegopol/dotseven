using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pescador.Support.Exceptions
{
    public class SecurityException : PescadorException
    {
        /// <summary>
        /// Constructor Básico
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error que se registrará en los archivos de Log</param>
        public SecurityException(int errorCode, string errorMessage):base(errorCode, errorMessage)
        {}

        /// <summary>
        /// Constructor Avanzado
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="errorMessage">Mensaje de error que se registrará en los archivos de Log</param>
        /// <param name="innerException">Excepcion a anexar</param>
        public SecurityException(int errorCode, string errorMessage, Exception innerException)
            : base(errorCode, errorMessage, innerException)
        { }
    }
}
