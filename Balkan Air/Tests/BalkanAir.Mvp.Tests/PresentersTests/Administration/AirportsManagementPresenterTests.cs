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
    public class AirportsManagementPresenterTests
    {
        private Mock<IAirportsManagementView> airportsView;
        private Mock<IAirportsServices> airportsServices;
        private Mock<ICountriesServices> countriesServices;
        private AirportsManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.airportsView = TestObjectFactoryViews.GetAirportsManagementView();
            this.airportsServices = TestObjectFactoryServices.GetAirportsServices();
            this.countriesServices = TestObjectFactoryServices.GetCountriesServices();
            this.presenter = new AirportsManagementPresenter(this.airportsView.Object,
                this.airportsServices.Object, this.countriesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAirportsServicesIsNull()
        {
            var presenter = new AirportsManagementPresenter(this.airportsView.Object,
                null, this.countriesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenCountriesServicesIsNull()
        {
            var presenter = new AirportsManagementPresenter(this.airportsView.Object,
                this.airportsServices.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddAirportsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.airportsView.Raise(a => a.OnAirprotsGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Airports.ToList(),
                this.airportsView.Object.Model.Airports.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                It.Is<AirportsManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.airportsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.airportsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs() { Id = invalidId });

            this.airportsView.Verify(a => a.TryUpdateModel(It.IsAny<Airport>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;
            var validCountryId = 1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs()
                {
                    Id = validId,
                    CountryId = validCountryId
                });

            this.airportsView.Verify(a => a.TryUpdateModel(
                It.Is<Airport>(ar => ar.Id == validId && ar.CountryId == validCountryId)),
                Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateAirportWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs() { Id = validId });

            this.airportsServices.Verify(a => a.UpdateAirport(validId, It.IsAny<Airport>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateItemShouldThrowExceptionWhenCountryIdIsInvalid()
        {
            var validId = 1;
            var invalidCountryId = -1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs()
                {
                    Id = validId,
                    CountryId = invalidCountryId
                });
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateAirportWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;
            var validCountryId = 1;

            this.airportsView.Raise(a => a.OnAirportsUpdateItem += null,
                new AirportsManagementEventArgs()
                {
                    Id = validId,
                    CountryId = validCountryId
                });

            this.airportsServices.Verify(a => a.UpdateAirport(
                validId,
                It.Is<Airport>(ar => ar.Id == validId && ar.CountryId == validCountryId)),
                Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.airportsView.Raise(a => a.OnAirportsDeleteItem += null,
                It.Is<AirportsManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteAirportWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.airportsView.Raise(a => a.OnAirportsDeleteItem += null,
                new AirportsManagementEventArgs() { Id = validId });

            this.airportsServices.Verify(a => a.DeleteAirport(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.airportsView.Raise(a => a.OnAirprotsAddItem += null,
                It.Is<AirportsManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddAirportAndReturnIdWhenOnAddItemEventIsRaised()
        {
            var name = "Test Name";
            var abbreviation = "ABC";
            var countryId = 1;

            var airportEventArgs = new AirportsManagementEventArgs()
            {
                Name = name,
                Abbreviation = abbreviation,
                CountryId = countryId
            };

            this.airportsView.Raise(a => a.OnAirprotsAddItem += null, airportEventArgs);

            var expectedId = 1;

            this.airportsServices.Verify(a => a.AddAirport(
                It.Is<Airport>(ar => ar.Name == name && ar.Abbreviation == abbreviation &&
                    ar.CountryId == countryId)),
                Times.Once);

            Assert.AreEqual(expectedId, airportEventArgs.Id);
        }

        [TestMethod]
        public void GeDataShouldAddCountriesToViewModelWhenOnGetDataEventIsRaised()
        {
            this.airportsView.Raise(a => a.OnCountriesGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.airportsView.Object.Model.Countries.ToList().Any());
            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Countries.ToList(),
                this.airportsView.Object.Model.Countries.ToList());
        }
    }
}
