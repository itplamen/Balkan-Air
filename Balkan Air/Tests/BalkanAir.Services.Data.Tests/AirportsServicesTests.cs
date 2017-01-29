namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class AirportsServicesTests
    {
        private IAirportsServices airportsServices;

        private InMemoryRepository<Airport> airportRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            this.airportRepo = TestObjectFactory.GetAirportsRepository();
            this.airportsServices = new AirportsServices(this.airportRepo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithNotFoundAirport()
        {
            var result = this.airportsServices.AddAirport(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.airportsServices.AddAirport(new Airport()
            {
                Name = "Test Airport",
                Abbreviation = "123",
                CountryId = 1
            });

            Assert.AreEqual(1, this.airportRepo.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateAirportToDatabase()
        {
            var result = this.airportsServices.AddAirport(new Airport()
            {
                Name = "Test Airport",
                Abbreviation = "123",
                CountryId = 1
            });

            var addedAirport = this.airportRepo.All()
                .FirstOrDefault(a => a.Name == "Test Airport");

            Assert.IsNotNull(addedAirport);
            Assert.AreEqual("Test Airport", addedAirport.Name);
            Assert.AreEqual("123", addedAirport.Abbreviation);
            Assert.AreEqual(1, addedAirport.CountryId);
        }
    }
}
