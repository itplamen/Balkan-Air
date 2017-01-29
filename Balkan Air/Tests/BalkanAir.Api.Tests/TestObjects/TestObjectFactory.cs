namespace BalkanAir.Api.Tests.TestObjects
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using Data.Models;
    using Services.Data.Contracts;
    using Web.Areas.Api.Models.AircraftManufacturers;
    using Web.Areas.Api.Models.Aircrafts;
    using Web.Areas.Api.Models.Airports;

    public static class TestObjectFactory
    {
        private const int CORRECT_ID = 1;
        private const int NOT_FOUND_ID = 2;

        private static IQueryable<AircraftManufacturer> aircraftManufacturers = new List<AircraftManufacturer>()
        {
            new AircraftManufacturer()
            {
                Id = 1,
                Name = "Manufacturer Test"
            }
        }.AsQueryable();

        private static AircraftManufacturer nullableAircraftManufacturer = null;

        private static IQueryable<Aircraft> aircrafts = new List<Aircraft>()
        {
            new Aircraft()
            {
                Id = 1,
                Model = "Aircraft Model Test",
                TotalSeats = 1
            }
        }.AsQueryable();

        private static Aircraft nullabelAircraft = null;

        private static IQueryable<Airport> airports = new List<Airport>()
        {
            new Airport()
            {   Id = 1,
                Name = "Test Name",
                Abbreviation = "ABC",
                CountryId = 1
            }
        }.AsQueryable();

        private static Airport nullableAirport = null;

        public static IAircraftManufacturersServices GetAircraftManufacturersServices()
        {
            var aircraftManufacturersServices = new Mock<IAircraftManufacturersServices>();

            aircraftManufacturersServices.Setup(am => am.AddManufacturer(
                    It.IsAny<AircraftManufacturer>()))
                .Returns(1);

            aircraftManufacturersServices.Setup(am => am.GetAll())
                .Returns(aircraftManufacturers);

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns(nullableAircraftManufacturer);

            aircraftManufacturersServices.Setup(am => am.UpdateManufacturer(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<AircraftManufacturer>()))
                .Returns(new AircraftManufacturer() { Id = 1 });

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableAircraftManufacturer);

            aircraftManufacturersServices.Setup(am => am.DeleteManufacturer(
                    It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new AircraftManufacturer() { Id = 1 });

            return aircraftManufacturersServices.Object;
        }

        public static IAircraftsServices GetAircraftsServices()
        {
            var aircraftsServices = new Mock<IAircraftsServices>();

            aircraftsServices.Setup(a => a.AddAircraft(
                    It.IsAny<Aircraft>()))
                .Returns(1);

            aircraftsServices.Setup(a => a.GetAll())
                .Returns(aircrafts);

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Aircraft>()))
                .Returns(nullabelAircraft);

            aircraftsServices.Setup(a => a.UpdateAircraft(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<Aircraft>()))
                .Returns(new Aircraft() { Id = 1 });

            aircraftsServices.Setup(a => a.DeleteAircraft(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullabelAircraft);

            aircraftsServices.Setup(a => a.DeleteAircraft(
                It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Aircraft() { Id = 1 });

            return aircraftsServices.Object;
        }

        public static IAirportsServices GetAirportsServices()
        {
            var airportsServices = new Mock<IAirportsServices>();

            airportsServices.Setup(a => a.AddAirport(
                    It.IsAny<Airport>()))
                .Returns(1);

            airportsServices.Setup(a => a.GetAll())
                .Returns(airports);

            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i >= NOT_FOUND_ID),
                    It.IsAny<Airport>()))
                .Returns(nullableAirport);
            
            airportsServices.Setup(a => a.UpdateAirport(
                    It.Is<int>(i => i == CORRECT_ID),
                    It.IsAny<Airport>()))
                .Returns(new Airport() { Id = 1 });

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i >= NOT_FOUND_ID)))
                .Returns(nullableAirport);

            airportsServices.Setup(a => a.DeleteAirport(
                    It.Is<int>(i => i == CORRECT_ID)))
                .Returns(new Airport() { Id = 1 });

            return airportsServices.Object;
        }

        public static AircraftManufacturerRequestModel GetInvalidAircraftManufacturerRequestModel()
        {
            return new AircraftManufacturerRequestModel() { Name = "TOOOOOOOOOO LOOOOONG NAAAAAAAAME TEST" };
        }

        public static AircraftManufacturerRequestModel GetValidAircraftManufacturerRequestModel()
        {
            return new AircraftManufacturerRequestModel() { Name = "Manufacturer Test" };
        }

        public static UpdateAircraftManufacturerRequestModel GetInvalidUpdateAircraftManufacturerRequestModel()
        {
            return new UpdateAircraftManufacturerRequestModel() { Name = "OOOOOOOOOO LOOOOONG NAAAAAAAAME TEST" };
        }

        public static UpdateAircraftManufacturerRequestModel GetValidUpdateAircraftManufacturerRequestModel()
        {
            return new UpdateAircraftManufacturerRequestModel()
            {
                Id = 1,
                Name = "Manufacturer Test",
                IsDeleted = false
            };
        }

        public static AircraftRequesModel GetInvalidAircraftRequesModel()
        {
            return new AircraftRequesModel() { Model = "Model Test" };
        }

        public static AircraftRequesModel GetValidAircraftRequesModel()
        {
            return new AircraftRequesModel()
            {
                Model = "Model Test",
                TotalSeats = 180,
                AircraftManufacturerId = 1
            };
        }

        public static UpdateAircraftRequestModel GetInvalidUpdateAircraftRequestModel()
        {
            return new UpdateAircraftRequestModel() { Model = "Model Test" };
        }

        public static UpdateAircraftRequestModel GetValidUpdateAircraftRequestModel()
        {
            return new UpdateAircraftRequestModel()
            {
                Id = 1,
                Model = "Model Test",
                TotalSeats = 180,
                IsDeleted = false,
                AircraftManufacturerId = 1
            };
        }

        public static AirportRequestModel GetInvalidAirportRequesModel()
        {
            return new AirportRequestModel() { Abbreviation = "abc" };
        }

        public static AirportRequestModel GetValidAirportRequesModel()
        {
            return new AirportRequestModel()
            {
                Name = "Airport Test",
                Abbreviation = "ABC",
                CountryId = 1
            };
        }

        public static UpdateAirportRequestModel GetInvalidUpdateAirportRequestModel()
        {
            return new UpdateAirportRequestModel() { Abbreviation = "abc" };
        }

        public static UpdateAirportRequestModel GetValidUpdateAirportRequestModel()
        {
            return new UpdateAirportRequestModel()
            {
                Id = 1,
                Name = "Test Name Update",
                Abbreviation = "ABC",
                IsDeleted = false,
                CountryId = 1
            };
        }
    } 
}
