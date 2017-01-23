namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    using Common;
    using Data.Models;
    using Data.Services.Contracts;

    public partial class Confirm : Page
    {
        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            string userId = IdentityHelper.GetUserIdFromRequest(Request);

            if (code != null && userId != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = manager.ConfirmEmail(userId, code);

                if (result.Succeeded)
                {
                    successPanel.Visible = true;

                    var user = this.GetCurrentUser();

                    bool didUserReceivedSetAccountNotification = this.UserNotificationsServices.GetAll()
                            .Where(un => un.UserId.Equals(user.Id) && un.NotificationId == Parameters.SET_ACCOUNT_NOTIFICATION_ID)
                            .Any();

                    if (!didUserReceivedSetAccountNotification)
                    {
                        this.UserNotificationsServices.SendNotification(Parameters.SET_ACCOUNT_NOTIFICATION_ID, user.Id);
                    }

                    return;
                }
            }

            successPanel.Visible = false;
            errorPanel.Visible = true;
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