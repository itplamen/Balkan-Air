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
    using Models.Aircrafts;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class AircraftsControllerTests
    {
        private Mock<IAircraftsServices> aircraftsServices;
        private AircraftsController aircraftsController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftsServices = TestObjectFactoryServices.GetAircraftsServices();
            this.aircraftsController = new AircraftsController(this.aircraftsServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAircraftRequesModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Create(model);

            Assert.IsFalse(this.aircraftsController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAircraftRequesModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidAircraftRequesModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;
            
            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.aircraftsController.All();
            var okResult = result as OkNegotiatedContentResult<List<AircraftResponseModel>>;
            var expectedAircraftsCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedAircraftsCount, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.aircraftsController.Get(0);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.aircraftsController.Get(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.aircraftsController.Get(1);
            var okResult = result as OkNegotiatedContentResult<AircraftResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.AIRCRAFT_VALID_MODEL, okResult.Content.Model);
            Assert.AreEqual(Constants.AIRCRAFT_TOTAL_SEATS, okResult.Content.TotalSeats);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.aircraftsController.Update(0, TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftRequestModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.IsFalse(this.aircraftsController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftRequestModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAircraftRequestModel();
            this.aircraftsController.Validate(model);

            var notFoundId = 10;
            var result = this.aircraftsController.Update(notFoundId, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.aircraftsController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAircraftRequestModel();
            this.aircraftsController.Validate(model);

            var result = this.aircraftsController.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.aircraftsController.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.aircraftsController.Delete(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.aircraftsController.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }
    }
}
