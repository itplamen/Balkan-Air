namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using AjaxControlToolkit;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Auth;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class Profile : Page
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected string SuccessMessage { get; private set; }

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
                this.DateOfBirthCalendar.EndDate = DateTime.Now;
                this.DatepickerTextBox.Attributes.Add("readonly", "readonly");

                this.BindGenderDropDownList();
                this.BindNationalityDropDownList();

                if (this.CurrentUser != null)
                {
                    this.FillPersonalInformation();
                }
            }
        }

        protected void SavePersonalInfoDataBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.CurrentUser != null)
            {
                this.CurrentUser.UserSettings.FirstName = this.FirstNameTextBox.Text;
                this.CurrentUser.UserSettings.LastName = this.LastNameTextBox.Text;
                this.CurrentUser.UserSettings.DateOfBirth = this.GetDateOfBirthFromDatePickerString();
                this.CurrentUser.UserSettings.Gender = (Gender)Enum.Parse(typeof(Gender), this.GenderDropDownList.SelectedItem.Text);
                this.CurrentUser.UserSettings.IdentityDocumentNumber = this.IdentityDocumentNumberTextBox.Text;

                int nationalityId = int.Parse(this.NationalityDropDownList.SelectedItem.Value);
                this.CurrentUser.UserSettings.Nationality = this.CountriesServices.GetCountry(nationalityId).Name;

                this.CurrentUser.UserSettings.FullAddress = this.FullAddressTextBox.Text;
                this.CurrentUser.PhoneNumber = this.PhoneNumberTextBox.Text;

                this.Manager.Update(this.CurrentUser);

                this.SuccessMessage = "Personal information has been successfully updated!";
                this.SuccessMessagePlaceHolder.Visible = !string.IsNullOrEmpty(this.SuccessMessage);
            }
        }

        protected void OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
        {
            byte[] profilePicture = e.GetContents();
            this.UsersServices.Upload(this.CurrentUser.Id, profilePicture);
        }

        private void FillPersonalInformation()
        {
            if (this.CurrentUser.UserSettings.ProfilePicture != null)
            {
                this.ProfileImage.Src = "data:image/jpeg;base64," + 
                    Convert.ToBase64String(this.CurrentUser.UserSettings.ProfilePicture);

                this.ProfileImage.Visible = true;
                this.NoPictureLabel.Visible = false;
            }
            else
            {
                this.NoPictureLabel.Visible = true;
                this.ProfileImage.Visible = false;
            }

            this.FirstNameTextBox.Text = this.CurrentUser.UserSettings.FirstName;
            this.LastNameTextBox.Text = this.CurrentUser.UserSettings.LastName;
            this.DatepickerTextBox.Text = this.GetDateOfBirthAsAString(this.CurrentUser.UserSettings.DateOfBirth);

            if (this.CurrentUser.UserSettings.Gender == Gender.Male || 
                this.CurrentUser.UserSettings.Gender == Gender.Female)
            {
                this.GenderDropDownList.SelectedValue = ((int)this.CurrentUser.UserSettings.Gender).ToString();
            }

            if (!string.IsNullOrEmpty(this.CurrentUser.UserSettings.Nationality))
            {
                int countryId = this.CountriesServices.GetAll()
                    .FirstOrDefault(c => c.Name.ToLower() == this.CurrentUser.UserSettings.Nationality.ToLower())
                    .Id;

                this.NationalityDropDownList.SelectedValue = countryId.ToString();
            }

            this.IdentityDocumentNumberTextBox.Text = this.CurrentUser.UserSettings.IdentityDocumentNumber;
            this.PhoneNumberTextBox.Text = this.CurrentUser.PhoneNumber;
            this.FullAddressTextBox.Text = this.CurrentUser.UserSettings.FullAddress;
        }

        private void BindGenderDropDownList()
        {
            var genders = Enum.GetValues(typeof(Gender));

            foreach (var gender in genders)
            {
                this.GenderDropDownList.Items.Add(new ListItem(gender.ToString(), ((int)gender).ToString()));
            }

            if (this.CurrentUser.UserSettings.Gender != Gender.Male &&
                this.CurrentUser.UserSettings.Gender != Gender.Female)
            {
                this.GenderDropDownList.Items.Insert(
                    Common.WebConstants.GENDER_NOT_SELECTED_INDEX,
                    new ListItem(
                        Common.WebConstants.GENDER_NOT_SELECTED_TEXT,
                        Common.WebConstants.GENDER_NOT_SELECTED_INDEX.ToString()
                    )
                );
            }
        }

        private void BindNationalityDropDownList()
        {
            this.NationalityDropDownList.DataSource = this.CountriesServices.GetAll()
                .OrderBy(c => c.Name)
                .ToList();
            this.NationalityDropDownList.DataBind();

            if (string.IsNullOrEmpty(this.CurrentUser.UserSettings.Nationality))
            {
                this.NationalityDropDownList.Items.Insert(
                    Common.WebConstants.NATIONALITY_NOT_SELECTED_INDEX,
                    new ListItem(
                        Common.WebConstants.NATIONALITY_NOT_SELECTED_TEXT,
                        Common.WebConstants.NATIONALITY_NOT_SELECTED_INDEX.ToString()
                    )
                );
            }
        }

        private string GetDateOfBirthAsAString(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
            {
                return string.Empty;
            }

            string separator = "/";

            return dateOfBirth.Value.Day.ToString() + separator + 
                dateOfBirth.Value.Month.ToString() + separator +
                dateOfBirth.Value.Year.ToString();
        }

        private DateTime? GetDateOfBirthFromDatePickerString()
        {
            if (this.DatepickerTextBox.Text.Equals(string.Empty))
            {
                return null;
            }

            string[] date = this.DatepickerTextBox.Text.Split('/');
            int day = int.Parse(date[0]);
            int month = int.Parse(date[1]);
            int year = int.Parse(date[2]);

            return new DateTime(year, month, day);
        }
    }
}