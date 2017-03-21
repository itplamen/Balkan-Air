namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAircraftManufacturersView : IView<AircraftManufacturersViewModel>
    {
        event EventHandler OnAircraftManufacturersGetData;

        event EventHandler<AircraftManufacturersEventArgs> OnAircraftManufacturersUpdateItem;

        event EventHandler<AircraftManufacturersEventArgs> OnAircraftManufacturersDeleteItem;

        event EventHandler<AircraftManufacturersEventArgs> OnAircraftManufacturersAddItem;

        event EventHandler OnAircraftsGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
