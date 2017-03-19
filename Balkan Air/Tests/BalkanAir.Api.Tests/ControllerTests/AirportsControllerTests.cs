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
    using Web.Areas.Api.Models.Airports;

    [TestClass]
    public class AirportsControllerTests
    {
        private IAirportsServices airportsServices;

        [TestInitialize]
        public void TestInitialize()
        {
            this.airportsServices = TestObjectFactory.GetAirportsServices();
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidAirportRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidAirportRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidAirportRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var controller = new AirportsController(this.airportsServices);
            
            var result = controller.All();
            var okResult = result as OkNegotiatedContentResult<List<AirportResponseModel>>;
           
            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Get(0);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Get(10);
            
            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Get(1);
            var okResult = result as OkNegotiatedContentResult<AirportResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Test Name", okResult.Content.Name);
            Assert.AreEqual("ABC", okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnBadRequesWithInvalidAbbreviationMessage()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.GetByAbbreviation(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnNotFound()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.GetByAbbreviation("ppp");

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnOkResultWithData()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.GetByAbbreviation("ABC");
            var okResult = result as OkNegotiatedContentResult<AirportResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Test Name", okResult.Content.Name);
            Assert.AreEqual("ABC", okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Update(0, TestObjectFactory.GetInvalidUpdateAirportRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateAirportRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateAirportRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateAirportRequestModel();
            controller.Validate(model);

            var result = controller.Update(10, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateAirportRequestModel();
            controller.Validate(model);

            var result = controller.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var controller = new AirportsController(this.airportsServices);
           
            var result = controller.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var controller = new AirportsController(this.airportsServices);

            var result = controller.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
