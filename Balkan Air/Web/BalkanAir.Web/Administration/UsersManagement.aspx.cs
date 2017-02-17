namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class UsersManagement : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
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
    }
}