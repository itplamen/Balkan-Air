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
    using Data.Services.Contracts;
   
    public partial class Notifications : Page
    {
        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<UserNotification> NotificationsListView_GetData()
        {
            var user = this.GetCurrentUser();

            return this.UserNotificationsServices.GetAll()
                .Where(un => un.UserId.Equals(user.Id))
                .OrderByDescending(un => un.DateReceived);
        }

        protected void MarkAllNotificationsAsReadBtn_Click(object sender, EventArgs e)
        {
            this.UserNotificationsServices.SetAllNotificationsAsRead(this.GetCurrentUser().Id);
        }

        private ApplicationUserManager GetManager()
        {
            return Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private User GetCurrentUser()
        {
            var manager = this.GetManager();
            return manager.FindById(this.Context.User.Identity.GetUserId());
        }
    }
}