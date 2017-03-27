namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;

    using WebFormsMvp;

    using Models.Administration;
    
    public interface IAdministrationHomeView : IView<AdministrationHomeViewModel>
    {
        event EventHandler OnAdministratorsGetData;
    }
}
