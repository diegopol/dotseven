using System;
using System.Collections.Generic;

namespace PeldarAutoAssignment.Core
{
    /// <summary>
    /// Diccionario Extendido
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TO"></typeparam>
    public class DictionaryExt<T, TO> : Dictionary<T, TO>
    {
        /// <summary>
        /// Extraer la lista de valores del diccionario en formato contatenado (key=valor&key=valor...)
        /// </summary>
        /// <returns>Lista de valores contactenados</returns>
        public string ExtractParameters()
        {
            var p = string.Empty;
            foreach (var k in base.Keys)
            {
                if (!p.EndsWith("&") && p != string.Empty)
                    p = p + "&";

                var valor = base[k].ToString();
                p = p + k + "=" + Uri.EscapeDataString(valor);
            }

            return p;

        }
    }
}