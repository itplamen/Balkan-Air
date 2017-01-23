namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IUserRolesServices
    {
        string AddRole(IdentityRole role);

        IdentityRole GetRole(string id);

        IQueryable<IdentityRole> GetAll();

        IdentityRole UpdateRole(string id, IdentityRole role);

        IdentityRole DeleteRole(string id);
    }
}
