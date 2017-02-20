namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

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
                NotificationType selectedType;
                bool isTypeValid = Enum.TryParse(this.TypeDropDownList.SelectedItem.Text, out selectedType);

                if (!isTypeValid)
                {
                    this.InvalidTypeCustomValidatior.ErrorMessage = "Invalid notification type!";
                    this.InvalidTypeCustomValidatior.IsValid = false;
                    return;
                }

                var notification = new Notification()
                {
                    Content = this.ContentHtmlEditor.Content,
                    DateCreated = DateTime.Now,
                    Type = selectedType
                };

                this.NotificationsServices.AddNotification(notification);

                this.ClearFeilds();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFeilds();
        }

        private void ClearFeilds()
        {
            this.ContentHtmlEditor.Content = string.Empty;
            this.TypeDropDownList.SelectedIndex = 0;
        }
    }
}