namespace BalkanAir.Api.Tests.RouteTests
{
    using System;
    using System.Net.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;
    using MyTested.WebApi.Exceptions;

    using Newtonsoft.Json;

    using TestObjects;
    using Web.Areas.Api.Controllers;

    [TestClass]
    public class FlightsRouteTests
    {
        private const string CREATE_PATH = "/Api/Flights/Create/";
        private const string GET_PATH_WITH_INVALID_ACTION = "/Api/Flight/";
        private const string GET_PATH = "/Api/Flights/";
        private const string GET_BY_FLIGHT_STATUS = "/Api/Flights/Status/";
        private const string GET_BY_DEPARTURE_AIRPORT = "/Api/Flights/Departures/";
        private const string GET_BY_ARRIVAL_AIRPORT = "/Api/Flights/Arrivals/";
        private const string GET_BY_DEPARTURE_DATE_TIME = "/Api/Flights/DateTime/";
        private const string UPDATE_PATH = "/Api/Flights/Update/";
        private const string DELETE_PATH = "/Api/Flights/Delete/";

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void CreateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var flightRequestModel = TestObjectFactory.GetValidFlightRequestModel();
            string jsonContent = JsonConvert.SerializeObject(flightRequestModel);

            var invalidHttpMethod = HttpMethod.Get;

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<FlightsController>(f => f.Create(flightRequestModel));
        }

        [TestMethod]
        public void CreateShouldMapCorrectAction()
        {
            var flightRequestModel = TestObjectFactory.GetValidFlightRequestModel();
            string jsonContent = JsonConvert.SerializeObject(flightRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Post)
                .To<FlightsController>(f => f.Create(flightRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH_WITH_INVALID_ACTION)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenControllerIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<AirportsController>(f => f.All());
        }

        [TestMethod]
        public void GetAllShouldMapCorrectAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.Get(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var modelId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + pathId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.Get(modelId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + notIntegerId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.Get(1));
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validId)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.Get(validId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByFlightNumberShouldThrowExceptionWithRouteDoesNotExistWhenNumberIsNull()
        {
            var nullableFlightNumber = string.Empty;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + nullableFlightNumber)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByFlightNumber(null));
        }

        [TestMethod]
        public void GetByFlightNumberShouldMapCorrectAction()
        {
            var flightNumber = "NUMBER";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + flightNumber)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByFlightNumber(flightNumber));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByFlightStatusShouldThrowExceptionWithRouteDoesNotExistWhenStatusIsNull()
        {
            var nullableFlightStatus = string.Empty;

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_FLIGHT_STATUS)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByFlightStatus(nullableFlightStatus));
        }

        [TestMethod]
        public void GetByFlightStatusShouldMapCorrectAction()
        {
            var flightStatus = "Test";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_FLIGHT_STATUS + flightStatus)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByFlightStatus(flightStatus));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByDepartureAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooLong()
        {
            var longAbbreviation = "Too long abbreviation";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_AIRPORT + longAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureAirport(longAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByDepartureAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooShort()
        {
            var shortAbbreviation = "s";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_AIRPORT + shortAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureAirport(shortAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByDepartureAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsNotString()
        {
            var notStringAbbreviation = "123";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_AIRPORT + notStringAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureAirport(notStringAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByDepartureAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsNull()
        {
            var nullableAbbreviation = string.Empty;

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_AIRPORT + nullableAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureAirport(null));
        }

        [TestMethod]
        public void GetByDepartureAirportShouldMapCorrectAction()
        {
            var validAbbreviation = "SOF";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_AIRPORT + validAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureAirport(validAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByArrivalAirporttShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooLong()
        {
            var longAbbreviation = "Too long abbreviation";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_ARRIVAL_AIRPORT + longAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByArrivalAirport(longAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByArrivalAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooShort()
        {
            var shortAbbreviation = "s";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_ARRIVAL_AIRPORT + shortAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByArrivalAirport(shortAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByArrivalAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsNotString()
        {
            var notStringAbbreviation = "123";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_ARRIVAL_AIRPORT + notStringAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByArrivalAirport(notStringAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByArrivalAirportShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsNull()
        {
            var nullableAbbreviation = string.Empty;

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_ARRIVAL_AIRPORT + nullableAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByArrivalAirport(null));
        }

        [TestMethod]
        public void GetByArrivalAirportShouldMapCorrectAction()
        {
            var validAbbreviation = "MAD";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_ARRIVAL_AIRPORT + validAbbreviation)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByArrivalAirport(validAbbreviation));
        }
        
        [TestMethod]
        public void GetByDepartureDateTimeShouldMapCorrectAction()
        {
            var dateTime = new DateTime(2017, 4, 26, 17, 30, 0);
            var dateTimeAsString = "2017-04-26T17:30:00";

            MyWebApi
                .Routes()
                .ShouldMap(GET_BY_DEPARTURE_DATE_TIME + dateTimeAsString)
                .WithHttpMethod(HttpMethod.Get)
                .To<FlightsController>(f => f.GetByDepartureDateTime(dateTime));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var updateFlightRequestModel = TestObjectFactory.GetValidUpdateFlightRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateFlightRequestModel);

            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + negativeId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FlightsController>(a => a.Update(negativeId, updateFlightRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var updateFlightRequestModel = TestObjectFactory.GetValidUpdateFlightRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateFlightRequestModel);

            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + pathId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FlightsController>(a => a.Update(methodId, updateFlightRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var updateFlightRequestModel = TestObjectFactory.GetValidUpdateFlightRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateFlightRequestModel);

            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + notIntegerId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FlightsController>(a => a.Update(updateFlightRequestModel.Id, updateFlightRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            var updateFlightRequestModel = TestObjectFactory.GetValidUpdateFlightRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateFlightRequestModel);

            var invalidHttpMethod = HttpMethod.Post;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateFlightRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(invalidHttpMethod)
                .To<FlightsController>(a => a.Update(updateFlightRequestModel.Id, updateFlightRequestModel));
        }

        [TestMethod]
        public void UpdateShouldMapCorrectAction()
        {
            var updateFlightRequestModel = TestObjectFactory.GetValidUpdateFlightRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateFlightRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateFlightRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<FlightsController>(a => a.Update(updateFlightRequestModel.Id, updateFlightRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + negativeId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FlightsController>(a => a.Delete(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + pathId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FlightsController>(a => a.Delete(methodId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + notIntegerId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FlightsController>(a => a.Delete(1));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            var validId = 1;
            var invalidHttpMethod = HttpMethod.Post;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(invalidHttpMethod)
                .To<FlightsController>(a => a.Delete(validId));
        }

        [TestMethod]
        public void DeleteShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<FlightsController>(a => a.Delete(validId));
        }
    }
}
