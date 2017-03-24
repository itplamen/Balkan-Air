namespace BalkanAir.Services.Data.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using BalkanAir.Data.Models;
    using Contracts;
    using TestObjects;

    [TestClass]
    public class UsersServicesTests
    {
        private IUsersServices usersServices;

        private InMemoryRepository<User> usersRepository;

        private User user;

        [TestInitialize]
        public void TestInitialize()
        {
            this.usersRepository = TestObjectFactoryRepositories.GetUsersRepository();
            this.usersServices = new UsersServices(this.usersRepository);

            this.user = new User()
            {
                Email = "user_email@test.bg",
                UserName = "user_email@test.bg"
            };

            this.user.UserSettings.FirstName = "First Name Test";
            this.user.UserSettings.LastName = "Last Name Test";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldThrowExceptionWithInvalidUser()
        {
            var result = this.usersServices.AddUser(null);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.usersServices.AddUser(this.user);

            Assert.AreEqual(1, this.usersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateUserToDatabase()
        {
            var result = this.usersServices.AddUser(this.user);

            var addedUser = this.usersRepository.All()
                .FirstOrDefault(u => u.Email == this.user.Email);

            Assert.IsNotNull(addedUser);
            Assert.AreEqual(this.user.Email, addedUser.Email);
            Assert.AreEqual(this.user.UserName, addedUser.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByIdShouldThrowExceptionWithInvalidId()
        {
            var result = this.usersServices.GetUser(string.Empty);
        }

        [TestMethod]
        public void GetByIdShouldReturnUser()
        {
            var result = this.usersServices.GetUser("valid user id");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByEmailShouldThrowExceptionWithInvalidEmail()
        {
            var result = this.usersServices.GetUserByEmail(string.Empty);
        }

        [TestMethod]
        public void GetByEmailShouldReturnUser()
        {
            var addedUserId = this.usersServices.AddUser(this.user);

            var result = this.usersServices.GetUserByEmail(this.user.Email);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.user.Email, result.Email);
            Assert.AreEqual(this.user.UserName, result.UserName);
        }

        [TestMethod]
        public void GetAllShouldReturnAllUsersInDatabase()
        {
            var result = this.usersServices.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidId()
        {
            var result = this.usersServices.UpdateUser(string.Empty, new User());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateShouldThrowExceptionWithInvalidUser()
        {
            var result = this.usersServices.UpdateUser("valid user id", null);
        }

        [TestMethod]
        public void UpdateShouldInvokeSaveChanges()
        {
            var result = this.usersServices.UpdateUser("valid user id", this.user);

            Assert.AreEqual(1, this.usersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void UpdateShouldReturnEditedUser()
        {
            var result = this.usersServices.UpdateUser("valid user id", new User());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateShouldEditUser()
        {
            var result = this.usersServices.UpdateUser("valid user id", this.user);

            Assert.AreEqual(this.user.UserSettings.FirstName, result.UserSettings.FirstName);
            Assert.AreEqual(this.user.UserSettings.LastName, result.UserSettings.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteShouldThrowExceptionWithInvalidId()
        {
            var result = this.usersServices.DeleteUser(string.Empty);
        }

        [TestMethod]
        public void DeleteShouldInvokeSaveChanges()
        {
            var result = this.usersServices.DeleteUser("valid user id");

            Assert.AreEqual(1, this.usersRepository.NumberOfSaves);
        }

        [TestMethod]
        public void DeleteShouldReturnNotNullUser()
        {
            var result = this.usersServices.DeleteUser("valid user id");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteShouldUpdateIsDeletedAndDeletedOnProperties()
        {
            var result = this.usersServices.DeleteUser("valid user id");

            Assert.IsTrue(result.IsDeleted);
            Assert.IsNotNull(result.DeletedOn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UploadShouldThrowExceptionWithInvalidId()
        {
            this.usersServices.Upload(string.Empty, new byte[] { 1, 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UploadShouldThrowExceptionWithInvalidImageToUpload()
        {
            this.usersServices.Upload("valid user id", null);
        }
        
        [TestMethod]
        public void UploadShouldSetProfilePictureProperty()
        {
            var userToTest = this.usersServices.GetUser("valid user id");

            this.usersServices.Upload(userToTest.Id, new byte[] { 1, 2 });

            Assert.IsNotNull(userToTest.UserSettings.ProfilePicture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetLastLoginShouldThrowExceptionWithInvalidEmail()
        {
            this.usersServices.SetLastLogin(string.Empty, new DateTime());
        }

        [TestMethod]
        public void SetLastLoginShouldSetLastLoginProperty()
        {
            var userToTest = this.usersServices.GetUser("valid user id");
            var lastLogin = new DateTime(2017, 3, 3);

            this.usersServices.SetLastLogin(userToTest.Email, lastLogin);

            Assert.IsNotNull(userToTest.UserSettings.LastLogin);
            Assert.AreEqual(userToTest.UserSettings.LastLogin, lastLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetLastLogoutShouldThrowExceptionWithInvalidEmail()
        {
            this.usersServices.SetLastLogout(string.Empty, new DateTime());
        }

        [TestMethod]
        public void SetLastLogoutShouldSetSetLastLogoutProperty()
        {
            var userToTest = this.usersServices.GetUser("valid user id");
            var lastLogout = new DateTime(2017, 3, 3);

            this.usersServices.SetLastLogout(userToTest.Email, lastLogout);

            Assert.IsNotNull(userToTest.UserSettings.LastLogout);
            Assert.AreEqual(userToTest.UserSettings.LastLogout, lastLogout);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetLogoffForUserShouldThrowExceptionWithInvalidId()
        {
            this.usersServices.SetLogoffForUser(string.Empty, true);
        }

        [TestMethod]
        public void SetLogoffForUserShouldSetDoesAdminForcedLogoffProperty()
        {
            var userToTest = this.usersServices.GetUser("valid user id");

            this.usersServices.SetLogoffForUser(userToTest.Id, true);

            Assert.IsTrue(userToTest.DoesAdminForcedLogoff);
        }
    }
}
