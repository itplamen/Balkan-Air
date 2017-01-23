namespace BalkanAir.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    using BalkanAir.Common;
    using BalkanAir.Data.Models;
    using Data.Services.Contracts;

    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected int NumberOfUnreadNotifications
        {
            get
            {
                return this.GetCurrentUser().NumberOfUnreadNotifications;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.SiteMapPathBreadcrump.Visible = (SiteMap.CurrentNode != SiteMap.RootNode);

                var currentUser = this.GetCurrentUser();

                // TODO: Check for admin
                if (currentUser != null)
                {
                    this.AdministrationMenu.Visible = true;

                    if (!currentUser.EmailConfirmed)
                    {
                        this.EmailNotConfirmedPanel.Visible = true;
                    }
                }
            }
        }

        public IEnumerable<UserNotification> LatestNotificationsRepeater_GetData()
        {
            var user = this.GetCurrentUser();

            return this.UserNotificationsServices.GetAll()
                .Where(un => un.UserId.Equals(user.Id))
                .OrderByDescending(un => un.DateReceived)
                .Take(5)
                .ToList();
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void SendAnotherConfirmationEmailLinkButton_Click(object sender, EventArgs e)
        {
            var user = this.GetCurrentUser();
            string code = this.GetManager().GenerateEmailConfirmationToken(user.Id);
            string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

            var mailSender = MailSender.Instance;

            string messageBody = "Hello, " + user.Email.Trim() + ",";
            messageBody += "<br /><br />Please click the following link to confirm your account!";
            messageBody += "<br /><a href =\"" + callbackUrl + "\">Click here to confirm your account.</a>";
            messageBody += "<br /><br /><i>Best regards, <br /><span style=\"color:#C5027C; font-size: 15px;\"><strong>Balkan Air Bulgaria</strong></span></i>";

            mailSender.SendMail(user.Email, "Confirm your account!", messageBody);
        }

        protected string GetUserInfo()
        {
            var user = this.GetCurrentUser();

            if (!string.IsNullOrEmpty(user.UserSettings.FirstName) && !string.IsNullOrEmpty(user.UserSettings.LastName))
            {
                return user.UserSettings.FirstName + " " + user.UserSettings.LastName;
            }

            return user.Email;
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