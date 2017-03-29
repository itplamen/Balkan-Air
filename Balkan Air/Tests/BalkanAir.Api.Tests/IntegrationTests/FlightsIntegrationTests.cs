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
    public class FlightsIntegrationTests
    {
        private const string IN_MEMORY_SERVER_URL = "http://localhost:12345";
        private const string GET_REQUEST_URL = "/Api/Flights/";
        private const string INVALID_GET_REQUEST_URL = "/Api/Flight/";
        private const string GET_BY_STATUS_REQUEST_URL = "/Api/Flights/Status/"; 
        private const string GET_BY_DEPARTURE_AIRPORT_REQUEST_URL = "/Api/Flights/Departures/";
        private const string GET_BY_ARRIVAL_AIRPORT_REQUEST_URL = "/Api/Flights/Arrivals/";
        private const string GET_BY_ROUTE_REQUEST_URL = "/Api/Flights/Route/{0}/{1}/";
        private const string GET_BY_DATE_TIME_REQUEST_URL = "/Api/Flights/DateTime/";

        private InMemoryHttpServer<LegInstance> server;

        [TestInitialize]
        public void TestInitialize()
        {
            this.server = new InMemoryHttpServer<LegInstance>(
                IN_MEMORY_SERVER_URL,
                TestObjectFactoryRepositories.GetLegInstancesRepository());
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
        public void GetByIdShouldMapCorrectActionAndReturnStatus404NotFoundWhenThereIsNoFlightWithThisId()
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
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByFlightNumberShouldMapCorrectActionAndReturnStatus404NotFoundtWhenThereIsNoFlightWithThisNumber()
        {
            var noSuchFlightNumber = "ABC132D";

            var response = this.server.CreateGetRequest(GET_REQUEST_URL + noSuchFlightNumber);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByFlightNumberShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(GET_REQUEST_URL + Constants.FLIGHT_VALID_NUMBER);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.FLIGHT_VALID_NUMBER));
        }

        [TestMethod]
        public void GetByFlightStatusShouldMapCorrectActionAndReturnStatus404NotFoundtWhenThereIsNoFlightWithThisFlightStatus()
        {
            var noSuchFlightStatus = "Status123";

            var response = this.server.CreateGetRequest(GET_BY_STATUS_REQUEST_URL + noSuchFlightStatus);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByFlightStatusShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(
                GET_BY_STATUS_REQUEST_URL + 
                Constants.FLIGHT_VALID_STATUS);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.FLIGHT_VALID_STATUS));
        }

        [TestMethod]
        public void GetByDepartureAirportShouldMapCorrectActionAndReturnStatus404NotFoundtWhenThereIsNoFlightWithThisAirportAbbreviation()
        {
            var noSuchDepartureAbbreviation = "aaa";

            var response = this.server.CreateGetRequest(
                GET_BY_DEPARTURE_AIRPORT_REQUEST_URL +
                noSuchDepartureAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByDepartureAirportShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(
                GET_BY_DEPARTURE_AIRPORT_REQUEST_URL +
                Constants.ROUTE_VALID_ORIGIN_ABBREVIATION);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.ROUTE_VALID_ORIGIN_ABBREVIATION));
        }

        [TestMethod]
        public void GetByArrivalAirportShouldMapCorrectActionAndReturnStatus404NotFoundtWhenThereIsNoFlightWithThisAirportAbbreviation()
        {
            var noSuchArrivalAbbreviation = "aaa";

            var response = this.server.CreateGetRequest(
                GET_BY_ARRIVAL_AIRPORT_REQUEST_URL +
                noSuchArrivalAbbreviation);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByArrivalAirportShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(
                GET_BY_ARRIVAL_AIRPORT_REQUEST_URL +
                Constants.ROUTE_VALID_DESTINATION_ABBREVIATION);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.ROUTE_VALID_DESTINATION_ABBREVIATION));
        }

        [TestMethod]
        public void GetByRouteShouldNotMapCorrectActionAndReturnStatus404NotFoundtWhenOriginAbbreviationIsTooShort()
        {
            var tooShortOriginAbbreviation = "a";
            var validDestinationAbbreviation = "sof";

            var response = this.server.CreateGetRequest(
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    tooShortOriginAbbreviation,
                    validDestinationAbbreviation));

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
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    tooLongOriginAbbreviation,
                    validDestinationAbbreviation));

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
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    validOriginAbbreviation,
                    tooShortDestinationAbbreviation));

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
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    validOriginAbbreviation,
                    tooLongDestinationAbbreviation));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
        }

        [TestMethod]
        public void GetByRouteShouldReturnStatus404NotFoundWhenThereIsNoFlightWithThisOriginAbbreviation()
        {
            var noSuchOriginAbbreviation = "sof";

            var response = this.server.CreateGetRequest(
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    noSuchOriginAbbreviation,
                    Constants.ROUTE_VALID_DESTINATION_ABBREVIATION));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByRouteShouldReturnStatus404NotFoundWhenThereIsNoFlightWithThisDestinationAbbreviation()
        {
            var noSuchDestinationAbbreviation = "qwe";

            var response = this.server.CreateGetRequest(
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    Constants.ROUTE_VALID_ORIGIN_ABBREVIATION,
                    noSuchDestinationAbbreviation));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByRouteShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var response = this.server.CreateGetRequest(
                string.Format(
                    GET_BY_ROUTE_REQUEST_URL,
                    Constants.ROUTE_VALID_ORIGIN_ABBREVIATION,
                    Constants.ROUTE_VALID_DESTINATION_ABBREVIATION));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.ROUTE_VALID_ORIGIN_ABBREVIATION));
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(Constants.ROUTE_VALID_DESTINATION_ABBREVIATION));
        }

        [TestMethod]
        public void GetByDepartureDateTimeShouldReturnStatus404NotFoundWhenThereIsNoFlightWithThiDateTime()
        {
            var noSuchDateTimeAsString = "2017-04-26T17:30:00";
            var response = this.server.CreateGetRequest(GET_BY_DATE_TIME_REQUEST_URL + noSuchDateTimeAsString);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetByDepartureDateTimeShouldMapCorrectActionAndReturnStatus200OkWithData()
        {
            var validDateTimeAsString = "2017-01-01T01:01:01";

            var response = this.server.CreateGetRequest(
               GET_BY_DATE_TIME_REQUEST_URL + validDateTimeAsString);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsNotNull(response.Content);
            Assert.AreNotEqual(typeof(ObjectContent<HttpError>), response.Content.GetType());
            Assert.IsTrue(response.Content.ReadAsStringAsync().Result.Contains(validDateTimeAsString));
        }
    }
}
