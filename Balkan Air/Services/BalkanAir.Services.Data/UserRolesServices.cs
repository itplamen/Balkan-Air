//namespace BalkanAir.Services.Data
//{
//    using System.Linq;

//    using BalkanAir.Data.Models;
//    using BalkanAir.Data.Repositories.Contracts;
//    using Contracts;

//    public class UserRolesServices : IUserRolesServices
//    {
//        private readonly IRepository<IdentityRole> roles;

//        public UserRolesServices(IRepository<IdentityRole> roles)
//        {
//            this.roles = roles;
//        }

//        public string AddRole(IdentityRole role)
//        {
//            this.roles.Add(role);
//            this.roles.SaveChanges();

//            return role.Id;
//        }

//        public IdentityRole GetRole(string id)
//        {
//            return this.roles.GetById(id);
//        }

//        public IQueryable<IdentityRole> GetAll()
//        {
//            return this.roles.All();
//        }

//        public IdentityRole UpdateRole(string id, IdentityRole role)
//        {
//            var roleToUpdate = this.roles.GetById(id);
            
//            if (roleToUpdate != null)
//            {
//                roleToUpdate.Name = role.Name;
//                this.roles.SaveChanges();
//            }

//            return roleToUpdate;
//        }

//        public IdentityRole DeleteRole(string id)
//        {
//            var roleToDelete = this.roles.GetById(id);

//            if (roleToDelete != null)
//            {
//                this.roles.Delete(id);
//                this.roles.SaveChanges();
//            }

//            return roleToDelete;
//        }
//    }
//}
