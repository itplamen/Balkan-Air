namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services.Common;
    using Services.Data.Contracts;
    using TestObjects;
    using Web.Areas.Api.Controllers;
    using Web.Areas.Api.Models.Fares;

    [TestClass]
    public class FaresControllerTests
    {
        private IFaresServices faresServices;
        private FaresController faresController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.faresServices = TestObjectFactory.GetFaresServices();
            this.faresController = new FaresController(this.faresServices);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);

            Assert.IsFalse(this.faresController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidFareRequestModel();
            this.faresController.Validate(model);

            var result = this.faresController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.faresController.All();
            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequestWithInvalidIdMessage()
        {
            var result = this.faresController.Get(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var result = this.faresController.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.faresController.Get(1);
            var okResult = result as OkNegotiatedContentResult<FareResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual(1m, okResult.Content.Price);
            Assert.AreEqual("Test Origin", okResult.Content.Route.Origin.Name);
            Assert.AreEqual("Test Destination", okResult.Content.Route.Destination.Name);
        }

        [TestMethod]
        public void GetByRouteShouldReturnBadRequestWithInvalidMessage()
        {
            var result = this.faresController.GetByRoute("origin", null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByRouteShouldReturnOkResultWithData()
        {
            var result = this.faresController.GetByRoute("ABC", "DEF");
            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidIdMessage()
        {
            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();

            var result = this.faresController.Update(-1, validModel);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactory.GetInvalidUpdateFareRequestModel();
            this.faresController.Validate(invalidModel);

            var result = this.faresController.Update(1, invalidModel);

            Assert.IsFalse(this.faresController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactory.GetInvalidUpdateFareRequestModel();
            this.faresController.Validate(invalidModel);

            var result = this.faresController.Update(1, invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();
            this.faresController.Validate(validModel);

            var result = this.faresController.Update(10, validModel);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturOkResultWithId()
        {
            this.faresController.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();
            this.faresController.Validate(validModel);

            var result = this.faresController.Update(1, validModel);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(validModel.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequestWithInvalidIdMessage()
        {
            var result = this.faresController.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var result = this.faresController.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.faresController.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}