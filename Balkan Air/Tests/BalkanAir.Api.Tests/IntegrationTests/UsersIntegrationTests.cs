namespace BalkanAir.Api.Tests.IntegrationTests
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Tests.Common;
    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Data.Models;
    using Services.Data.Tests.TestObjects;

    [TestClass]
    public class UsersIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/Users/";
        private const string INVALID_GET_REQUEST_URL = "/Api/User/";
        private const string GET_BY_GENDER_REQUEST_URL = "/Api/Users/Gender/";
        private const string GET_BY_NATIONALITY_REQUEST_URL = "/Api/Users/Nationality/";

        private InMemoryHttpServer<User> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<User>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetUsersRepository());
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
        public void GetByGenderShouldMapCorrectActionAndReturnStatus400BadRequestWhenGenderIsInvalid()
        {
            var invalidGender = "gender";

            var response = this.server.CreateGetRequest(GET_BY_GENDER_REQUEST_URL + invalidGender);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(ErrorMessages.INVALID_GENDER));
        }

        [TestMethod]
        public void GetByGenderShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validGender = Gender.Male.ToString();

            var response = this.server.CreateGetRequest(GET_BY_GENDER_REQUEST_URL + validGender);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.USER_VALID_GENDER.ToString()));
        }

        [TestMethod]
        public void GetByNationalityShouldMapCorrectActionAndReturnStatus404NotFoundWhenNationalityIsInvalid()
        {
            var invalidNationality = "test"; 

            var response = this.server.CreateGetRequest(GET_BY_NATIONALITY_REQUEST_URL + invalidNationality);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByNationalityShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(GET_BY_NATIONALITY_REQUEST_URL + Constants.USER_VALID_NATIONALITY);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.USER_VALID_NATIONALITY));
        }
    }
}
