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

        private InMemoryRepository<Airport> airportsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.airportsRepository = TestObjectFactory.GetAirportsRepository();
            this.airportsServices = new AirportsServices(this.airportsRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidAirport()
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

            Assert.AreEqual(1, this.airportsRepository.NumberOfSaves);
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

            var addedAirport = this.airportsRepository.All()
                .FirstOrDefault(a => a.Name == "Test Airport");

            Assert.IsNotNull(addedAirport);
            Assert.AreEqual("Test Airport", addedAirport.Name);
            Assert.AreEqual("123", addedAirport.Abbreviation);
            Assert.AreEqual(1, addedAirport.CountryId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.airportsServices.GetAirport(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnAirport()
        {
            var result = this.airportsServices.GetAirport(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllAirportsInDatabase()
        {
            var result = this.airportsServices.GetAll();

            Assert.IsNotNull(result);
        } 

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.airportsServices.UpdateAirport(-1, new Airport());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidAirport()
        {
            var result = this.airportsServices.UpdateAirport(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.airportsServices.UpdateAirport(1, new Airport()
            {
                Name = "Test Airport",
                Abbreviation = "123",
                CountryId = 1
            });

            Assert.AreEqual(1, this.airportsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedAirport()
        {
            var result = this.airportsServices.UpdateAirport(1, new Airport());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditAirport()
        {
            var result = this.airportsServices.UpdateAirport(1, new Airport()
            {
                Name = "Test Airport",
                Abbreviation = "123",
                CountryId = 1
            });

            Assert.AreEqual("Test Airport", result.Name);
            Assert.AreEqual("123", result.Abbreviation);
            Assert.AreEqual(1, result.CountryId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.airportsServices.DeleteAirport(-1);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.airportsServices.DeleteAirport(1);

            Assert.AreEqual(1, this.airportsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullAirport()
        {
            var result = this.airportsServices.DeleteAirport(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var result = this.airportsServices.DeleteAirport(1);

            Assert.IsTrue(result.IsDeleted);
        }
    }
}
