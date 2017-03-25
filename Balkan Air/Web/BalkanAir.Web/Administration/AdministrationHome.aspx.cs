namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class AdministrationHome : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserRolesServices UserRolesServices { get; set; }

        public IEnumerable<User> AdministratorsRepeater_GetData()
        {
            var administratorRole = this.UserRolesServices.GetAll()
                .FirstOrDefault(r => r.Name == UserRolesConstants.ADMINISTRATOR_ROLE);

            return this.UsersServices.GetAll()
                .Where(u => u.Roles.Select(r => r.RoleId).Contains(administratorRole.Id))
                .ToList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}