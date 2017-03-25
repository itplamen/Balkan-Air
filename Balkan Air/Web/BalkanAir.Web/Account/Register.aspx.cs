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
    using Common;
    using Data.Models;
    using Services.Data.Contracts;
    using App_Start;

    public partial class Register : Page
    {
        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Context.User.Identity.IsAuthenticated)
            {
                this.Response.Redirect(Pages.HOME);
            }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new User() { UserName = Email.Text, Email = Email.Text };

            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                manager.AddToRole(user.Id, UserRolesConstants.AUTHENTICATED_USER_ROLE);
                
                this.SendWelcomeNotification(user.Id);
                
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

                string messageBody = "Hello, " + user.Email.Trim() + ",";
                messageBody += "<br /><br />Please, click the following link to confirm your account!";
                messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to confirm your account.</a>";

                var mailSender = MailSender.Instance;
                mailSender.SendMail(user.Email, "Confirm your account!", messageBody);

                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                this.UsersServices.SetLastLogin(user.Email, DateTime.Now);

                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        private void SendWelcomeNotification(string userId)
        {
            var welcomeNotification = this.NotificationsServices.GetAll()
                .FirstOrDefault(n => n.Type == NotificationType.Welcome);

            if (welcomeNotification != null)
            {
                this.UserNotificationsServices.SendNotification(welcomeNotification.Id, userId);
            }
        }
    }
}