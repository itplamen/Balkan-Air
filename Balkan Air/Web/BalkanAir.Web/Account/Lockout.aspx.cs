namespace BalkanAir.Web.Account
{
    using System;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Auth;
    using Data.Models;

    public partial class Lockout : Page
    {
        protected ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        protected User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //CurrentUser.LockoutEnabled = true;
            //CurrentUser.LockoutEnabled = true;
            //CurrentUser.LockoutEndDateUtc = DateTime.UtcNow.AddMinutes(42);
          
        }
    }
}