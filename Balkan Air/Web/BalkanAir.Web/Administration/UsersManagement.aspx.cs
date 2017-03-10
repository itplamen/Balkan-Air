namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class UsersManagement : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserRolesServices UserRolesServices { get; set; }

        public IQueryable<User> UsersGridView_GetData()
        {
            return this.UsersServices.GetAll();
        }

        public void UsersGridView_UpdateItem(string id)
        {
            var user = this.UsersServices.GetUser(id);
            
            if (user == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(user);
            if (ModelState.IsValid)
            {
                this.UsersServices.UpdateUser(id, user);
            }
        }

        public void UsersGridView_DeleteItem(string id)
        {
            this.UsersServices.DeleteUser(id);
        }

        public IQueryable<object> UsersListBox_GetData()
        {
            var users = this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.Email)
                .Select(u => new
                {
                    Id = u.Id,
                    UserInfo = string.IsNullOrEmpty(u.UserSettings.FirstName) && string.IsNullOrEmpty(u.UserSettings.LastName) ?
                        u.Email + ", (Name not set)" : u.Email + ", (" + u.UserSettings.FirstName + " " + u.UserSettings.LastName + ")"
                });

            return users;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string GetUserRole(ICollection<IdentityUserRole> roles)
        {
            string userRoles = string.Empty;

            foreach (var role in roles)
            {
                var userRole = this.UserRolesServices.GetRole(role.RoleId);

                if (userRole != null)
                {
                    userRoles += userRole.Name + " ";
                }
            }

            return userRoles;
        }
    }
}