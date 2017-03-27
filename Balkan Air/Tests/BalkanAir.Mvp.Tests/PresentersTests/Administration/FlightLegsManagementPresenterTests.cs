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
    public class FlightLegsManagementPresenterTests
    {
        private Mock<IFlightLegsManagementView> flightLegsView;
        private Mock<IFlightLegsServices> flightLegsServices;
        private Mock<IAirportsServices> airportsServices;
        private Mock<IFlightsServices> flightsServices;
        private Mock<IRoutesServices> routesServices;
        private Mock<ILegInstancesServices> legInstancesServices;
        private FlightLegsManagementPresenter presenter;

        [TestInitialize]
        public void TestInitialize()
        {
            this.flightLegsView = TestObjectFactoryViews.GetFlightLegsManagementView();
            this.flightLegsServices = TestObjectFactoryServices.GetFlightLegsServices();
            this.airportsServices = TestObjectFactoryServices.GetAirportsServices();
            this.flightsServices = TestObjectFactoryServices.GetFlightsServices();
            this.routesServices = TestObjectFactoryServices.GetRoutesServices();
            this.legInstancesServices = TestObjectFactoryServices.GetLegInstancesServices();

            this.presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object,
                this.flightLegsServices.Object, this.airportsServices.Object,
                this.flightsServices.Object, this.routesServices.Object,
                this.legInstancesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenFlightLegsServicesIsNull()
        {
            var presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object, null, 
                this.airportsServices.Object, this.flightsServices.Object, this.routesServices.Object, 
                this.legInstancesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenAirportsServicesIsNull()
        {
            var presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object, 
                this.flightLegsServices.Object, null, this.flightsServices.Object, 
                this.routesServices.Object, this.legInstancesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenFlightsServicesIsNull()
        {
            var presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object,
                this.flightLegsServices.Object, this.airportsServices.Object, null,
                this.routesServices.Object, this.legInstancesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenRoutesServicesIsNull()
        {
            var presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object,
                this.flightLegsServices.Object, this.airportsServices.Object, 
                this.flightsServices.Object, null, this.legInstancesServices.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorShouldThrowExceptionWhenLegInstancesServicesIsNull()
        {
            var presenter = new FlightLegsManagementPresenter(this.flightLegsView.Object,
                this.flightLegsServices.Object, this.airportsServices.Object,
                this.flightsServices.Object, this.routesServices.Object, null);
        }

        [TestMethod]
        public void GetDataShouldAddFlightLegsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.flightLegsView.Raise(f => f.OnFlightLegsGetData += null, EventArgs.Empty);

            CollectionAssert.AreEquivalent(TestObjectFactoryDataModels.FlightLegs.ToList(),
                this.flightLegsView.Object.Model.FlightLegs.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null, It.Is<FlightLegsManagementEventArgs>(null));
        }

        [TestMethod]
        public void UpdateItemShouldAddModelErrorToModelStateWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidId = -1;

            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null, new FlightLegsManagementEventArgs() { Id = invalidId });

            string expectedError = string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, invalidId);

            Assert.AreEqual(1, this.flightLegsView.Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors.Count);
            Assert.AreEqual(expectedError, this.flightLegsView.Object.ModelState[ErrorMessages.MODEL_ERROR_KEY].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void UpdateItemShouldNotPerformTryUpdateModelWhenItemIsNotFound()
        {
            var invalidId = -1;

            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null, new FlightLegsManagementEventArgs() { Id = invalidId });

            this.flightLegsView.Verify(f => f.TryUpdateModel(It.IsAny<FlightLeg>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldPerformTryUpdateModelWhenItemIsFound()
        {
            var validId = 1;
            var departureAirportId = 1;
            var arrivalAirportId = 1;
            var flightId = 1;
            var routeId = 1;

            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null,
                new FlightLegsManagementEventArgs()
                {
                    Id = validId,
                    DepartureAirportId = departureAirportId,
                    ArrivalAirportId = arrivalAirportId,
                    FlightId = flightId,
                    RouteId = routeId
                });

            this.flightLegsView.Verify(f => f.TryUpdateModel(
                It.Is<FlightLeg>(m => m.Id == validId && m.DepartureAirportId == departureAirportId &&
                    m.ArrivalAirportId == arrivalAirportId && m.FlightId == flightId && 
                    m.RouteId == routeId)),
                Times.Once);
        }

        [TestMethod]
        public void UpdateItemShouldNotCallUpdateFlightLegWhenItemIsFoundAndModelStateIsInvalid()
        {
            TestObjectFactoryViews.ModelStateDictionary.AddModelError("test key", "test error message");

            var validId = 1;

            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null, new FlightLegsManagementEventArgs() { Id = validId });

            this.flightLegsServices.Verify(f => f.UpdateFlightLeg(validId, It.IsAny<FlightLeg>()), Times.Never);
        }

        [TestMethod]
        public void UpdateItemShouldCallUpdateFlightLegWhenItemIsFoundAndModelStateIsValid()
        {
            var validId = 1;
            var departureAirportId = 1;
            var arrivalAirportId = 1;
            var flightId = 1;
            var routeId = 1;

            this.flightLegsView.Raise(f => f.OnFlightLegsUpdateItem += null,
                new FlightLegsManagementEventArgs()
                {
                    Id = validId,
                    DepartureAirportId = departureAirportId,
                    ArrivalAirportId = arrivalAirportId,
                    FlightId = flightId,
                    RouteId = routeId
                });

            this.flightLegsServices.Verify(f => f.UpdateFlightLeg(
                validId,
                It.Is<FlightLeg>(m => m.Id == validId && m.DepartureAirportId == departureAirportId &&
                    m.ArrivalAirportId == arrivalAirportId && m.FlightId == flightId &&
                    m.RouteId == routeId)),
                Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.flightLegsView.Raise(f => f.OnFlightLegsDeleteItem += null, It.Is<FlightLegsManagementEventArgs>(null));
        }

        [TestMethod]
        public void DeleteItemShouldDeleteFlightLegWhenOnDeleteItemEventIsRaised()
        {
            var validId = 1;
            this.flightLegsView.Raise(f => f.OnFlightLegsDeleteItem += null, new FlightLegsManagementEventArgs() { Id = validId });

            this.flightLegsServices.Verify(f => f.DeleteFlightLeg(validId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.flightLegsView.Raise(f => f.OnFlightLegsAddItem += null, It.Is<FlightLegsManagementEventArgs>(null));
        }

        [TestMethod]
        public void AddItemShouldAddFlightLegAndReturnIdWhenOnAddItemEventIsRaised()
        {
            var departureAirportId = 1;
            var scheduledDepartureDateTime = new DateTime(2017, 1, 1);
            var arrivalAirportId = 1;
            var scheduledArrivalDateTime = new DateTime(2017, 1, 1);
            var flightId = 1;
            var routeId = 1;
            
            var flightLegEventArgs = new FlightLegsManagementEventArgs()
            {
                DepartureAirportId = departureAirportId,
                ScheduledDepartureDateTime = scheduledDepartureDateTime,
                ScheduledArrivalDateTime = scheduledArrivalDateTime,
                ArrivalAirportId = arrivalAirportId,
                FlightId = flightId,
                RouteId = routeId
            };

            this.flightLegsView.Raise(f => f.OnFlightLegsAddItem += null, flightLegEventArgs);

            var expectedId = 1;

            this.flightLegsServices.Verify(f => f.AddFlightLeg(
                It.Is<FlightLeg>(m => m.DepartureAirportId == departureAirportId &&
                    m.ArrivalAirportId == arrivalAirportId && m.FlightId == flightId &&
                    m.RouteId == routeId)),
                Times.Once);

            Assert.AreEqual(expectedId, flightLegEventArgs.Id);
        }

        [TestMethod]
        public void GeDataShouldAddAirportsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.flightLegsView.Raise(a => a.OnAirportsGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.flightLegsView.Object.Model.Airports.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.Airports.ToList(),
                this.flightLegsView.Object.Model.Airports.ToList());
        }

        [TestMethod]
        public void GeDataShouldAddFlightsToViewModelWhenOnGetDataEventIsRaised()
        {
            this.flightLegsView.Raise(f => f.OnFlightsGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.flightLegsView.Object.Model.Flights.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.Flights.ToList(), this.flightLegsView.Object.Model.Flights.ToList());
        }

        [TestMethod]
        public void GeDataShouldAddRoutesToViewModelWhenOnGetDataEventIsRaised()
        {
            this.flightLegsView.Raise(r => r.OnRoutesGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.flightLegsView.Object.Model.Routes.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.Routes.ToList(), this.flightLegsView.Object.Model.Routes.ToList());
        }

        [TestMethod]
        public void GeDataShouldAddLegInstancesToViewModelWhenOnGetDataEventIsRaised()
        {
            this.flightLegsView.Raise(f => f.OnLegInstancesGetData += null, EventArgs.Empty);

            Assert.IsTrue(this.flightLegsView.Object.Model.LegInstances.ToList().Any());
            Enumerable.SequenceEqual(TestObjectFactoryDataModels.LegInstances.ToList(),
                this.flightLegsView.Object.Model.LegInstances.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAirportItemShouldThrowExceptionWhenEventArgsIsNull()
        {
            this.flightLegsView.Raise(f => f.OnAirportGetItem += null, It.Is<FlightLegsManagementEventArgs>(null));
        }

        [TestMethod]
        public void GetAirportItemShouldAddErrorMessageToModelWhenIdIsInvalidAndItemIsNotFound()
        {
            var invalidAirportId = -1;

            this.flightLegsView.Raise(a => a.OnAirportGetItem += null, 
                new FlightLegsManagementEventArgs() { AirportId = invalidAirportId });

            Assert.AreEqual(ErrorMessages.AIRPORT_NOT_FOUND, this.flightLegsView.Object.Model.AirportInfo);
        }
    }
}
