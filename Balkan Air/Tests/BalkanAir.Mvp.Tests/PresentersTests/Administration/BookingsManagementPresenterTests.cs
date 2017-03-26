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
    public class BookingsManagementPresenterTests
    {
        private Mock<IBookingsManagementView> bookingsView;
        private Mock<IBookingsServices> bookingsServices;
        private BookingsManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.bookingsView = TestObjectFactoryViews.GetBookingsManagementView();
            this.bookingsServices = TestObjectFactoryServices.GetBookingsServices();

            this.presenter = new BookingsManagementPresenter(this.bookingsView.Object, 
                this.bookingsServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenBookingsServicesIsNull()
        {
            var presenter = new BookingsManagementPresenter(this.bookingsView.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddBookingsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.bookingsView.Raise(b => b.OnBookingsGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.Bookings.ToList(),
                this.bookingsView.Object.Model.Bookings.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                It.Is<BookingsManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                new BookingsManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.bookingsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);

            Assert.AreEqual(expectedError, this.bookingsView
                .Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                new BookingsManagementEventArgs() { Id = invalidId });

            this.bookingsView.Verify(b => b.TryUpdateModel(It.IsAny<Booking>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;

            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                new BookingsManagementEventArgs() { Id = validId });

            this.bookingsView.Verify(b => b.TryUpdateModel(It.Is<Booking>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateBookingWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                new BookingsManagementEventArgs() { Id = validId });

            this.bookingsServices.Verify(b => b.UpdateBooking(validId, It.IsAny<Booking>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateBookingWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;

            this.bookingsView.Raise(b => b.OnBookingsUpdateItem += null,
                new BookingsManagementEventArgs() { Id = validId });

            this.bookingsServices
                .Verify(b => b.UpdateBooking(validId, It.Is<Booking>(m => m.Id == validId)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.bookingsView.Raise(b => b.OnBookingsDeleteItem += null,
                It.Is<BookingsManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteBookingWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.bookingsView.Raise(b => b.OnBookingsDeleteItem += null,
                new BookingsManagementEventArgs() { Id = validId });

            this.bookingsServices.Verify(b => b.DeleteBooking(validId), Times.Once);
        }
    }
}
