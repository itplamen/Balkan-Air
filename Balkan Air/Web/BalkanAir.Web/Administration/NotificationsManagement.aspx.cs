namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;
    
    [PresenterBinding(typeof(NotificationsManagementPresenter))]
    public partial class NotificationsManagement : MvpPage<NotificationsManagementViewModel>, INotificationsManagementView
    {
        public event EventHandler OnNotificationsGetData;

        public event EventHandler<NotificationsManagementEventArgs> OnNotificationsUpdateItem;

        public event EventHandler<NotificationsManagementEventArgs> OnNotificationsDeleteItem;

        public event EventHandler<NotificationsManagementEventArgs> OnNotificationsAddItem;

        public IQueryable<Notification> NotificationsGridView_GetData()
        {
            this.OnNotificationsGetData?.Invoke(null, null);

            return this.Model.Notifications;
        }

        public void NotificationsGridView_UpdateItem(int id)
        {
            this.OnNotificationsUpdateItem?.Invoke(null, new NotificationsManagementEventArgs() { Id = id });
        }

        public void NotificationsGridView_DeleteItem(int id)
        {
            this.OnNotificationsDeleteItem?.Invoke(null, new NotificationsManagementEventArgs() { Id = id });
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

                var notificationEventArgs = new NotificationsManagementEventArgs()
                {
                    Content = this.ContentAjaxHtmlEditor.Content,
                    DateCreated = DateTime.Now,
                    Type = selectedType
                };

                this.OnNotificationsAddItem?.Invoke(null, notificationEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedNotificationIdLiteral.Text = notificationEventArgs.Id.ToString();

                this.ClearFeilds();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFeilds();
        }

        private void ClearFeilds()
        {
            this.ContentAjaxHtmlEditor.Content = string.Empty;
            this.TypeDropDownList.SelectedIndex = 0;
        }
    }
}