namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

 
    using Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class AdministrationHome : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        //[Inject]
        //public IUserRolesServices UserRolesServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IEnumerable<User> AdministratorsRepeater_GetData()
        {
            //var administratorRole = this.UserRolesServices.GetAll()
            //    .FirstOrDefault(r => r.Name.ToLower().Equals(GlobalConstants.ADMINISTRATOR_ROLE));

            //if (administratorRole == null)
            //{
            //    return null;
            //}


            //return this.UsersServices.GetAll()
            //    .Where(u => u.Roles.Select(s => s.RoleId).Contains(administratorRole.Id))
            //    .ToList(); ;

            return null;
        }
    }
}