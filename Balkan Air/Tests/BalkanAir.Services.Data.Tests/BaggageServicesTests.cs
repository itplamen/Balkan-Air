namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class BaggageServicesTests
    {
        private IBaggageServices baggageServices;

        private InMemoryRepository<Baggage> baggageRepository;

        private Baggage bag;

        [TestInitialize]
        public void TestInitialize()
        {
            this.baggageRepository = TestObjectFactoryRepositories.GetBaggageRepository();
            this.baggageServices = new BaggageServices(this.baggageRepository);
            this.bag = new Baggage()
            {
                Type = BaggageType.Cabin,
                Price = 1m,
                BookingId = 1
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidBag()
        {
            var result = this.baggageServices.AddBaggage(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.baggageServices.AddBaggage(this.bag);

            Assert.AreEqual(1, this.baggageRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateBagToDatabase()
        {
            var result = this.baggageServices.AddBaggage(this.bag);

            var addedBag = this.baggageRepository.All()
                .FirstOrDefault(b => b.Size == this.bag.Size);

            Assert.IsNotNull(addedBag);
            Assert.AreEqual(this.bag.Type, addedBag.Type);
            Assert.AreEqual(this.bag.Price, addedBag.Price);
            Assert.AreEqual(this.bag.Size, addedBag.Size);
            Assert.AreEqual(this.bag.BookingId, addedBag.BookingId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.baggageServices.GetBaggage(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnBag()
        {
            var result = this.baggageServices.GetBaggage(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllBagsInDatabase()
        {
            var result = this.baggageServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.baggageServices.UpdateBaggage(-1, new Baggage());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidBag()
        {
            var result = this.baggageServices.UpdateBaggage(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.baggageServices.UpdateBaggage(1, this.bag);

            Assert.AreEqual(1, this.baggageRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedBag()
        {
            var result = this.baggageServices.UpdateBaggage(1, new Baggage());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditBag()
        {
            var result = this.baggageServices.UpdateBaggage(1, this.bag);
            
            Assert.AreEqual(this.bag.Type, result.Type);
            Assert.AreEqual(this.bag.Price, result.Price);
            Assert.AreEqual(this.bag.Size, result.Size);
            Assert.AreEqual(this.bag.BookingId, result.BookingId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.baggageServices.DeleteBaggage(-1);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.baggageServices.DeleteBaggage(1);

            Assert.AreEqual(1, this.baggageRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullBag()
        {
            var result = this.baggageServices.DeleteBaggage(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldSetIsDeletedProperyToTrue()
        {
            var result = this.baggageServices.DeleteBaggage(1);

            Assert.IsTrue(result.IsDeleted);
        }
    }
}
