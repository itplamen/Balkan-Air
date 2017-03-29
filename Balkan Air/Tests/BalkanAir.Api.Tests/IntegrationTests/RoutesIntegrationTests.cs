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
    public class RoutesIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/Routes/";
        private const string INVALID_GET_REQUEST_URL = "/Api/Route/";

        private InMemoryHttpServer<Route> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<Route>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetRoutesRepository());
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
        public void GetByIdShouldMapCorrectActionAndReturnStatus404NotFoundWhenThereIsNoRouteWithThisId()
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
    }
}
