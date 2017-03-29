namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BalkanAir.Tests.Common;
    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Controllers;
    using Models.Airports;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class AirportsControllerTests
    {
        private Mock<IAirportsServices> airportsServices;
        private AirportsController airportsController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.airportsServices = TestObjectFactoryServices.GetAirportsServices();
            this.airportsController = new AirportsController(this.airportsServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAirportRequesModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Create(model);

            Assert.IsFalse(this.airportsController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAirportRequesModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidAirportRequesModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {            
            var result = this.airportsController.All();
            var okResult = result as OkNegotiatedContentResult<List<AirportResponseModel>>;
            var expectedAirportsCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedAirportsCount, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.airportsController.Get(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.airportsController.Get(notFoundId);
            
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.airportsController.Get(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<AirportResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.AIRPROT_VALID_NAME, okResult.Content.Name);
            Assert.AreEqual(Constants.AIRPORT_VALID_ABBREVIATION, okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnBadRequesWithInvalidAbbreviationMessage()
        {
            var result = this.airportsController.GetByAbbreviation(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnNotFound()
        {
            var notFoundAbbreviation = "ppp";
            var result = this.airportsController.GetByAbbreviation(notFoundAbbreviation);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnOkResultWithData()
        {
            var result = this.airportsController.GetByAbbreviation(Constants.AIRPORT_VALID_ABBREVIATION);
            var okResult = result as OkNegotiatedContentResult<AirportResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.AIRPROT_VALID_NAME, okResult.Content.Name);
            Assert.AreEqual(Constants.AIRPORT_VALID_ABBREVIATION, okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.airportsController.Update(0, TestObjectFactoryDataTransferModels.GetInvalidUpdateAirportRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateAirportRequestModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.IsFalse(this.airportsController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateAirportRequestModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAirportRequestModel();
            this.airportsController.Validate(model);

            var notFoundId = 10;
            var result = this.airportsController.Update(notFoundId, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.airportsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAirportRequestModel();
            this.airportsController.Validate(model);

            var result = this.airportsController.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.airportsController.Delete(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.airportsController.Delete(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.airportsController.Delete(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }
    }
}
