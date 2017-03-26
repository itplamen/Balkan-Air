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
    public class BaggageManagementPresenterTests
    {
        private Mock<IBaggageManagementView> baggageView;
        private Mock<IBaggageServices> baggageServices;
        private Mock<IBookingsServices> bookingsServices;
        private BaggageManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.baggageView = TestObjectFactoryViews.GetBaggageManagementView();
            this.baggageServices = TestObjectFactoryServices.GetBaggageServices();
            this.bookingsServices = TestObjectFactoryServices.GetBookingsServices();

            this.presenter = new BaggageManagementPresenter(this.baggageView.Object,
                this.baggageServices.Object, this.bookingsServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenBaggageServicesIsNull()
        {
            var presenter = new BaggageManagementPresenter(this.baggageView.Object,
                null, this.bookingsServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenBookingsServicesIsNull()
        {
            var presenter = new BaggageManagementPresenter(this.baggageView.Object,
                this.baggageServices.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddBaggageToViewModelWhenOnGetDataEventIsRaised()
        {
            this.baggageView.Raise(b => b.OnBaggageGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Baggage.ToList(),
                this.baggageView.Object.Model.Baggage.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null,
                It.Is<BaggageManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null, 
                new BaggageManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.baggageView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.baggageView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null, 
                new BaggageManagementEventArgs() { Id = invalidId });

            this.baggageView.Verify(b => b.TryUpdateModel(It.IsAny<Baggage>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;

            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null, 
                new BaggageManagementEventArgs() { Id = validId });

            this.baggageView.Verify(b => b.TryUpdateModel(It.Is<Baggage>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateBaggageWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null, 
                new BaggageManagementEventArgs() { Id = validId });

            this.baggageServices.Verify(b => b.UpdateBaggage(validId, It.IsAny<Baggage>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateBaggageWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;

            this.baggageView.Raise(b => b.OnBaggageUpdateItem += null, 
                new BaggageManagementEventArgs() { Id = validId });

            this.baggageServices.Verify(b => b.UpdateBaggage(validId, It.Is<Baggage>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.baggageView.Raise(b => b.OnBaggageDeleteItem += null, 
                It.Is<BaggageManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteBaggageWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.baggageView.Raise(b => b.OnBaggageDeleteItem += null, 
                new BaggageManagementEventArgs() { Id = validId });

            this.baggageServices.Verify(b => b.DeleteBaggage(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.baggageView.Raise(b => b.OnBaggageAddItem += null, 
                It.Is<BaggageManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddBaggageAndReturnIdWhenOnAddItemEventIsRaised()
        {
            var type = BaggageType.Cabin;
            var maxKilograms = 32;
            var size = "test size";
            var price = 1;
            var bookingId = 1;

            var bagEventArgs = new BaggageManagementEventArgs()
            {
                Type = type,
                MaxKilograms = maxKilograms,
                Size = size,
                Price = price,
                BookingId = bookingId
            };

            this.baggageView.Raise(b => b.OnBaggageAddItem += null, bagEventArgs);

            var expectedId = 1;

            this.baggageServices.Verify(b => b.AddBaggage(
                It.Is<Baggage>(m => m.Type == type && m.MaxKilograms == maxKilograms && 
                    m.Size == size && m.Price == price && m.BookingId == bookingId)),
                Times.Once);

            Assert.AreEqual(expectedId, bagEventArgs.Id);
        }

        [TestMethod]
        public void GeDataShouldAddBookingsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.baggageView.Raise(b => b.OnBookingsGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.baggageView.Object.Model.Bookings.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.Bookings.ToList(),
                this.baggageView.Object.Model.Bookings.ToList());
        }
    }
}
