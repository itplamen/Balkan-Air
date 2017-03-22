namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IFlightsManagementView : IView<FlightsManagementViewModel>
    {
        event EventHandler OnFlightsGetData;

        event EventHandler<FlightsManagementEventArgs> OnFlightsUpdateItem;

        event EventHandler<FlightsManagementEventArgs> OnFlightsDeleteItem;

        event EventHandler<FlightsManagementEventArgs> OnFlightsAddItem;

        event EventHandler<FlightsManagementEventArgs> OnUniqueFlightNumberGetItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
