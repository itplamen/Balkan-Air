namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class AdministrationHomePresenter : Presenter<IAdministrationHomeView>
    {
        private readonly IUsersServices usersServices;
        private readonly IUserRolesServices userRolesServices;

        public AdministrationHomePresenter(
            IAdministrationHomeView view, 
            IUsersServices usersServices,
            IUserRolesServices userRolesServices) 
            : base(view)
        {
            if (this.usersServices == null)
            {
                throw new ArgumentNullException(nameof(IUsersServices));
            }

            if (this.userRolesServices == null)
            {
                throw new ArgumentNullException(nameof(IUserRolesServices));
            }

            this.usersServices = usersServices;
            this.userRolesServices = userRolesServices;

            this.View.OnAdministratorsGetData += this.View_OnAdministratorsGetData;
        }

        private void View_OnAdministratorsGetData(object sender, EventArgs e)
        {
            var administratorRole = this.userRolesServices.GetAll()
                .FirstOrDefault(r => r.Name == UserRolesConstants.ADMINISTRATOR_ROLE);

            this.View.Model.Administrators = this.usersServices.GetAll()
                .Where(u => u.Roles.Select(r => r.RoleId).Contains(administratorRole.Id))
                .ToList();
        }
    }
}
