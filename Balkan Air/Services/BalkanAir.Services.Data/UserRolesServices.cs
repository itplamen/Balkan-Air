namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class UserRolesServices : IUserRolesServices
    {
        private readonly IRepository<IdentityRole> roles;

        public UserRolesServices(IRepository<IdentityRole> roles)
        {
            this.roles = roles;
        }

        public string AddRole(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.roles.Add(role);
            this.roles.SaveChanges();

            return role.Id;
        }

        public IdentityRole GetRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            return this.roles.GetById(id);
        }

        public IQueryable<IdentityRole> GetAll()
        {
            return this.roles.All();
        }

        public IdentityRole UpdateRole(string id, IdentityRole role)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            if (role == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var roleToUpdate = this.roles.GetById(id);

            if (roleToUpdate != null)
            {
                roleToUpdate.Name = role.Name;
                this.roles.SaveChanges();
            }

            return roleToUpdate;
        }

        public IdentityRole DeleteRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_ID);
            }

            var roleToDelete = this.roles.GetById(id);

            if (roleToDelete != null)
            {
                this.roles.Delete(id);
                this.roles.SaveChanges();
            }
            
            return roleToDelete;
        }
    }
}
