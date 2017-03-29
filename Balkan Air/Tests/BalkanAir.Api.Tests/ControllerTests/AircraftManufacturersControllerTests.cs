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
    using Models.AircraftManufacturers;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class AircraftManufacturersControllerTests
    {
        private Mock<IAircraftManufacturersServices> aircraftManufacturersServices;
        private AircraftManufacturersController manufacturersController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftManufacturersServices = TestObjectFactoryServices.GetAircraftManufacturersServices();
            this.manufacturersController = new AircraftManufacturersController(this.aircraftManufacturersServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var result = this.manufacturersController.Create(model);

            Assert.IsFalse(this.manufacturersController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var result = this.manufacturersController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var result = this.manufacturersController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;
            var expectedId = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedId, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.manufacturersController.All();
            var okResult = result as OkNegotiatedContentResult<List<AircraftManufacturerResponseModel>>;
            var expectedManufacturersCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedManufacturersCount, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.manufacturersController.Get(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var noSuchValidId = 10; 
            var result = this.manufacturersController.Get(noSuchValidId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.manufacturersController.Get(1);
            var okResult = result as OkNegotiatedContentResult<AircraftManufacturerResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.MANUFACTURER_VALID_NAME, okResult.Content.Name);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.manufacturersController.Update(0, TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftManufacturerRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var result = this.manufacturersController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.IsFalse(this.manufacturersController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidUpdateAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(invalidModel);

            var result = this.manufacturersController.Update(Constants.ENTITY_VALID_ID, invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var notFoundId = 10;
            var result = this.manufacturersController.Update(notFoundId, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.manufacturersController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateAircraftManufacturerRequestModel();
            this.manufacturersController.Validate(model);

            var result = this.manufacturersController.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.manufacturersController.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var result = this.manufacturersController.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.manufacturersController.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
