namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class AircraftsServicesTests
    {
        private IAircraftsServices aircraftsServices;

        private InMemoryRepository<Aircraft> aircraftsRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.aircraftsRepository = TestObjectFactory.GetAircraftsRepository();
            this.aircraftsServices = new AircraftsServices(this.aircraftsRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidAircraft()
        {
            var result = this.aircraftsServices.AddAircraft(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.aircraftsServices.AddAircraft(new Aircraft()
            {
                Model = "Test Aircraft",
                TotalSeats = 1,
                AircraftManufacturerId = 1
            });

            Assert.AreEqual(1, this.aircraftsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateAircraftToDatabase()
        {
            var result = this.aircraftsServices.AddAircraft(new Aircraft()
            {
                Model = "Test Aircraft",
                TotalSeats = 1,
                AircraftManufacturerId = 1
            });

            var addedAircraft = this.aircraftsRepository.All()
                .FirstOrDefault(a => a.Model == "Test Aircraft");

            Assert.IsNotNull(addedAircraft);
            Assert.AreEqual("Test Aircraft", addedAircraft.Model);
            Assert.AreEqual(1, addedAircraft.TotalSeats);
            Assert.AreEqual(1, addedAircraft.AircraftManufacturerId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftsServices.GetAircraft(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnAircraft()
        {
            var result = this.aircraftsServices.GetAircraft(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllAircraftsInDatabase()
        {
            var result = this.aircraftsServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftsServices.UpdateAircraft(-1, new Aircraft());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidAircraft()
        {
            var result = this.aircraftsServices.UpdateAircraft(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.aircraftsServices.UpdateAircraft(1, new Aircraft()
            {
                Model = "Test Aircraft",
                TotalSeats = 1,
                AircraftManufacturerId = 1
            });

            Assert.AreEqual(1, this.aircraftsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedAircraft()
        {
            var result = this.aircraftsServices.UpdateAircraft(1, new Aircraft());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditAircraft()
        {
            var result = this.aircraftsServices.UpdateAircraft(1, new Aircraft()
            {
                Model = "Test Aircraft",
                TotalSeats = 1,
                AircraftManufacturerId = 1
            });

            Assert.AreEqual("Test Aircraft", result.Model);
            Assert.AreEqual(1, result.TotalSeats);
            Assert.AreEqual(1, result.AircraftManufacturerId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftsServices.DeleteAircraft(-1);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.aircraftsServices.DeleteAircraft(1);

            Assert.AreEqual(1, this.aircraftsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullAircraft()
        {
            var result = this.aircraftsServices.DeleteAircraft(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var result = this.aircraftsServices.DeleteAircraft(1);

            Assert.IsTrue(result.IsDeleted);
        }
    }
}
