namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class NotificationsManagementPresenter : Presenter<INotificationsManagementView>
    {
        private readonly INotificationsServices notificationsServices;

        public NotificationsManagementPresenter(INotificationsManagementView view, INotificationsServices notificationsServices) 
            : base(view)
        {
            if (notificationsServices == null)
            {
                throw new ArgumentNullException(nameof(INotificationsServices));
            }

            this.notificationsServices = notificationsServices;

            this.View.OnNotificationsGetData += this.View_OnNotificationsGetData;
            this.View.OnNotificationsUpdateItem += this.View_OnNotificationsUpdateItem;
            this.View.OnNotificationsDeleteItem += this.View_OnNotificationsDeleteItem;
            this.View.OnNotificationsAddItem += this.View_OnNotificationsAddItem;
        }

        private void View_OnNotificationsGetData(object sender, EventArgs e)
        {
            this.View.Model.Notifications = this.notificationsServices.GetAll();
        }

        private void View_OnNotificationsUpdateItem(object sender, NotificationsManagementEventArgs e)
        {
            var notification = this.notificationsServices.GetNotification(e.Id);

            if (notification == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(notification);

            if (this.View.ModelState.IsValid)
            {
                this.notificationsServices.UpdateNotification(e.Id, notification);
            }
        }

        private void View_OnNotificationsDeleteItem(object sender, NotificationsManagementEventArgs e)
        {
            this.notificationsServices.DeleteNotification(e.Id);
        }

        private void View_OnNotificationsAddItem(object sender, NotificationsManagementEventArgs e)
        {
            var notification = new Notification()
            {
                Content = e.Content,
                DateCreated = e.DateCreated,
                Type = e.Type
            };

            e.Id = this.notificationsServices.AddNotification(notification);
        }
    }
}
