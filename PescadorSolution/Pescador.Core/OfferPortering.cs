namespace Pescador.Core
{
    /// <summary>
    /// Representa una Oferta de Viaje
    /// </summary>
    public class OfferPortering
    {
        /// <summary>
        /// ID de viaje
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// ID de elemento HTML para realizar operaciones
        /// </summary>
        public string ElementId { get; set; }
        /// <summary>
        /// Ciudad de Destino
        /// </summary>
        public string CityDestination { get; set; }

        /// <summary>
        /// ID del boton para realizar la reservación
        /// </summary>
        public string ReservationButtonID { get; set; }
    }
}