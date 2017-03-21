namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAircraftsManagementView : IView<AircraftsManagementViewModels>
    {
        event EventHandler OnAircraftsGetData;

        event EventHandler<AircraftsManagementEventArgs> OnAircraftsUpdateItem;

        event EventHandler<AircraftsManagementEventArgs> OnAircraftsDeleteItem;

        event EventHandler<AircraftsManagementEventArgs> OnAircraftsAddItem;

        event EventHandler OnAircraftManufacturersGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
