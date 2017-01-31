namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Services.Data.Contracts;

    public partial class ResetPassword : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            if (code != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = this.UsersServices.GetAll()
                    .Where(u => u.Email.Equals(this.Email.Text))
                    .FirstOrDefault();

                if (user == null)
                {
                    ErrorMessage.Text = "No user found!";
                    return;
                }

                var result = manager.ResetPassword(user.Id, code, this.Password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/ResetPasswordConfirmation");
                    return;
                }

                ErrorMessage.Text = result.Errors.FirstOrDefault();
                return;
            }

            ErrorMessage.Text = "An error has occurred";
        }
    }
}