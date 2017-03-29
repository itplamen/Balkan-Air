namespace BalkanAir.Api.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Data.Models;
    using Services.Data.Tests.TestObjects;

    [TestClass]
    public class TravelClassesIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/TravelClasses/";
        private const string INVALID_GET_REQUEST_URL = "/Api/TravelClass/";
        private const string GET_BY_TYPE_REQUEST_URL = "/Api/TravelClasses/Type/";
        private const string GET_BY_AIRCRAFT_ID_REQUEST_URL = "/Api/TravelClasses/AircraftId/";

        private InMemoryHttpServer<TravelClass> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<TravelClass>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetTravelClassesRepository());
        }

        [TestMethod]
        public void GetAllShouldNotMapCorrectActionAndReturnStatus404NotFoundWhenRequestUrlIsInvalid()
        {
            var response = this.server.CreateGetRequest(INVALID_GET_REQUEST_URL);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetAllShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(GET_REQUEST_URL);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectActionAndReturnStatus400BadRequestWhenIdIsZero()
        {
            var invalidId = 0;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + invalidId);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(ErrorMessages.INVALID_ID));
        }

        [TestMethod]
        public void GetByIdShouldNotMapCorrectActionAndReturnStatus404NotFoundWhenIdIsNegative()
        {
            var negativeId = -1;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + negativeId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectActionAndReturnStatus404NotFoundWhenThereIsNoTravelClassWithThisId()
        {
            var noSuchId = 1000;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + noSuchId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validId = 1;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + validId);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetByTypeShouldMapCorrectActionAndReturnStatus404NotFoundWhenTypeIsInvalid()
        {
            var noSuchType = "type";

            var response = this.server.CreateGetRequest(GET_BY_TYPE_REQUEST_URL + noSuchType);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByTypeShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validType = TravelClassType.Economy.ToString();

            var response = this.server.CreateGetRequest(GET_BY_TYPE_REQUEST_URL + validType);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(TravelClassType.Economy.ToString()));
        }

        [TestMethod]
        public void GetByAircraftIdShouldNotMapCorrectActionAndReturnStatus404NotFoundWhenAircraftIdIsNegative()
        {
            var negativeAircraftId = -1;

            var response = this.server.CreateGetRequest(GET_BY_AIRCRAFT_ID_REQUEST_URL + negativeAircraftId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByAircraftIdShouldMapCorrectActionAndReturnStatus400BadRequestWhenAircraftIdIsZero()
        {
            var invalidAircraftId = 0;

            var response = this.server.CreateGetRequest(GET_BY_AIRCRAFT_ID_REQUEST_URL + invalidAircraftId);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(ErrorMessages.INVALID_ID));
        }

        [TestMethod]
        public void GetByAircraftIdShouldMapCorrectActionAndReturnStatus404NotFoundWhenThereIsNoAircraftWithThisId()
        {
            var aircraftId = 1000;

            var response = this.server.CreateGetRequest(GET_BY_AIRCRAFT_ID_REQUEST_URL + aircraftId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByAircraftIdShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validAircraftId = 1;

            var response = this.server.CreateGetRequest(GET_BY_AIRCRAFT_ID_REQUEST_URL + validAircraftId);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }
    }
}
