namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(AdministrationHomePresenter))]
    public partial class AdministrationHome : MvpPage<AdministrationHomeViewModel>, IAdministrationHomeView
    {
        public event EventHandler OnAdministratorsGetData;

        public IEnumerable<User> AdministratorsRepeater_GetData()
        {
            this.OnAdministratorsGetData?.Invoke(null, null);

            return this.Model.Administrators;
        }
    }
}