namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Services.Data.Contracts;
    using Data.Models;

    public partial class NotificationsManagement : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        public IQueryable<Notification> NotificationsGridView_GetData()
        {
            return this.NotificationsServices.GetAll();
        }

        public void NotificationsGridView_UpdateItem(int id)
        {
            var notification = this.NotificationsServices.GetNotification(id);
            if (notification == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(notification);
            if (ModelState.IsValid)
            {
                this.NotificationsServices.UpdateNotification(id, notification);
            }
        }

        public void NotificationsGridView_DeleteItem(int id)
        {
            this.NotificationsServices.DeleteNotification(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var notoficationType = Enum.GetValues(typeof(NotificationType));

                this.TypeDropDownList.DataSource = notoficationType;
                this.TypeDropDownList.DataBind();
            }
        }

        protected void CreateNotificationtBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var notification = new Notification()
                {
                    Content = this.ContentHtmlEditor.Content,
                    DateCreated = DateTime.Now,
                    Type = NotificationType.Other
                };

                int notificationId = this.NotificationsServices.AddNotification(notification);
            }
        }
    }
}