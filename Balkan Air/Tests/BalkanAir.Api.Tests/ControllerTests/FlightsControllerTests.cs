namespace BalkanAir.Api.Tests.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Controllers;
    using Models.Flights;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class FlightsControllerTests
    {
        private Mock<ILegInstancesServices> flightsServices;
        private FlightsController flightsController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.flightsServices = TestObjectFactoryServices.GetFlightServices();
            this.flightsController = new FlightsController(this.flightsServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidFlightRequestModel();
            this.flightsController.Validate(invalidModel);

            var result = this.flightsController.Create(invalidModel);

            Assert.IsFalse(this.flightsController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidFlightRequestModel();
            this.flightsController.Validate(invalidModel);

            var result = this.flightsController.Create(invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactoryDataTransferModels.GetValidFlightRequestModel();
            this.flightsController.Validate(validModel);

            var result = this.flightsController.Create(validModel);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.flightsController.All();
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequestWithInvalidIdMessage()
        {
            var result = this.flightsController.Get(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var result = this.flightsController.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.flightsController.Get(1);
            var okResult = result as OkNegotiatedContentResult<FlightResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
        }

        [TestMethod]
        public void GetByFlightNumberShouldReturnBadRequestWithInvalidFlightNumberMessage()
        {
            var result = this.flightsController.GetByFlightNumber(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.NULL_OR_EMPTY_FLIGHT_NUMBER, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByFlightNumberShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByFlightNumber("Test12");
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByFlightStatusShouldReturnBadRequestWithInvalidFlightStatusMesssage()
        {
            var result = this.flightsController.GetByFlightStatus(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.NULL_OR_EMPTY_FLIGHT_STATUS, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByFlightStatusShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByFlightStatus("Test");
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByDepartureAirportShouldReturnBadRequestWithInvalidAbbreviationMessage()
        {
            var result = this.flightsController.GetByDepartureAirport(null);
            var badRequestMessage = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestMessage.Message);
        }

        [TestMethod]
        public void GetByDepartureAirportShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByDepartureAirport("ABC");
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByArrivalAirportShouldReturnBadRequestWithInvalidAbbreviationMessage()
        {
            var result = this.flightsController.GetByArrivalAirport(null);
            var badRequestMessage = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestMessage.Message);
        }

        [TestMethod]
        public void GetByArrivalAirportShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByArrivalAirport("DEF");
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByRouteShouldReturnBadRequestWithInvalidAbbreviationMessage()
        {
            var result = this.flightsController.GetByRoute(null, "DEF");
            var badRequestMessage = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestMessage.Message);
        }

        [TestMethod]
        public void GetByRouteShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByRoute("ABC", "DEF");
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByDepartureDateTimeShouldReturnOkResultWithData()
        {
            var result = this.flightsController.GetByDepartureDateTime(new DateTime(2017, 1, 1, 1, 1, 1));
            var okResult = result as OkNegotiatedContentResult<List<FlightResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidIdMessage()
        {
            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFlightRequestModel();
            var result = this.flightsController.Update(-1, validModel);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidUpdateFlightRequestModel();
            this.flightsController.Validate(invalidModel);

            var result = this.flightsController.Update(1, invalidModel);

            Assert.IsFalse(this.flightsController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidUpdateFlightRequestModel();
            this.flightsController.Validate(invalidModel);

            var result = this.flightsController.Update(1, invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFlightRequestModel();
            this.flightsController.Validate(validModel);

            var result = this.flightsController.Update(10, validModel);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.flightsController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFlightRequestModel();
            this.flightsController.Validate(validModel);

            var result = this.flightsController.Update(1, validModel);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(validModel.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequestWithInvalidIdMessage()
        {
            var result = this.flightsController.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var result = this.flightsController.Delete(10);
            
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.flightsController.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
