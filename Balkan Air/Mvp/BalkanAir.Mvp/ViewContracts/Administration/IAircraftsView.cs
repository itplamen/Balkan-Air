namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAircraftsView : IView<AircraftsViewModels>
    {
        event EventHandler OnAircraftsGetData;

        event EventHandler<AircraftsEventArgs> OnAircraftsUpdateItem;

        event EventHandler<AircraftsEventArgs> OnAircraftsDeleteItem;

        event EventHandler<AircraftsEventArgs> OnAircraftsAddItem;

        event EventHandler OnAircraftManufacturersGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
