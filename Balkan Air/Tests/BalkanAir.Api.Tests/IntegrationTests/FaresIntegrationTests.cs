namespace BalkanAir.Api.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Tests.Common;
    using BalkanAir.Tests.Common.TestObjects;
    using Data.Models;
    using Services.Data.Tests.TestObjects;

    [TestClass]
    public class FaresIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/Fares/";
        private const string INVALID_GET_REQUEST_URL = "/Api/Fare/";

        private InMemoryHttpServer<Fare> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<Fare>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetFaresRepository());
        }

        [TestMethod]
        public void GetAllShouldReturnStatus404NotFoundWhenRequestUrlIsInvalid()
        {
            var response = this.server.CreateGetRequest(INVALID_GET_REQUEST_URL);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetAllShouldReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(GET_REQUEST_URL);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnStatus400BadRequestWhenIdIsZero()
        {
            var invalidId = 0;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + invalidId);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
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
        public void GetByIdShouldReturnStatus404NotFoundWhenThereIsNoCountryWithThisId()
        {
            var noSuchId = 1000;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + noSuchId);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByIdShouldReturnStatus200OkWithData()
        {
            var validId = 1;

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + validId);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldNotMapCorrectActionAndReturnStatus404NotFoundtWhenOriginAbbreviationIsTooShort()
        {
            var tooShortOriginAbbreviation = "a";
            var validDestinationAbbreviation = "sof";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL + 
                tooShortOriginAbbreviation + "/" + 
                validDestinationAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldNotMapCorrectActionAndReturnStatus404NotFoundtWhenOriginAbbreviationIsTooLong()
        {
            var tooLongOriginAbbreviation = "abcdef";
            var validDestinationAbbreviation = "sof";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                tooLongOriginAbbreviation + "/" +
                validDestinationAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldNotMapCorrectActionAndReturnStatus404NotFoundtWhenDestinationAbbreviationIsTooShort()
        {
            var validOriginAbbreviation = "sof";
            var tooShortDestinationAbbreviation = "a";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                validOriginAbbreviation + "/" +
                tooShortDestinationAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldNotMapCorrectActionAndReturnStatus404NotFoundtWhenDestinationAbbreviationIsTooLong()
        {
            var validOriginAbbreviation = "sof";
            var tooLongDestinationAbbreviation = "a";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                validOriginAbbreviation + "/" +
                tooLongDestinationAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldReturnStatus404NotFoundWhenThereIsNoFareWithThisOriginAbbreviation()
        {
            var noSuchOriginAbbreviation = "sof";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                noSuchOriginAbbreviation + "/" +
                Constants.ROUTE_VALID_DESTINATION_ABBREVIATION);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByRouteShouldReturnStatus404NotFoundWhenThereIsNoFareWithThisDestinationAbbreviation()
        {
            var noSuchDestinationAbbreviation = "qwe";

            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                Constants.ROUTE_VALID_ORIGIN_ABBREVIATION + "/" +
                noSuchDestinationAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByRouteShouldReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(
                GET_REQUEST_URL +
                Constants.ROUTE_VALID_ORIGIN_ABBREVIATION + "/" +
                Constants.ROUTE_VALID_DESTINATION_ABBREVIATION);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }
    }
}
