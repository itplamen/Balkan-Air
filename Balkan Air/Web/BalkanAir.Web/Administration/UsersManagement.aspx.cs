namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Auth;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class UsersManagement : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserRolesServices UserRolesServices { get; set; }

        private ApplicationUserManager Manager
        {
            get
            {
                return Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private User CurrentUser
        {
            get
            {
                var userId = this.UsersToChangeRolesListBox.SelectedItem.Value;
                return this.UsersServices.GetUser(userId);
            }
        }

        public IQueryable<User> UsersGridView_GetData()
        {
            return this.UsersServices.GetAll();
        }

        public void UsersGridView_UpdateItem(string id)
        {
            var user = this.UsersServices.GetUser(id);

            if (user == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format("Item with id {0} was not found", id));
                return;
            }

            this.TryUpdateModel(user);
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

        public IQueryable<IdentityRole> RolesOfSelectedUserListBox_GetData()
        {
            return this.UserRolesServices.GetAll()
                .OrderBy(r => r.Name);
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

        protected void LogoffUsersButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                foreach (ListItem item in this.UsersListBox.Items)
                {
                    if (item.Selected)
                    {
                        string userId = item.Value;

                        this.UsersServices.SetLogoffForUser(userId, true);
                    }
                }

                this.SuccessPanelLiteral.Text = "Users are logged off successfully!";
                this.SuccessPanel.Visible = true;

                this.UsersListBox.ClearSelection();
            }
        }

        protected void CancelLogoffUsersButton_Click(object sender, EventArgs e)
        {
            this.UsersListBox.ClearSelection();
            this.LogoffUsersPanel.Visible = false;
        }

        protected void LogoffUsersBtn_Click(object sender, EventArgs e)
        {
            this.LogoffUsersPanel.Visible = true;
            this.ChangeRolesPanel.Visible = false;
        }

        protected void ChangeUsersRolesBtn_Click(object sender, EventArgs e)
        {
            this.ChangeRolesPanel.Visible = true;
            this.LogoffUsersPanel.Visible = false;
        }

        protected void UsersToChangeRolesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentUser != null)
            {
                this.RolesOfSelectedUserListBox.ClearSelection();

                foreach (ListItem item in this.RolesOfSelectedUserListBox.Items)
                {
                    var listBoxRoleId = item.Value;

                    if (this.CurrentUser.Roles.Any(ur => ur.RoleId == listBoxRoleId))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        protected void AddToUserRolesButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                foreach (ListItem item in this.RolesOfSelectedUserListBox.Items)
                {
                    if (item.Selected)
                    {
                        var roleId = item.Value;

                        if (!this.CurrentUser.Roles.Any(ur => ur.RoleId == roleId))
                        {
                            var roleName = item.Text;
                            this.Manager.AddToRole(this.CurrentUser.Id, roleName);
                        }
                    }
                }

                this.SuccessPanelLiteral.Text = "Added successfully!";
                this.SuccessPanel.Visible = true;
            }
        }

        protected void DeleteFromUserRolesButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                foreach (ListItem item in this.RolesOfSelectedUserListBox.Items)
                {
                    if (item.Selected)
                    {
                        var roleId = item.Value;

                        if (this.CurrentUser.Roles.Any(ur => ur.RoleId == roleId))
                        {
                            var roleName = item.Text;
                            this.Manager.RemoveFromRole(this.CurrentUser.Id, roleName);
                        }
                    }
                }

                this.SuccessPanelLiteral.Text = "Removed successfully!";
                this.SuccessPanel.Visible = true;
            }
        }

        protected void CancelChanginUserRolesButton_Click(object sender, EventArgs e)
        {
            this.UsersToChangeRolesListBox.ClearSelection();
            this.UsersToChangeRolesListBox.ClearSelection();
            this.ChangeRolesPanel.Visible = false;
        }
    }
}