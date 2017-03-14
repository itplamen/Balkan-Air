namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class CountriesServicesTests
    {
        private ICountriesServices countriesServices;

        private InMemoryRepository<Country> countriesRepository;

        private Country country;

        [TestInitialize]
        public void TestInitialize()
        {
            this.countriesRepository = TestObjectFactory.GetCountriesRepository();
            this.countriesServices = new CountriesServices(this.countriesRepository);
            this.country = new Country()
            {
                Name = "Test Country",
                Abbreviation = "AB"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidCountry()
        {
            var result = this.countriesServices.AddCountry(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.countriesServices.AddCountry(this.country);

            Assert.AreEqual(1, this.countriesRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateCountryToDatabase()
        {
            var result = this.countriesServices.AddCountry(this.country);

            var addedCountry = this.countriesRepository.All()
                .FirstOrDefault(a => a.Name == this.country.Name);

            Assert.IsNotNull(addedCountry);
            Assert.AreEqual(this.country.Name, addedCountry.Name);
            Assert.AreEqual(this.country.Abbreviation, addedCountry.Abbreviation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.countriesServices.GetCountry(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnCountry()
        {
            var result = this.countriesServices.GetCountry(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllCountriesInDatabase()
        {
            var result = this.countriesServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.countriesServices.UpdateCountry(-1, new Country());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidCountry()
        {
            var result = this.countriesServices.UpdateCountry(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.countriesServices.UpdateCountry(1, this.country);

            Assert.AreEqual(1, this.countriesRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedCountry()
        {
            var result = this.countriesServices.UpdateCountry(1, new Country());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditCountry()
        {
            var result = this.countriesServices.UpdateCountry(1, this.country);

            Assert.AreEqual(this.country.Name, result.Name);
            Assert.AreEqual(this.country.Abbreviation, result.Abbreviation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.countriesServices.DeleteCountry(-1);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.countriesServices.DeleteCountry(1);

            Assert.AreEqual(1, this.countriesRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullCountry()
        {
            var result = this.countriesServices.DeleteCountry(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var result = this.countriesServices.DeleteCountry(1);

            Assert.IsTrue(result.IsDeleted);
        }
    }
}
