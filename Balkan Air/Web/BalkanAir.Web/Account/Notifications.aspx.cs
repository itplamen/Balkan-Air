namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    using Data.Models;
    using BalkanAir.Services.Data.Contracts;
    using System.Web.ModelBinding;
    using Common;
    using Auth;

    public partial class Notifications : Page
    {
        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        public IQueryable<UserNotification> NotificationsListView_GetData()
        {
            return this.UserNotificationsServices.GetAll()
                .Where(un => un.UserId.Equals(this.CurrentUser.Id))
                .OrderByDescending(un => un.DateReceived);
        }

        public UserNotification ViewNotificationFormView_GetItem([QueryString] int id)
        {
            var userNotification = this.UserNotificationsServices.GetAll()
                .FirstOrDefault(un => un.NotificationId == id && un.UserId == this.CurrentUser.Id);

            if (userNotification == null)
            {
                this.Response.Redirect(Pages.NOTIFICATIONS);
            }

            if (!userNotification.IsRead)
            {
                this.UserNotificationsServices.SetNotificationAsRead(userNotification.NotificationId, this.CurrentUser.Id);
            }

            return userNotification;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Request.QueryString.Count > 0)
                {
                    this.ConcreteNotificationPanel.Visible = true;
                    this.AllNotificationsPanel.Visible = false;
                }
                else
                {
                    this.AllNotificationsPanel.Visible = true;
                    this.ConcreteNotificationPanel.Visible = false;
                }
            }
        }

        protected void MarkAllNotificationsAsReadBtn_Click(object sender, EventArgs e)
        {
            this.UserNotificationsServices.SetAllNotificationsAsRead(this.CurrentUser.Id);
        }
    }
}