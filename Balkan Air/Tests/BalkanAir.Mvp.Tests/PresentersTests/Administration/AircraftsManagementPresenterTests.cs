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
    public class AircraftsManagementPresenterTests
    {
        private Mock<IAircraftsManagementView> aircraftsView;
        private Mock<IAircraftsServices> aircraftsServices;
        private Mock<IAircraftManufacturersServices> aircraftManufacturersServices;
        private AircraftsManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftsView = TestObjectFactoryViews.GetAircraftsManagementView();
            this.aircraftsServices = TestObjectFactoryServices.GetAircraftsServices();
            this.aircraftManufacturersServices = TestObjectFactoryServices.GetAircraftManufacturersServices();

            this.presenter = new AircraftsManagementPresenter(this.aircraftsView.Object,
                this.aircraftsServices.Object, this.aircraftManufacturersServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAircraftsServicesIsNull()
        {
            var presenter = new AircraftsManagementPresenter(this.aircraftsView.Object,
                null, this.aircraftManufacturersServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAircraftManufacturersServicesIsNull()
        {
            var presenter = new AircraftsManagementPresenter(this.aircraftsView.Object,
                this.aircraftsServices.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddAircraftsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.aircraftsView.Raise(a => a.OnAircraftsGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Aircrafts.ToList(),
                this.aircraftsView.Object.Model.Aircrafts.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                It.Is<AircraftsManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.aircraftsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.aircraftsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs() { Id = invalidId });

            this.aircraftsView.Verify(a => a.TryUpdateModel(It.IsAny<Aircraft>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;
            var validManufacturerId = 1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs()
                {
                    Id = validId,
                    AircraftManufacturerId = validManufacturerId
                });

            this.aircraftsView.Verify(a => a.TryUpdateModel(
                It.Is<Aircraft>(ar => ar.Id == validId && 
                    ar.AircraftManufacturerId == validManufacturerId)),
                Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateAircraftWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs() { Id = validId });

            this.aircraftsServices.Verify(a => a.UpdateAircraft(validId, It.IsAny<Aircraft>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateItemShouldThrowExceptionWhenAircraftManufacturerIdIsInvalid()
        {
            var validId = 1;
            var invalidManufacturerId = -1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs()
                {
                    Id = validId,
                    AircraftManufacturerId = invalidManufacturerId
                });
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateAircraftWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;
            var validManufacturerId = 1;

            this.aircraftsView.Raise(a => a.OnAircraftsUpdateItem += null,
                new AircraftsManagementEventArgs()
                {
                    Id = validId,
                    AircraftManufacturerId = validManufacturerId
                });

            this.aircraftsServices.Verify(a => a.UpdateAircraft(
                validId, 
                It.Is<Aircraft>(ar => ar.Id == validId && ar.AircraftManufacturerId == validManufacturerId)), 
                Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftsView.Raise(a => a.OnAircraftsDeleteItem += null,
                It.Is<AircraftsManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteAircraftWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.aircraftsView.Raise(a => a.OnAircraftsDeleteItem += null,
                new AircraftsManagementEventArgs() { Id = validId });

            this.aircraftsServices.Verify(a => a.DeleteAircraft(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.aircraftsView.Raise(a => a.OnAircraftsAddItem += null,
                It.Is<AircraftsManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddAircraftAndReturnIdWhenOnAddItemEventIsRaised()
        {
            var model = "Test Model";
            var totalSeats = 50;
            var manufacturerId = 1;

            var aircraftEventArgs = new AircraftsManagementEventArgs()
            {
                Model = model,
                TotalSeats = totalSeats,
                AircraftManufacturerId = manufacturerId
            };

            this.aircraftsView.Raise(a => a.OnAircraftsAddItem += null, aircraftEventArgs);

            var expectedId = 1;

            this.aircraftsServices.Verify(a => a.AddAircraft(
                It.Is<Aircraft>(ar => ar.Model == model && ar.TotalSeats == totalSeats &&
                    ar.AircraftManufacturerId == manufacturerId)),
                Times.Once);

            Assert.AreEqual(expectedId, aircraftEventArgs.Id);
        }

        [TestMethod]
        public void GeDataShouldAddManufacturersToViewModelWhenOnGetDataEventIsRaised()
        {
            this.aircraftsView.Raise(a => a.OnAircraftManufacturersGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.aircraftsView.Object.Model.AircraftManufacturers.ToList().Any());
            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.AircraftManufacturers.ToList(),
                this.aircraftsView.Object.Model.AircraftManufacturers.ToList());
        }
    }
}