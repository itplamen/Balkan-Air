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
    using Models.Fares;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class FaresControllerTests
    {
        private Mock<IFaresServices> faresServices;
        private FaresController faresController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.faresServices = TestObjectFactoryServices.GetFaresServices();
            this.faresController = new FaresController(this.faresServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);

            Assert.IsFalse(this.faresController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.faresController.All();
            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;
            var expectedFaresCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedFaresCount, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequestWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.faresController.Get(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.faresController.Get(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.faresController.Get(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<FareResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.FARE_VALID_PRICE, okResult.Content.Price);
            Assert.AreEqual(Constants.ROUTE_VALID_ORIGIN_NAME, okResult.Content.Route.Origin.Name);
            Assert.AreEqual(Constants.ROUTE_VALID_DESTINATION_NAME, okResult.Content.Route.Destination.Name);
        }

        [TestMethod]
        public void GetByRouteShouldReturnBadRequestWithInvalidMessage()
        {
            var result = this.faresController.GetByRoute(
                Constants.ROUTE_VALID_ORIGIN_ABBREVIATION, 
                null);

            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByRouteShouldReturnOkResultWithData()
        {
            var result = this.faresController.GetByRoute(
                Constants.ROUTE_VALID_ORIGIN_ABBREVIATION,
                Constants.ROUTE_VALID_DESTINATION_ABBREVIATION);

            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;
            var expectedFaresCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedFaresCount, okResult.Content.Count);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidIdMessage()
        {
            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();

            var invalidId = -1;
            var result = this.faresController.Update(invalidId, validModel);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidUpdateFareRequestModel();
            this.faresController.Validate(invalidModel);

            var result = this.faresController.Update(Constants.ENTITY_VALID_ID, invalidModel);

            Assert.IsFalse(this.faresController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactoryDataTransferModels.GetInvalidUpdateFareRequestModel();
            this.faresController.Validate(invalidModel);

            var result = this.faresController.Update(Constants.ENTITY_VALID_ID, invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            this.faresController.Validate(validModel);

            var notFoundId = 10;
            var result = this.faresController.Update(notFoundId, validModel);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturOkResultWithId()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactoryDataTransferModels.GetValidUpdateFareRequestModel();
            this.faresController.Validate(validModel);

            var result = this.faresController.Update(Constants.ENTITY_VALID_ID, validModel);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(validModel.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequestWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.faresController.Delete(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.faresController.Delete(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.faresController.Delete(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }
    }
}