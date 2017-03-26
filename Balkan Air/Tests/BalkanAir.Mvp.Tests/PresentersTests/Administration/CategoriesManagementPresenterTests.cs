namespace BalkanAir.Mvp.Tests.PresentersTests.Administration
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Data.Models;
    using EventArgs.Administration;
    using Presenters.Administration;
    using Services.Data.Contracts;
    using TestObjects;
    using ViewContracts.Administration;

    [TestClass]
    public class CategoriesManagementPresenterTests
    {
        private Mock<ICategoriesManagementView> categoriesView;
        private Mock<ICategoriesServices> categoriesServices;
        private CategoriesManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.categoriesView = TestObjectFactoryViews.GetCategoriesManagementView();
            this.categoriesServices = TestObjectFactoryServices.GetCategoriesServices();

            this.presenter = new CategoriesManagementPresenter(this.categoriesView.Object,
                this.categoriesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenCategoriesServicesIsNull()
        {
            var presenter = new CategoriesManagementPresenter(this.categoriesView.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddCategoriesToViewModelWhenOnGetDataEventIsRaised()
        {
            this.categoriesView.Raise(c => c.OnCategoriesGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Categories.ToList(),
                this.categoriesView.Object.Model.Categories.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                It.Is<CategoriesManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                new CategoriesManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.categoriesView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.categoriesView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                new CategoriesManagementEventArgs() { Id = invalidId });

            this.categoriesView.Verify(c => c.TryUpdateModel(It.IsAny<Category>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;

            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                new CategoriesManagementEventArgs() { Id = validId });

            this.categoriesView.Verify(c => c.TryUpdateModel(It.Is<Category>(ct => ct.Id == validId)), Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateCategoriesWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                new CategoriesManagementEventArgs() { Id = validId });

            this.categoriesServices
                .Verify(c => c.UpdateCategory(validId, It.IsAny<Category>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateCategoryWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;

            this.categoriesView.Raise(c => c.OnCategoriesUpdateItem += null,
                new CategoriesManagementEventArgs() { Id = validId });

            this.categoriesServices
                .Verify(c => c.UpdateCategory(validId, It.Is<Category>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.categoriesView.Raise(c => c.OnCategoriesDeleteItem += null,
                It.Is<CategoriesManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteCategoryWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.categoriesView.Raise(c => c.OnCategoriesDeleteItem += null,
                new CategoriesManagementEventArgs() { Id = validId });

            this.categoriesServices.Verify(a => a.DeleteCategory(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.categoriesView.Raise(a => a.OnCategoriesAddItem += null,
                It.Is<CategoriesManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddCategoryAndReturnIdWhenOnAddItemEventIsRaised()
        {
            var name = "Test Name";

            var categoryEventArgs = new CategoriesManagementEventArgs()
            {
                Name = name
            };

            this.categoriesView.Raise(c => c.OnCategoriesAddItem += null, categoryEventArgs);

            var expectedId = 1;

            this.categoriesServices
                .Verify(c => c.AddCategory(It.Is<Category>(m => m.Name == name)),Times.Once);

            Assert.AreEqual(expectedId, categoryEventArgs.Id);
        }
    }
}
