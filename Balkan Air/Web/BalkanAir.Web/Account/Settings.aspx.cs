namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Data.Models;

    public partial class Settings : Page
    {
        private User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.user = this.GetCurrentUser();

                if (this.user != null && !this.user.EmailConfirmed)
                {
                    this.IsEmailConfirmedLiteral.Text = "No!";
                    this.SendConfirmationEmailLinkButton.Visible = true;
                }
                else
                {
                    this.IsEmailConfirmedLiteral.Text = "Yes!";
                }
            }   
        }

        protected string GetUserEmail()
        {
            return this.GetCurrentUser().Email;
        }

        protected void SaveSettingsBtn_Click(object sender, EventArgs e)
        {

            //this.ReceiveEmailWhenNewNewsCheckBox.Checked
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