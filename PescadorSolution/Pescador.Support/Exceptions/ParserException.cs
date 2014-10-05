using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pescador.Support.Exceptions
{
    /// <summary>
    /// Objeto del tipo base Excepcion encargado de manejar cualquier tipo de error de Parseo.
    /// </summary>
    public class ParserException : PescadorException
    {
        /// <summary>
        /// Constructor Básico
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="htmlContent">Contenido completo del HTML</param>
        /// <param name="errorMessage">Mensaje de error al intentar de parsear el HTML</param>
        public ParserException(int errorCode, string htmlContent, string errorMessage)
            : base(errorCode, errorMessage)
        {
            
        }

        /// <summary>
        /// Constructor Avanzado
        /// </summary>
        /// <param name="errorCode">Código interno de error</param>
        /// <param name="htmlContent">Contenido completo del HTML</param>
        /// <param name="errorMessage">Mensaje de error al intentar de parsear el HTML</param>
        /// <param name="innerException">Excepcion a anexar</param>
        public ParserException(int errorCode, string htmlContent, string errorMessage, Exception innerException)
            : base(errorCode, errorMessage, innerException)
        {

        }
    }
}
