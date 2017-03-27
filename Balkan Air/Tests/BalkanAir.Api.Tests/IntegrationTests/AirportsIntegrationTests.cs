//namespace BalkanAir.Api.Tests.IntegrationTests
//{
//    using System.Net;

//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    using BalkanAir.Tests.Common.TestObjects;
//    using Data.Models;
//    using Services.Data.Tests.TestObjects;

//    [TestClass]
//    public class AirportsIntegrationTests
//    {
//        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
//        private const string GET_REQUEST_URL = "/Api/Airports";

//        [TestMethod]
//        public void GetByIdShouldReturnCorrectResponse()
//        {
//            var server = new InMemoryHttpServer<Airport>(
//                IN_MEMORY_SERVER_URL, 
//                TestObjectFactoryRepositories.GetAirportsRepository());

//            var response = server.CreateGetRequest(GET_REQUEST_URL);

//            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
//            Assert.IsNotNull(response.Content);
//        }
//    }
//}
