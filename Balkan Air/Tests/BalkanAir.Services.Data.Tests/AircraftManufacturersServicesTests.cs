namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class AircraftManufacturersServicesTests
    {
        private IAircraftManufacturersServices aircraftManufacturersServices;

        private InMemoryRepository<AircraftManufacturer> manufacturersRepository;

        private AircraftManufacturer aircraftManufacturer;

        [TestInitialize]
        public void TestInitialize()
        {
            this.manufacturersRepository = TestObjectFactoryRepositories.GetAircraftManufacturersRepository();
            this.aircraftManufacturersServices = new AircraftManufacturersServices(this.manufacturersRepository);
            this.aircraftManufacturer = new AircraftManufacturer()
            {
                Name = "Test Manufacturer"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidManufacturer()
        {
            var result = this.aircraftManufacturersServices.AddManufacturer(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.aircraftManufacturersServices.AddManufacturer(this.aircraftManufacturer);

            Assert.AreEqual(1, this.manufacturersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateManufacturerToDatabase()
        {
            var result = this.aircraftManufacturersServices.AddManufacturer(this.aircraftManufacturer);

            var addedManufacturer = this.manufacturersRepository.All()
                .FirstOrDefault(a => a.Name == this.aircraftManufacturer.Name);

            Assert.IsNotNull(addedManufacturer);
            Assert.AreEqual(this.aircraftManufacturer.Name, addedManufacturer.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftManufacturersServices.GetManufacturer(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnManufacturer()
        {
            var result = this.aircraftManufacturersServices.GetManufacturer(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllManufacturersInDatabase()
        {
            var result = this.aircraftManufacturersServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftManufacturersServices.UpdateManufacturer(-1, new AircraftManufacturer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidManufacturer()
        {
            var result = this.aircraftManufacturersServices.UpdateManufacturer(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.aircraftManufacturersServices.UpdateManufacturer(1, this.aircraftManufacturer);

            Assert.AreEqual(1, this.manufacturersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedManufacturer()
        {
            var result = this.aircraftManufacturersServices.UpdateManufacturer(1, new AircraftManufacturer());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditManufacturer()
        {
            var result = this.aircraftManufacturersServices.UpdateManufacturer(1, this.aircraftManufacturer);

            Assert.AreEqual(this.aircraftManufacturer.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.aircraftManufacturersServices.DeleteManufacturer(-1);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.aircraftManufacturersServices.DeleteManufacturer(1);

            Assert.AreEqual(1, this.manufacturersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullManufacturer()
        {
            var result = this.aircraftManufacturersServices.DeleteManufacturer(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var result = this.aircraftManufacturersServices.DeleteManufacturer(1);

            Assert.IsTrue(result.IsDeleted);
        }
    }
}
