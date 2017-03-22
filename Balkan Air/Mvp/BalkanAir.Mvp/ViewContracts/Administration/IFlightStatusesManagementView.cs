namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IFlightStatusesManagementView : IView<FlightStatusesManagementViewModel>
    {
        event EventHandler OnFlightStatusesGetData;

        event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesUpdateItem;

        event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesDeleteItem;

        event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesAddItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
