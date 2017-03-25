namespace BalkanAir.Web.Account
{
    using System;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;
    using Common;
    using App_Start;
    using Auth;

    public partial class Settings : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        protected User CurrentUser
        {
            get{ return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.CurrentUser != null && !this.CurrentUser.EmailConfirmed)
                {
                    this.IsEmailConfirmedLiteral.Text = "No!";
                    this.SendConfirmationEmailLinkButton.Visible = true;
                }
                else
                {
                    this.IsEmailConfirmedLiteral.Text = "Yes!";
                }

                this.ReceiveEmailWhenNewNewsCheckBox.Checked = this.CurrentUser.UserSettings
                    .ReceiveEmailWhenNewNews;

                this.ReceiveEmailWhenNewFlightCheckBox.Checked = this.CurrentUser.UserSettings
                    .ReceiveEmailWhenNewFlight;

                this.ReceiveNotificationWhenNewNewsCheckBox.Checked = this.CurrentUser.UserSettings
                    .ReceiveNotificationWhenNewNews;

                this.ReceiveNotificationWhenNewFlightCheckBox.Checked = this.CurrentUser.UserSettings
                    .ReceiveNotificationWhenNewFlight;
            }   
        }

        protected void SendAnotherConfirmationEmailLinkButton_Click(object sender, EventArgs e)
        {
            string code = this.Manager.GenerateEmailConfirmationToken(this.CurrentUser.Id);
            string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, this.CurrentUser.Id, Request);

            string messageBody = "Hello, " + this.CurrentUser.Email.Trim() + ",";
            messageBody += "<br /><br />Please click the following link to confirm your account!";
            messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to confirm your account.</a>";

            var mailSender = MailSender.Instance;
            mailSender.SendMail(this.CurrentUser.Email, "Confirm your account!", messageBody);
        }

        protected void SaveSettingsBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.CurrentUser.UserSettings.ReceiveEmailWhenNewNews = 
                    this.ReceiveEmailWhenNewNewsCheckBox.Checked;

                this.CurrentUser.UserSettings.ReceiveEmailWhenNewFlight = 
                    this.ReceiveEmailWhenNewFlightCheckBox.Checked;

                this.CurrentUser.UserSettings.ReceiveNotificationWhenNewNews = 
                    this.ReceiveNotificationWhenNewNewsCheckBox.Checked;

                this.CurrentUser.UserSettings.ReceiveNotificationWhenNewFlight = 
                    this.ReceiveNotificationWhenNewFlightCheckBox.Checked;

                this.UsersServices.UpdateUser(this.CurrentUser.Id, this.CurrentUser);
            }
        }
    }
}