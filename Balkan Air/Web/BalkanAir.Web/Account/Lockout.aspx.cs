using BalkanAir.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BalkanAir.Web.Account
{
    public partial class Lockout : System.Web.UI.Page
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