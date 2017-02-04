namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;

    using AjaxControlToolkit;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class Profile : Page
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindGenderDropDownList();

                if (this.CurrentUser != null)
                {
                    if (this.CurrentUser.UserSettings.ProfilePicture != null)
                    {
                        this.ProfileImage.Src = "data:image/jpeg;base64," + Convert.ToBase64String(this.CurrentUser.UserSettings.ProfilePicture);
                        this.ProfileImage.Visible = true;
                        this.NoProfilePictureLabel.Visible = false;
                    }
                    else
                    {
                        this.NoProfilePictureLabel.Visible = true;
                        this.ProfileImage.Visible = false;
                    }

                    this.FirstNameTextBox.Text = this.CurrentUser.UserSettings.FirstName;
                    this.LastNameTextBox.Text = this.CurrentUser.UserSettings.LastName;
                    this.DatepickerTextBox.Text = this.GetDateOfBirthAsAString(this.CurrentUser.UserSettings.DateOfBirth);
                    this.GenderDropDownList.SelectedValue = Enum.GetName(typeof(Gender), this.CurrentUser.UserSettings.Gender);
                    this.IdentityDocumentNumberTextBox.Text = this.CurrentUser.UserSettings.IdentityDocumentNumber;
                    this.PhoneNumberTextBox.Text = this.CurrentUser.PhoneNumber;
                    this.FullAddressTextBox.Text = this.CurrentUser.UserSettings.FullAddress;
                }
            }
        }

        protected void SavePersonalInfoDataBtn_Click(object sender, EventArgs e)
        {
            if (this.CurrentUser != null)
            {
                this.CurrentUser.UserSettings.FirstName = this.FirstNameTextBox.Text;
                this.CurrentUser.UserSettings.LastName = this.LastNameTextBox.Text;
                this.CurrentUser.UserSettings.DateOfBirth = this.GetDateOfBirthFromDatePickerString();
                this.CurrentUser.UserSettings.Gender = (Gender)Enum.Parse(typeof(Gender), this.GenderDropDownList.SelectedItem.Text);
                this.CurrentUser.UserSettings.IdentityDocumentNumber = this.IdentityDocumentNumberTextBox.Text;
                this.CurrentUser.UserSettings.Nationality = "Bulgaria";
                this.CurrentUser.UserSettings.FullAddress = this.FullAddressTextBox.Text;
                this.CurrentUser.PhoneNumber = this.PhoneNumberTextBox.Text;

                this.Manager.Update(this.CurrentUser);
            }
        }

        protected void OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            byte[] profilePicture = e.GetContents();
            this.UsersServices.Upload(this.CurrentUser.Id, profilePicture);
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