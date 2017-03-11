namespace BalkanAir.Services.Data.Contracts
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IUsersServices
    {
        string AddUser(User user);

        User GetUser(string id);

        User GetUserByEmail(string email);

        IQueryable<User> GetAll();

        User UpdateUser(string id, User user);

        User DeleteUser(string id);

        void Upload(string userId, byte[] image);

        void SetLastLogin(string userEmail, DateTime dateTime);

        void SetLastLogout(string userEmail, DateTime dateTime);
    }
}
