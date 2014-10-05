using Pescador.Core.Database;

namespace Pescador.Core
{
    /// <summary>
    /// Detalle de una reservación
    /// </summary>
    public class ReservationDetail
    {
        /// <summary>
        /// Oferta a la cual se realizó la Reservación
        /// </summary>
        public OfferPortering Offer { get; set; }

        /// <summary>
        /// Camión que se reservó
        /// </summary>
        public Truck Truck { get; set; }
    }
}