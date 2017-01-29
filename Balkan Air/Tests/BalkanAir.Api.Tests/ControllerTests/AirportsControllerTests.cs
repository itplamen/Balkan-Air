namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TestObjects;
    using Web.Areas.Api.Controllers;
    using Web.Areas.Api.Models.Airports;
    using System.Collections.Generic;
    using Services.Data.Contracts;

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
        public void PostShouldValidateModelState()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new AirportsController(this.airportsServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
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
    }
}
