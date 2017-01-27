namespace BalkanAir.Services.Data.Contracts
{
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
    }
}
