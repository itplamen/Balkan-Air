namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAircraftManufacturersManagementView : IView<AircraftManufacturersManagementViewModel>
    {
        event EventHandler OnAircraftManufacturersGetData;

        event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersUpdateItem;

        event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersDeleteItem;

        event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersAddItem;

        event EventHandler OnAircraftsGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
