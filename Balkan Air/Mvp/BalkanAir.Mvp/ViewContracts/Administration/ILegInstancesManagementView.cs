namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface ILegInstancesManagementView : IView<LegInstancesManagementViewModel>
    {
        event EventHandler OnLegInstancesGetData;

        event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesUpdateItem;

        event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesDeleteItem;

        event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesAddItem;

        event EventHandler OnFlightLegsGetData;

        event EventHandler OnFlightStatusesGetData;

        event EventHandler OnAircraftsGetData;

        event EventHandler OnFaresGetData;

        event EventHandler<LegInstancesManagementEventArgs> OnAirportInfoGetItem;

        event EventHandler<LegInstancesManagementEventArgs> OnSendNotificationToSubscribedUsers;

        event EventHandler<LegInstancesManagementEventArgs> OnSendMailToSubscribedUsers;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}