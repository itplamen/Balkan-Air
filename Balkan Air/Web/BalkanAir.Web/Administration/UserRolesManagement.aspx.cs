namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Ninject;

    using BalkanAir.Services.Data.Contracts;

    public partial class UserRolesManagement : Page
    {
        //[Inject]
        //public IUserRolesServices UserRolesServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<IdentityRole> UserRolesGridView_GetData()
        {
            //return this.UserRolesServices.GetAll();
            return null;
        }

        public void UserRolesGridView_UpdateItem(string id)
        {
            //var role = this.UserRolesServices.GetRole(id);

            //if (role == null)
            //{
            //    ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
            //    return;
            //}

            //TryUpdateModel(role);
            //if (ModelState.IsValid)
            //{
            //    this.UserRolesServices.UpdateRole(id, role);
            //}
        }

        public void UserRolesGridView_DeleteItem(string id)
        {
            //this.UserRolesServices.DeleteRole(id);
        }

        protected void CreateUserRoleBtn_Click(object sender, EventArgs e)
        {
            //IdentityRole role = new IdentityRole() { Name = this.UserRoleNameTextBox.Text };
            //this.UserRolesServices.AddRole(role);
        }
    }
}