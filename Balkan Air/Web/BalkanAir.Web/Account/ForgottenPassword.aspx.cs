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
    using App_Start;
    using Auth;

    public partial class ForgotPassword : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var user = this.UsersServices.GetAll()
                    .Where(u => u.Email.Equals(this.Email.Text))
                    .FirstOrDefault();

                if (user == null || !user.EmailConfirmed)
                {
                    FailureText.Text = "The user either does not exist or is not confirmed.";
                    ErrorMessage.Visible = true;
                    return;
                }

                var manager = this.Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                string code = manager.GeneratePasswordResetToken(user.Id);
                string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);

                string username = string.Empty;

                if (string.IsNullOrEmpty(user.UserSettings.FirstName) && string.IsNullOrEmpty(user.UserSettings.LastName))
                {
                    username = user.Email;
                }
                else
                {
                    username = user.UserSettings.FirstName + " " + user.UserSettings.LastName;
                }

                string messageBody = "Hello, " + username + ",";
                messageBody += "<br /><br />Please click the following link to reset your password!";
                messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to reset your password.</a>";

                var mailSender = MailSender.Instance;
                mailSender.SendMail(user.Email, "Reset Password!", messageBody);

                loginForm.Visible = false;
                DisplayEmail.Visible = true;
            }
        }
    }
}