namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IRoutesManagementView : IView<RoutesManagementViewModel>
    {
        event EventHandler OnRoutesGetData;

        event EventHandler<RoutesManagementEventArgs> OnRoutesUpdateItem;

        event EventHandler<RoutesManagementEventArgs> OnRoutesDeleteItem;

        event EventHandler<RoutesManagementEventArgs> OnRoutesAddItem;

        event EventHandler OnAirportsGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
