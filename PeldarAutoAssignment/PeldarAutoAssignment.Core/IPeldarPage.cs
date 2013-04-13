using System.Net;

namespace PeldarAutoAssignment.Core
{
    public interface IPeldarPage : IHtmlPage
    {
        /// <summary>
        /// Autenticarse
        /// </summary>
        /// <param name="loginUserName">Nombre de Usuario para Autenticación</param>
        /// <param name="loginPassword">Contraseña de usuario para Autenticación</param>
        /// <returns>Retorna La Cookie de autenticación (si se autenticó correctamente), sinó Null</returns>
        Cookie Authenticate(string loginUserName, string loginPassword);

        /// <summary>
        /// Obtener lista de Ofertas de Viajes
        /// </summary>
        /// <param name="authenticationCookie">Cookie obtenido luego de haber autenticado</param>
        /// <returns>Lista de Ofertas</returns>
        OffersCollection GetOffers(Cookie authenticationCookie);
    }
}