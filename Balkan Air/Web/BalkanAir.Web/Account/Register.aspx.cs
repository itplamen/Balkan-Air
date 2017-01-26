namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    using BalkanAir.Common;
    using BalkanAir.Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class Register : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new User() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                this.SendWelcomeNotification(user.Id);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

                var mailSender = MailSender.Instance;

                string messageBody = "Hello, " + user.Email.Trim() + ",";
                messageBody += "<br /><br />Please click the following link to confirm your account!";
                messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to confirm your account.</a>";
                messageBody += "<br /><br /><i>Best regards, <br /><span style=\"color:#C5027C; font-size: 15px;\"><strong>Balkan Air Bulgaria</strong></span></i>";

                mailSender.SendMail(user.Email, "Confirm your account!", messageBody);

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        private void SendWelcomeNotification(string userId)
        {
            int welcomeNotificationId = 1;
            this.UserNotificationsServices.SendNotification(welcomeNotificationId, userId);
        }
    }
}