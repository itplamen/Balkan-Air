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
    using Ninject;

    using Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class Profile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindGenderDropDownList();

                var user = this.GetCurrentUser();

                if (user != null)
                {
                    this.FirstNameTextBox.Text = user.UserSettings.FirstName;
                    this.LastNameTextBox.Text = user.UserSettings.LastName;
                    this.DatepickerTextBox.Text = this.GetDateOfBirthAsAString(user.UserSettings.DateOfBirth);
                    this.GenderDropDownList.SelectedValue = Enum.GetName(typeof(Gender), user.UserSettings.Gender);
                    this.IdentityDocumentNumberTextBox.Text = user.UserSettings.IdentityDocumentNumber;
                    this.PhoneNumberTextBox.Text = user.PhoneNumber;
                    this.FullAddressTextBox.Text = user.UserSettings.FullAddress;
                }
            }
        }

        protected void SavePersonalInfoDataBtn_Click(object sender, EventArgs e)
        {
            var user = this.GetCurrentUser();

            if (user != null)
            {
                user.UserSettings.FirstName = this.FirstNameTextBox.Text;
                user.UserSettings.LastName = this.LastNameTextBox.Text;
                user.UserSettings.DateOfBirth = this.GetDateOfBirthFromDatePickerString();
                user.UserSettings.Gender = (Gender)Enum.Parse(typeof(Gender), this.GenderDropDownList.SelectedItem.Text);
                user.UserSettings.IdentityDocumentNumber = this.IdentityDocumentNumberTextBox.Text;
                user.UserSettings.Nationality = "Bulgaria";
                user.UserSettings.FullAddress = this.FullAddressTextBox.Text;
                user.PhoneNumber = this.PhoneNumberTextBox.Text;

                this.GetManager().Update(user);
            }
        }

        private void BindGenderDropDownList()
        {
            List<Gender> genders = new List<Gender>
            {
                Gender.Male,
                Gender.Female
            };

            this.GenderDropDownList.DataSource = genders;
            this.GenderDropDownList.DataBind();
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

        private DateTime? GetDateOfBirthFromDatePickerString()
        {
            if (this.DatepickerTextBox.Text.Equals(string.Empty))
            {
                return null;
            }

            string[] date = this.DatepickerTextBox.Text.Split('/');
            int year = int.Parse(date[0]);
            int month = int.Parse(date[1]);
            int day = int.Parse(date[2]);

            return new DateTime(year, month, day);
        }

        private string GetDateOfBirthAsAString(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return string.Empty;
            }

            string separator = "/";

            return dateOfBirth.Value.Year.ToString() + separator +
                dateOfBirth.Value.Month.ToString() + separator +
                dateOfBirth.Value.Day.ToString();
        }
    }
}