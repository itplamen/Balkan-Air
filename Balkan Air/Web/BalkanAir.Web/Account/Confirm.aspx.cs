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
    using Services.Data.Contracts;
    using Data.Models;

    public partial class Confirm : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

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

                    bool didUserReceivedSetAccountNotification = this.UserNotificationsServices.GetAll()
                            .Where(un => un.UserId.Equals(userId) && un.Notification.Type == NotificationType.AccountConfirmation)
                            .Any();

                    if (!didUserReceivedSetAccountNotification)
                    {
                        var accountConfirmationNotification = this.NotificationsServices.GetAll()
                            .FirstOrDefault(n => n.Type == NotificationType.AccountConfirmation);

                        this.UserNotificationsServices.SendNotification(accountConfirmationNotification.Id, userId);
                    }

                    return;
                }
            }

            successPanel.Visible = false;
            errorPanel.Visible = true;
        }
    }
}