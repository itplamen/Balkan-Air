namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class UserNotificationsServicesTests
    {
        private IUserNotificationsServices userNotificationsServices;

        private InMemoryRepository<UserNotification> userNotificationsRepository;

        private UserNotification userNotification;

        [TestInitialize]
        public void TestInitialize()
        {
            this.userNotificationsRepository = TestObjectFactory.GetUserNotificationsRepository();
            this.userNotificationsServices = new UserNotificationsServices(this.userNotificationsRepository);
            this.userNotification = new UserNotification()
            {
                DateReceived = DateTime.Now,
                UserId = "1234 User Id",
                NotificationId = 1
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SendNotificationToSingleUserShouldThrowExceptionWithInvalidNotificationId()
        {
            this.userNotificationsServices.SendNotification(-1, "valid user id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationToSingleUserShouldThrowExceptionWithInvalidUserId()
        {
            this.userNotificationsServices.SendNotification(1, string.Empty);
        }

        [TestMethod]
        public void SendNotificationToSingleUserShouldInvokeSaveChanges()
        {
            this.userNotificationsServices.SendNotification(1, "user id");

            Assert.AreEqual(1, this.userNotificationsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void SendNotificationToSingleUserShouldPopulateNotificationToDatabase()
        {
            this.userNotificationsServices.SendNotification(1, "user id");

            var sentNotification = this.userNotificationsRepository.All()
                .FirstOrDefault(un => un.UserId == "user id");

            Assert.IsNotNull(sentNotification);
            Assert.AreEqual(1, sentNotification.NotificationId);
            Assert.AreEqual("user id", sentNotification.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SendNotificationToManyUsersShouldThrowExceptionWithInvalidNotificationId()
        {
            this.userNotificationsServices.SendNotification(
                -1,
                new List<string>()
                {
                    "valid user id 1",
                    "valid user id 2"
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SendNotificationToManyUsersShouldThrowExceptionWithNullOrEmptyListOfUsers()
        {
            this.userNotificationsServices.SendNotification(1, new List<string>());
        }

        [TestMethod]
        public void SendNotificationToManyUsersShouldInvokeSaveChanges()
        {
            this.userNotificationsServices.SendNotification(1, new List<string>() { "valid user id" });

            Assert.AreEqual(1, this.userNotificationsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void SendNotificationToManyUsersShouldPopulateNotificationToDatabase()
        {
            this.userNotificationsServices.SendNotification(1, new List<string>() { "valid user id" });

            var sentNotification = this.userNotificationsRepository.All()
                .FirstOrDefault(un => un.UserId == "valid user id");

            Assert.IsNotNull(sentNotification);
            Assert.AreEqual(1, sentNotification.NotificationId);
            Assert.AreEqual("valid user id", sentNotification.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetNotificationAsReadShouldThrowExceptionWithInvalidNotificationId()
        {
            this.userNotificationsServices.SetNotificationAsRead(-1, "valid user id");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNotificationAsReadShouldThrowExceptionWithInvalidUserId()
        {
            this.userNotificationsServices.SetNotificationAsRead(1, string.Empty);
        }

        [TestMethod]
        public void SetNotificationAsReadShouldInvokeSaveChanges()
        {
            this.userNotificationsServices.SetNotificationAsRead(1, "user id");

            Assert.AreEqual(1, this.userNotificationsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void SetNotificationAsReadShouldUpdateIsReadAndDateReadProperties()
        {
            string userId = "valid user id";

            this.userNotificationsServices.SendNotification(1, userId);

            this.userNotificationsServices.SetNotificationAsRead(1, userId);

            var sentNotification = this.userNotificationsRepository.All()
                .FirstOrDefault(un => un.UserId == userId);

            Assert.IsNotNull(sentNotification);
            Assert.AreEqual(1, sentNotification.NotificationId);
            Assert.AreEqual(userId, sentNotification.UserId);
            Assert.IsTrue(sentNotification.IsRead);
            Assert.IsNotNull(sentNotification.DateRead);
            Assert.AreNotEqual(DateTime.MinValue, sentNotification.DateRead);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.userNotificationsServices.GetUserNotification(-1);
        }

        [TestMethod]
        public void GetByIdShouldReturnNotification()
        {
            var result = this.userNotificationsServices.GetUserNotification(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllShouldReturnAllNotificationsInDatabase()
        {
            var result = this.userNotificationsServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.userNotificationsServices.UpdateUserNotification(-1, new UserNotification());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidNotification()
        {
            var result = this.userNotificationsServices.UpdateUserNotification(1, null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.userNotificationsServices.UpdateUserNotification(1, this.userNotification);

            Assert.AreEqual(1, this.userNotificationsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedNotification()
        {
            var result = this.userNotificationsServices.UpdateUserNotification(1, this.userNotification);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditNotification()
        {
            var result = this.userNotificationsServices.UpdateUserNotification(1, this.userNotification);

            Assert.AreEqual(this.userNotification.DateReceived, result.DateReceived);
            Assert.AreEqual(this.userNotification.UserId, result.UserId);
            Assert.AreEqual(this.userNotification.NotificationId, result.NotificationId);
        }
    }
}
