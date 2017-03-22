namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IFlightLegsManagementView : IView<FlightLegsManagementViewModel>
    {
        event EventHandler OnFlightLegsGetData;

        event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsUpdateItem;

        event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsDeleteItem;

        event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsAddItem;

        event EventHandler OnAirportsGetData;

        event EventHandler OnFlightsGetData;

        event EventHandler OnRoutesGetData;

        event EventHandler OnLegInstancesGetData;

        event EventHandler<FlightLegsManagementEventArgs> OnAirportGetItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
