namespace BalkanAir.Api.Tests.TestObjects
{
    using Moq;

    using Data.Models;
    using Services.Data.Contracts;
    using Web.Areas.Api.Models.Airports;
    using System.Linq;
    using System.Collections.Generic;

    public static class TestObjectFactory
    {
        private static IQueryable<Airport> airports = new List<Airport>()
        {
            new Airport()
            {   Id = 1,
                Name = "Test Name",
                Abbreviation = "ABC",
                CountryId = 1
            }
        }.AsQueryable();

        public static IAirportsServices GetAirportsServices()
        {
            var airportsServices = new Mock<IAirportsServices>();

            airportsServices.Setup(a => a.AddAirport(
                    It.IsAny<Airport>()))
                .Returns(1);

            airportsServices.Setup(a => a.GetAll())
                .Returns(airports);

            return airportsServices.Object;
        }

        public static AirportRequestModel GetInvalidModel()
        {
            return new AirportRequestModel() { Abbreviation = "abc" };
        }
    } 
}
