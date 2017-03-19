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

        [TestInitialize]
        public void TestInitialize()
        {
            this.faresServices = TestObjectFactory.GetFaresServices();
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidFareRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidFareRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidFareRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.All();
            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequestWithInvalidIdMessage()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Get(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Get(1);
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
            var cotnroller = new FaresController(this.faresServices);

            var result = cotnroller.GetByRoute("origin", null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByRouteShouldReturnOkResultWithData()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.GetByRoute("ABC", "DEF");
            var okResult = result as OkNegotiatedContentResult<List<FareResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidIdMessage()
        {
            var controller = new FaresController(this.faresServices);

            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();

            var result = controller.Update(-1, validModel);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactory.GetInvalidUpdateFareRequestModel();
            controller.Validate(invalidModel);

            var result = controller.Update(1, invalidModel);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var invalidModel = TestObjectFactory.GetInvalidUpdateFareRequestModel();
            controller.Validate(invalidModel);

            var result = controller.Update(1, invalidModel);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();
            controller.Validate(validModel);

            var result = controller.Update(10, validModel);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturOkResultWithId()
        {
            var controller = new FaresController(this.faresServices);
            controller.Configuration = new HttpConfiguration();

            var validModel = TestObjectFactory.GetValidUpdateFareRequestModel();
            controller.Validate(validModel);

            var result = controller.Update(1, validModel);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(validModel.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequestWithInvalidIdMessage()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var controller = new FaresController(this.faresServices);

            var result = controller.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
