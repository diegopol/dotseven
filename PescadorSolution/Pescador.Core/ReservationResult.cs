using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pescador.Core
{
    /// <summary>
    /// Resultado de una operación de Reservación
    /// </summary>
    public class ReservationResult
    {
        /// <summary>
        /// Lista de Ofertas publicadas
        /// </summary>
        public List<OfferPortering> AvailableOffers { get; set; }

        /// <summary>
        /// Lista de Reservaciones realizadas
        /// </summary>
        public List<ReservationDetail> ReservationDetails { get; set; }

        /// <summary>
        /// Tiene reservaciones realizadas???
        /// </summary>
        public bool HaveReservations { 
            get
            {
                if (ReservationDetails != null)
                    return ReservationDetails.Count > 0;
                else
                {
                    return false;
                }
            }
        }
    }
}
