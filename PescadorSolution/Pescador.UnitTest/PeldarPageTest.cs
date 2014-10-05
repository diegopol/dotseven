using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pescador.Core;
using Pescador.Core.Database;
using Pescador.Support.Exceptions;

namespace Pescador.UnitTest
{
    [TestClass]
    public class PeldarPageTest
    {
        private const string BaseUrl = "http://transporte.peldar.com:8080/";
        private const string LoginUserName = "Translogistica";
        private const string LoginPassword = "viviana01";
        private const int TimeOut = 60000; //60 segundos máximo

        /// <summary>
        /// Intentar de Logearse exitosamente
        /// </summary>
        [TestMethod]
        public void CanLogin()
        {
            var peldarService = new PeldarServicePage(BaseUrl) { Timeout = TimeOut };
            var authenticationCookie = peldarService.Authenticate(LoginUserName, LoginPassword);
            Assert.IsNotNull(authenticationCookie);
        }

        /// <summary>
        /// Intentar de Logearse con datos falsos
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void CannotLogin()
        {
            var peldarService = new PeldarServicePage(BaseUrl) { Timeout = TimeOut };
            var authenticationCookie = peldarService.Authenticate(LoginUserName, "xxxxxxxxxx");
            Assert.IsNotNull(authenticationCookie);
        }


        /// <summary>
        /// Intentar de obtener la lista de publicaciones de Ofertas de viajes
        /// </summary>
        [TestMethod]
        public void CheckOffers()
        {
            var peldarService = new PeldarServicePage(BaseUrl) { Timeout = TimeOut };
            var authenticationCookie = peldarService.Authenticate(LoginUserName, LoginPassword);
            var offers = peldarService.GetOffers();
            Assert.IsTrue(offers.OfferResult == OffersCollection.EOfferResults.HasOffers || offers.OfferResult == OffersCollection.EOfferResults.NoOffers);
        }

        ///// <summary>
        ///// Verificar si se tiene conexión a la BD y si existe como mínimo 1 camion.
        ///// </summary>
        //[TestMethod]
        //public void CanDatabaseConnect()
        //{
        //    using (var db = new PescadorDBEntities())
        //    {
        //        var allTrucks = db.Trucks.Count();
        //        Assert.IsTrue(allTrucks > 0);
        //    }
        //}
    }
}

