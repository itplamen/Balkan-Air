namespace BalkanAir.Api.Tests.TestObjects
{
    using System.Linq;
    using System.Collections.Generic;

    using Moq;

    using Data.Models;
    using Services.Data.Contracts;
    using Web.Areas.Api.Models.Airports;

    public static class TestObjectFactory
    {
        private const int CORRECT_ID = 1;
        private const int NOT_FOUND_ID = 2;

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
