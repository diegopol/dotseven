using System.Net;
using Pescador.Core.Database;

namespace Pescador.Core.Interfaces
{
    public interface IPeldarPage : IHtmlPage
    {
        /// <summary>
        /// Autenticarse
        /// </summary>
        /// <param name="loginUserName">Nombre de Usuario para Autenticación</param>
        /// <param name="loginPassword">Contraseña de usuario para Autenticación</param>
        /// <returns>Retorna True si se autenticó correctamente, sinó False</returns>
        bool Authenticate(string loginUserName, string loginPassword);

        /// <summary>
        /// Obtener lista de Ofertas de Viajes
        /// </summary>
        /// <returns>Lista de Ofertas</returns>
        OffersCollection GetOffers();

        /// <summary>
        /// Consultar si hay ofertas, si alguna de ellas concinde con la lista de camiones disponibles, entonces Reservar
        /// </summary>
        /// <returns>Resultado de la consulta y reserva</returns>
        ReservationResult CheckAndBook();
    }
}