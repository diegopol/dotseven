using System.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeldarAutoAssignment.Core;

namespace PeldarAutoAssignment.UnitTest
{
    [TestClass]
    public class PeldarPageTest
    {
        protected string BaseUrl = "http://transporte.peldar.com:8080/";
        protected string LoginUserName = "Translogistica";
        protected string LoginPassword = "viviana01";
        protected int TimeOut = 60000; //60 segundos máximo



        [TestMethod]
        public void CanLogin()
        {
            var peldarService = new PeldarServicePage(this.BaseUrl) {Timeout = this.TimeOut};
            var authenticationCookie = peldarService.Authenticate(this.LoginUserName, this.LoginPassword);
            Assert.IsNotNull(authenticationCookie);

        }

        [TestMethod]
        [ExpectedException(typeof(SecurityException), "Nombre o Usuario inválido")]
        public void CannotLogin()
        {
            var peldarService = new PeldarServicePage(this.BaseUrl) {Timeout = this.TimeOut};
            var authenticationCookie = peldarService.Authenticate(this.LoginUserName, "xxxxxxxxxx");
            Assert.IsNotNull(authenticationCookie);

        }

        [TestMethod]
        public void HasOffers()
        {
            var peldarService = new PeldarServicePage(this.BaseUrl) {Timeout = this.TimeOut};
            var authenticationCookie = peldarService.Authenticate(this.LoginUserName, this.LoginPassword);
            var offers = peldarService.GetOffers(authenticationCookie);
            Assert.IsTrue(offers.OfferResult == OffersCollection.EOfferResults.HasOffers);
        }
    }
}
