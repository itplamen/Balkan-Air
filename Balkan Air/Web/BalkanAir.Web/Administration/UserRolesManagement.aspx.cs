namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Ninject;

    using Services.Data.Contracts;

    public partial class UserRolesManagement : Page
    {
        [Inject]
        public IUserRolesServices UserRolesServices { get; set; }  

        public IQueryable<IdentityRole> UserRolesGridView_GetData()
        {
            return this.UserRolesServices.GetAll();
        }

        public void UserRolesGridView_UpdateItem(string id)
        {
            var role = this.UserRolesServices.GetRole(id);

            if (role == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            if (this.DoesUserRoleExist(role.Name))
            {
                this.ShowRoleExistsErrorMessage(role.Name);
                return;
            }

            TryUpdateModel(role);
            if (ModelState.IsValid)
            {
                this.UserRolesServices.UpdateRole(id, role);
            }
        }

        public void UserRolesGridView_DeleteItem(string id)
        {
            this.UserRolesServices.DeleteRole(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateUserRoleBtn_Click(object sender, EventArgs e)
        {
            if (this.DoesUserRoleExist(this.UserRoleNameTextBox.Text))
            {
                this.ShowRoleExistsErrorMessage(this.UserRoleNameTextBox.Text);                
            }

            if (this.Page.IsValid)
            {
                IdentityRole role = new IdentityRole() { Name = this.UserRoleNameTextBox.Text };
                this.UserRolesServices.AddRole(role);

                this.SuccessPanel.Visible = true;
                this.AddedUserRoleNameLiteral.Text = this.UserRoleNameTextBox.Text;

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ShowRoleExistsErrorMessage(string userRoleName)
        {
            this.CustomValidator.Text = userRoleName + " user role already exists!";
            this.CustomValidator.IsValid = false;
        }

        private bool DoesUserRoleExist(string userRoleName)
        {
            return this.UserRolesServices.GetAll()
                .Any(ur => ur.Name == userRoleName);
        }

        private void ClearFields()
        {
            this.UserRoleNameTextBox.Text = string.Empty;
        }
    }
}