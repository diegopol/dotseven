using System.Collections.Generic;

namespace PeldarAutoAssignment.Core
{
    /// <summary>
    /// Colección que contiene la lista de ofertas de viajes
    /// </summary>
    public class OffersCollection
    {
        /// <summary>
        /// Enum que contiene los diferentes tipos de Respuesta en la búsqueda de oferta de viajes
        /// </summary>
        public enum EOfferResults
        {
            NoOffers,
            HasOffers,
        }

        /// <summary>
        /// Obtener si contiene o no ofertas de viajes
        /// </summary>
        public EOfferResults OfferResult 
        { 
            get
            {
                if(this.Deals == null)
                    return EOfferResults.NoOffers;
                if(this.Deals.Count == 0)
                    return EOfferResults.NoOffers;
                return EOfferResults.HasOffers;
            }
        }
        /// <summary>
        /// Lista de ofertas de viajes
        /// </summary>
        public List<OfferPortering> Deals { get; set; }
    }
}