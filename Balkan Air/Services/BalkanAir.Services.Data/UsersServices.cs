namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class UsersServices : IUsersServices
    {
        private readonly IRepository<User> users;

        public UsersServices(IRepository<User> users)
        {
            this.users = users;
        }

        public string AddUser(User user)
        {
            this.users.Add(user);
            this.users.SaveChanges();

            return user.Id;
        }

        public User GetUser(string id)
        {
            return this.users.GetById(id);
        }

        public User GetUserByEmail(string email)
        {
            return this.users.All().FirstOrDefault(u => u.Email == email);
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User UpdateUser(string id, User user)
        {
            var userToUpdate = this.users.GetById(id);

            if (userToUpdate != null)
            {
                userToUpdate.UserSettings.FirstName = user.UserSettings.FirstName;
                userToUpdate.UserSettings.LastName = user.UserSettings.LastName;
                userToUpdate.UserSettings.DateOfBirth = user.UserSettings.DateOfBirth;
                userToUpdate.UserSettings.IdentityDocumentNumber = user.UserSettings.IdentityDocumentNumber;
                userToUpdate.UserSettings.Nationality = user.UserSettings.Nationality;
                userToUpdate.UserSettings.FullAddress = user.UserSettings.FullAddress;
                userToUpdate.IsDeleted = user.IsDeleted;

                if (!userToUpdate.IsDeleted && userToUpdate.DeletedOn != null)
                {
                    userToUpdate.DeletedOn = null;
                }

                this.users.SaveChanges();
            }

            return userToUpdate;
        }

        public User DeleteUser(string id)
        {
            var userToDelete = this.users.GetById(id);

            if (userToDelete != null)
            {
                userToDelete.IsDeleted = true;
                userToDelete.DeletedOn = DateTime.Now;
                this.users.SaveChanges();
            }

            return userToDelete;
        }
    }
}
