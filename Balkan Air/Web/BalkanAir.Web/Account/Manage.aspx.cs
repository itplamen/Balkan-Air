namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Auth;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class Manage : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        protected string SuccessMessage { get; private set; }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        private bool HasPassword(ApplicationUserManager manager)
        {
            return manager.HasPassword(User.Identity.GetUserId());
        }

        public bool HasPhoneNumber { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public bool TwoFactorBrowserRemembered { get; private set; }

        public int LoginsCount { get; set; }

        protected void Page_Load()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            HasPhoneNumber = String.IsNullOrEmpty(manager.GetPhoneNumber(User.Identity.GetUserId()));

            // Enable this after setting up two-factor authentientication
            // PhoneNumber.Text = manager.GetPhoneNumber(User.Identity.GetUserId()) ?? String.Empty;

            TwoFactorEnabled = manager.GetTwoFactorEnabled(User.Identity.GetUserId());

            LoginsCount = manager.GetLogins(User.Identity.GetUserId()).Count;

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            if (!IsPostBack)
            {
                this.WelcomeTextLiteral.Text = "Welcome back " + this.CurentUser.UserSettings.FirstName + "!";

                if (this.Context.User.Identity.IsAuthenticated)
                {
                    bool areAnyUpcomingTrips = this.BookingsServices.GetAll()
                    .Where(b => b.UserId == this.CurentUser.Id && b.LegInstance.DepartureDateTime < DateTime.Now)
                   .Any();

                    if (areAnyUpcomingTrips)
                    {
                        this.UpcomingTripsRepeater.Visible = true;
                    }
                    else
                    {
                        this.UpcomingTripsPanel.Visible = false;
                    }
                }

                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage");

                    this.SuccessMessage =
                        message == "ChangePwdSuccess" ? "Your password has been changed!"
                        : message == "SetPwdSuccess" ? "Your password has been set."
                        : message == "RemoveLoginSuccess" ? "The account was removed."
                        : message == "AddPhoneNumberSuccess" ? "Phone number has been added"
                        : message == "RemovePhoneNumberSuccess" ? "Phone number was removed"
                        : string.Empty;
                    this.SuccessMessagePlaceHolder.Visible = !string.IsNullOrEmpty(this.SuccessMessage);
                }
            }
        }

        public IEnumerable<Booking> UpcomingTripsRepeater_GetData()
        {
            return this.BookingsServices.GetAll()
                .Where(b => b.UserId == this.CurentUser.Id && !b.IsDeleted && b.LegInstance.DepartureDateTime > DateTime.Now)
                .ToList();
        }

        // Remove phonenumber from user
        protected void RemovePhone_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var result = manager.SetPhoneNumber(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return;
            }

            var user = manager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("/Account/Manage?m=RemovePhoneNumberSuccess");
            }
        }

        // DisableTwoFactorAuthentication
        protected void TwoFactorDisable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), false);

            Response.Redirect("/Account/Manage");
        }

        // EnableTwoFactorAuthentication 
        protected void TwoFactorEnable_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.SetTwoFactorEnabled(User.Identity.GetUserId(), true);

            Response.Redirect("/Account/Manage");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}