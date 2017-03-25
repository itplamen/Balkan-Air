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
    public class AircraftManufacturersManagementPresenterTests
    {
        private Mock<IAircraftManufacturersManagementView> aircraftManufacturersView;
        private Mock<IAircraftManufacturersServices> aircraftManufacturersServices;
        private Mock<IAircraftsServices> aircraftsServices;
        private AircraftManufacturersManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftManufacturersView = TestObjectFactoryViews.GetAircraftManufacturersManagementView();
            this.aircraftManufacturersServices = TestObjectFactoryServices.GetAircraftManufacturersServices();
            this.aircraftsServices = TestObjectFactoryServices.GetAircraftsServices();
            this.presenter = new AircraftManufacturersManagementPresenter(this.aircraftManufacturersView.Object,
                this.aircraftManufacturersServices.Object, this.aircraftsServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAircraftManufacturersServicesIsNull()
        {
            var presenter = new AircraftManufacturersManagementPresenter(
                this.aircraftManufacturersView.Object, null, this.aircraftsServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAircraftsServicesIsNull()
        {
            var presenter = new AircraftManufacturersManagementPresenter(this.aircraftManufacturersView.Object,
                this.aircraftManufacturersServices.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddManufacturersToViewModelWhenOnGetDataEventIsRaised()
        {
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.AircraftManufacturers.ToList(),
                this.aircraftManufacturersView.Object.Model.AircraftManufacturers.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                It.Is<AircraftManufacturersManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.aircraftManufacturersView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.aircraftManufacturersView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = invalidId });

            this.aircraftManufacturersView
                .Verify(a => a.TryUpdateModel(It.IsAny<AircraftManufacturer>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;

            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = validId });

            this.aircraftManufacturersView
                .Verify(a => a.TryUpdateModel(It.Is<AircraftManufacturer>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateManufacturerWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = validId });

            this.aircraftManufacturersServices
                .Verify(a => a.UpdateManufacturer(validId, It.IsAny<AircraftManufacturer>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateManufacturerWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersUpdateItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = validId });

            this.aircraftManufacturersServices
                .Verify(a => a.UpdateManufacturer(validId, It.Is<AircraftManufacturer>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersDeleteItem += null,
                It.Is<AircraftManufacturersManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteManufacturerWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersDeleteItem += null,
                new AircraftManufacturersManagementEventArgs() { Id = validId });

            this.aircraftManufacturersServices.Verify(a => a.DeleteManufacturer(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersAddItem += null,
                It.Is<AircraftManufacturersManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddManufacturerAndReturnIdWhenOnAddItemEventIsRaiser()
        {
            var manufacturerName = "Test Manufacturer";
            var manufacturerEventArgs = new AircraftManufacturersManagementEventArgs()
            {
                Name = manufacturerName
            };

            this.aircraftManufacturersView.Raise(a => a.OnAircraftManufacturersAddItem += null,
                manufacturerEventArgs);

            var expectedId = 1;

            this.aircraftManufacturersServices
                .Verify(a => a.AddManufacturer(
                    It.Is<AircraftManufacturer>(d => d.Name == manufacturerName)), Times.Once);

            Assert.AreEqual(expectedId, manufacturerEventArgs.Id);
        }

        [TestMethod]
        public void GetAircraftsShouldAddAircraftsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.aircraftManufacturersView.Raise(a => a.OnAircraftsGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.aircraftManufacturersView.Object.Model.Aircrafts.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.Aircrafts.ToList(), 
                this.aircraftManufacturersView.Object.Model.Aircrafts.ToList());
        }
    }
}
