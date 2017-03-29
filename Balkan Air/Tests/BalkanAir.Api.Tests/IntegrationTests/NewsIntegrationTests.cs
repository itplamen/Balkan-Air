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
    public class NewsIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/News/";
        private const string INVALID_GET_REQUEST_URL = "/Api/New/";
        private const string GET_LATES_NEWS_REQUEST_URL = "/Api/News/Latest/";

        private InMemoryHttpServer<News> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<News>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetNewsRepository());
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
        public void GetByIdShouldReturnStatus404NotFoundWhenThereIsNoNewsWithThisId()
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
        public void GetLatestNewsShouldNotMapCorrectActionAndReturnStatus404NotFoundWhenCountIsNegative()
        {
            var negativeCount = -1;

            var response = this.server.CreateGetRequest(GET_LATES_NEWS_REQUEST_URL + negativeCount);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetLatestNewsShouldMapCorrectActionAndReturnStatus400BadRequestWhenCountIsZero()
        {
            var invalidCount = 0;

            var response = this.server.CreateGetRequest(GET_LATES_NEWS_REQUEST_URL + invalidCount);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetLatestNewsShouldReturnStatus200OkWithDataWhenCountIsPositiveNumber()
        {
            var validCount = 1;

            var response = this.server.CreateGetRequest(GET_LATES_NEWS_REQUEST_URL + validCount);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetLatesByCategoryShouldNotMapCorrectActionAndReturnStatus404NotFoundWhenCountIsNegative()
        {
            var negativeCount = -1;
            var validCategoryName = "category";

            var response = this.server.CreateGetRequest(
                GET_LATES_NEWS_REQUEST_URL + 
                negativeCount + "/" +
                validCategoryName);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetLatesByCategoryShouldMapCorrectActionAndReturnStatus400BadRequestWhenCountZero()
        {
            var invalidCount = 0;
            var validCategoryName = "category";
            var response = this.server.CreateGetRequest(
                GET_LATES_NEWS_REQUEST_URL + 
                invalidCount + "/" + 
                validCategoryName);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetLatesByCategoryShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validCount = 1;

            var response = this.server.CreateGetRequest(
                GET_LATES_NEWS_REQUEST_URL +
                validCount + "/" +
                Constants.CATEGORY_VALID_NAME);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.CATEGORY_VALID_NAME));
        }
    }
}
