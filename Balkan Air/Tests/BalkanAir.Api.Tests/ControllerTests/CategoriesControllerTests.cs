namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Common;
    using Services.Data.Contracts;
    using TestObjects;
    using Web.Areas.Api.Controllers;
    using Web.Areas.Api.Models.Categories;

    [TestClass]
    public class CategoriesControllerTests
    {
        private ICategoriesServices categoriesServices;

        [TestInitialize]
        public void TestInitialize()
        {
            this.categoriesServices = TestObjectFactory.GetCategoriesServices();
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidCategoryRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidCategoryRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidCategoryRequesModel();
            controller.Validate(model);

            var result = controller.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.All();
            var okResult = result as OkNegotiatedContentResult<List<CategoryResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Get(0);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Get(1);
            var okResult = result as OkNegotiatedContentResult<CategoryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Category Test", okResult.Content.Name);
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnBadRequestWithNameNotNullOrEmptyMessage()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.GetCategoryByName(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.CATEGORY_NAME_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnBadRequestWithInvalidCategoryNameMessage()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.GetCategoryByName("Invaid Category Name");
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_CATEGORY_NAME, badRequestResult.Message);
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnOkResultWithData()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.GetCategoryByName("Category Test");
            var okResult = result as OkNegotiatedContentResult<CategoryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Category Test", okResult.Content.Name);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Update(0, TestObjectFactory.GetInvalidUpdateCategoryRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateCategoryRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetInvalidUpdateCategoryRequestModel();
            controller.Validate(model);

            var result = controller.Update(1, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateCategoryRequestModel();
            controller.Validate(model);

            var result = controller.Update(10, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            var controller = new CategoriesController(this.categoriesServices);
            controller.Configuration = new HttpConfiguration();

            var model = TestObjectFactory.GetValidUpdateCategoryRequestModel();
            controller.Validate(model);

            var result = controller.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var controller = new CategoriesController(this.categoriesServices);

            var result = controller.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
