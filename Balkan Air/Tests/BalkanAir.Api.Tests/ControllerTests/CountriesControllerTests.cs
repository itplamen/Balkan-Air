﻿namespace BalkanAir.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using BalkanAir.Tests.Common.TestObjects;
    using Common;
    using Controllers;
    using Models.Countries;
    using Services.Data.Contracts;
    using TestObjects;

    [TestClass]
    public class CountriesControllerTests
    {
        private Mock<ICountriesServices> countriesServices;
        private CountriesController countriesController;

        [TestInitialize]
        public void TestInitialize()
        {
            this.countriesServices = TestObjectFactoryServices.GetCountriesServices();
            this.countriesController = new CountriesController(this.countriesServices.Object);
        }

        [TestMethod]
        public void CreateShouldValidateModelState()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidCountryRequesModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Create(model);

            Assert.IsFalse(this.countriesController.ModelState.IsValid);
        }

        [TestMethod]
        public void CreateShouldReturnBadRequestWithInvalidModel()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidCountryRequesModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Create(model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void CreateShouldReturnOkResultWithId()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidCountryRequesModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Create(model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }

        [TestMethod]
        public void GetAllShouldReturnOkResultWithData()
        {
            var result = this.countriesController.All();
            var okResult = result as OkNegotiatedContentResult<List<CountryResponseModel>>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.countriesController.Get(0);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotFound()
        {
            var result = this.countriesController.Get(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByIdShouldReturnOkResultWithData()
        {
            var result = this.countriesController.Get(1);
            var okResult = result as OkNegotiatedContentResult<CountryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Country Test", okResult.Content.Name);
            Assert.AreEqual("CT", okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnBadRequesWithInvalidAbbreviationMessage()
        {
            var result = this.countriesController.GetByAbbreviation(null);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY, badRequestResult.Message);
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnNotFound()
        {
            var result = this.countriesController.GetByAbbreviation("IK");

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void GetByAbbreviationShouldReturnOkResultWithData()
        {
            var result = this.countriesController.GetByAbbreviation("CT");
            var okResult = result as OkNegotiatedContentResult<CountryResponseModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Country Test", okResult.Content.Name);
            Assert.AreEqual("CT", okResult.Content.Abbreviation);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.countriesController.Update(0, TestObjectFactoryDataTransferModels.GetInvalidUpdateCountryRequestModel());
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void UpdateShouldValidateModelState()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateCountryRequestModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Update(1, model);

            Assert.IsFalse(this.countriesController.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateShouldReturnBadRequestWithInvalidModel()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetInvalidUpdateCountryRequestModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Update(1, model);

            Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnNotFound()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateCountryRequestModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Update(10, model);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void UpdateShouldReturnOkResultWithId()
        {
            this.countriesController.Configuration = new HttpConfiguration();

            var model = TestObjectFactoryDataTransferModels.GetValidUpdateCountryRequestModel();
            this.countriesController.Validate(model);

            var result = this.countriesController.Update(model.Id, model);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(model.Id, okResult.Content);
        }

        [TestMethod]
        public void DeleteShouldReturnBadRequesWithInvalidIdMessage()
        {
            var result = this.countriesController.Delete(-1);
            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
            Assert.AreEqual(ErrorMessages.INVALID_ID, badRequestResult.Message);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var result = this.countriesController.Delete(10);

            Assert.AreEqual(typeof(NotFoundResult), result.GetType());
        }

        [TestMethod]
        public void DeleteShouldReturnOkResultWithId()
        {
            var result = this.countriesController.Delete(1);
            var okResult = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content);
        }
    }
}
