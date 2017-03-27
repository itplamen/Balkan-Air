namespace BalkanAir.Api.Tests.TestObjects
{
    using System;

    using Models.AircraftManufacturers;
    using Models.Aircrafts;
    using Models.Airports;
    using Models.Categories;
    using Models.Countries;
    using Models.Fares;
    using Models.Flights;
    using Models.News;

    public static class TestObjectFactoryDataTransferModels
    {
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
            return new UpdateAircraftManufacturerRequestModel() { Name = "TOOOOOOOOOO LOOOOONG NAAAAAAAAME TEST" };
        }

        public static UpdateAircraftManufacturerRequestModel GetValidUpdateAircraftManufacturerRequestModel()
        {
            return new UpdateAircraftManufacturerRequestModel()
            {
                Id = 1,
                Name = "Manufacturer Test",
                IsDeleted = true
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
                IsDeleted = true,
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

        // Return category without name, so that the model can be invalid.
        public static CategoryRequestModel GetInvalidCategoryRequesModel()
        {
            return new CategoryRequestModel();
        }

        public static CategoryRequestModel GetValidCategoryRequesModel()
        {
            return new CategoryRequestModel() { Name = "Category Test" };
        }

        public static UpdateCategoryRequestModel GetInvalidUpdateCategoryRequestModel()
        {
            return new UpdateCategoryRequestModel() { IsDeleted = false };
        }

        public static UpdateCategoryRequestModel GetValidUpdateCategoryRequestModel()
        {
            return new UpdateCategoryRequestModel()
            {
                Id = 1,
                Name = "Category Test",
                IsDeleted = true
            };
        }

        public static CountryRequestModel GetInvalidCountryRequesModel()
        {
            return new CountryRequestModel() { Abbreviation = "PP" };
        }

        public static CountryRequestModel GetValidCountryRequesModel()
        {
            return new CountryRequestModel()
            {
                Name = "Country Test",
                Abbreviation = "CT"
            };
        }

        public static UpdateCountryRequestModel GetInvalidUpdateCountryRequestModel()
        {
            return new UpdateCountryRequestModel() { Abbreviation = "PP" };
        }

        public static UpdateCountryRequestModel GetValidUpdateCountryRequestModel()
        {
            return new UpdateCountryRequestModel()
            {
                Id = 1,
                Name = "Country Test",
                Abbreviation = "CT",
                IsDeleted = true
            };
        }

        public static FareRequestModel GetInvalidFareRequestModel()
        {
            return new FareRequestModel() { Price = -1m, RouteId = -1 };
        }

        public static FareRequestModel GetValidFareRequestModel()
        {
            return new FareRequestModel()
            {
                Price = 1m,
                RouteId = 1
            };
        }

        public static UpdateFareRequestModel GetInvalidUpdateFareRequestModel()
        {
            return new UpdateFareRequestModel() { Price = -1m };
        }

        public static UpdateFareRequestModel GetValidUpdateFareRequestModel()
        {
            return new UpdateFareRequestModel()
            {
                Id = 1,
                Price = 1m,
                RouteId = 1,
                IsDeleted = true
            };
        }

        public static FlightRequestModel GetInvalidFlightRequestModel()
        {
            return new FlightRequestModel() { Price = -1m };
        }

        public static FlightRequestModel GetValidFlightRequestModel()
        {
            return new FlightRequestModel()
            {
                Departure = DateTime.Now,
                Arrival = DateTime.Now,
                Price = 1m,
                FlightLegId = 1,
                FlightStatusId = 1,
                AircraftId = 1
            };
        }

        public static UpdateFlightRequestModel GetInvalidUpdateFlightRequestModel()
        {
            return new UpdateFlightRequestModel() { Price = -1m };
        }

        public static UpdateFlightRequestModel GetValidUpdateFlightRequestModel()
        {
            return new UpdateFlightRequestModel()
            {
                Id = 1,
                Departure = DateTime.Now,
                Arrival = DateTime.Now,
                Price = 1m,
                FlightLegId = 1,
                FlightStatusId = 1,
                AircraftId = 1,
                IsDeleted = true
            };
        }

        public static NewsRequestModel GetInvalidNewsRequestModel()
        {
            return new NewsRequestModel() { Title = null };
        }

        public static NewsRequestModel GetValidNewsRequestModel()
        {
            return new NewsRequestModel()
            {
                Title = "Test News Title",
                Content = "Test News Content",
                CategoryId = 1
            };
        }

        public static UpdateNewsRequestModel GetInvalidUpdateNewsRequestModel()
        {
            return new UpdateNewsRequestModel() { Title = null };
        }

        public static UpdateNewsRequestModel GetValidUpdateNewsRequestModel()
        {
            return new UpdateNewsRequestModel()
            {
                Id = 1,
                Title = "Test News Title",
                Content = "Test News Content",
                CategoryId = 1,
                IsDeleted = true
            };
        }
    }
}
