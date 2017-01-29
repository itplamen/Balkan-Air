namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Services.Data.Contracts;
    using TestObjects;
    using Web.Areas.Api.Controllers;
    using Web.Areas.Api.Models.AircraftManufacturers;

    [TestClass]
    public class AircraftManufacturersControllerTests
    {
        private IAircraftManufacturersServices aircraftManufacturersServices;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftManufacturersServices = TestObjectFactory.GetAircraftManufacturersServices();
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.All();
            var okResult = result as OkNegotiatedContentResult<List<AircraftManufacturerResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Get(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual("Invalid ID!", badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Get(1);
            var okResult = result as OkNegotiatedContentResult<AircraftManufacturerResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Manufacturer Test", okResult.Content.Name);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Update(0, TestObjectFactory.GetInvalidUpdateAircraftManufacturerRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual("Invalid ID!", badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Update(10, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateAircraftManufacturerRequestModel();
            controller.Validate(model);

            var result = controller.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual("Invalid ID!", badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var controller = new AircraftManufacturersController(this.aircraftManufacturersServices);

            var result = controller.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
