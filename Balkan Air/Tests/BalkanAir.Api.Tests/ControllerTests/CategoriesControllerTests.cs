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
    using Models.Categories;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class CategoriesControllerTests
    {
        private Mock<ICategoriesServices> categoriesServices;
        private CategoriesController categoriesController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.categoriesServices = TestObjectFactoryServices.GetCategoriesServices();
            this.categoriesController = new CategoriesController(this.categoriesServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidCategoryRequesModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Create(model);

            Assert.IsFalse(this.categoriesController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidCategoryRequesModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidCategoryRequesModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.categoriesController.All();
            var okResult = result as OkNegotiatedContentResult<List<CategoryResponseModel>>;
            var expectedCategoriesCount = 1;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedCategoriesCount, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.categoriesController.Get(0);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.categoriesController.Get(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.categoriesController.Get(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<CategoryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.CATEGORY_VALID_NAME, okResult.Content.Name);
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnBadRequestWithNameNotNullOrEmptyMessage()
        {
            var result = this.categoriesController.GetCategoryByName(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.NULL_OR_EMPTY_ENTITY_NAME, badRequestResult.Message);
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnNotFoundWhenThereIsNoCategoryWithThisName()
        {
            var invalidCategoryName = "invalid category name";
            var result = this.categoriesController.GetCategoryByName(invalidCategoryName);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetCategoryByNameShouldReturnOkResultWithData()
        {
            var result = this.categoriesController.GetCategoryByName(Constants.CATEGORY_VALID_NAME);
            var okResult = result as OkNegotiatedContentResult<CategoryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content.Id);
            Assert.AreEqual(Constants.CATEGORY_VALID_NAME, okResult.Content.Name);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var invalidId = -1;

            var result = this.categoriesController.Update(
                invalidId, 
                TestObjectFactoryDataTransferModels.GetInvalidUpdateCategoryRequestModel());

            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateCategoryRequestModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.IsFalse(this.categoriesController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateCategoryRequestModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Update(Constants.ENTITY_VALID_ID, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateCategoryRequestModel();
            this.categoriesController.Validate(model);

            var notFoundId = 10;
            var result = this.categoriesController.Update(notFoundId, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.categoriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateCategoryRequestModel();
            this.categoriesController.Validate(model);

            var result = this.categoriesController.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var invalidId = -1;
            var result = this.categoriesController.Delete(invalidId);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var notFoundId = 10;
            var result = this.categoriesController.Delete(notFoundId);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.categoriesController.Delete(Constants.ENTITY_VALID_ID);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(Constants.ENTITY_VALID_ID, okResult.Content);
        }
    }
}
