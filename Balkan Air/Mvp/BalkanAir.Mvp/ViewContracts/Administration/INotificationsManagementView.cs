namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface INotificationsManagementView : IView<NotificationsManagementViewModel>
    {
        event EventHandler OnNotificationsGetData;

        event EventHandler<NotificationsManagementEventArgs> OnNotificationsUpdateItem;

        event EventHandler<NotificationsManagementEventArgs> OnNotificationsDeleteItem;

        event EventHandler<NotificationsManagementEventArgs> OnNotificationsAddItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
