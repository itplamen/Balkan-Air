namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(CountriesManagementPresenter))]
    public partial class CountriesManagement : MvpPage<CountriesManagementViewModel>, ICountriesManagementView
    {
        public event EventHandler OnCountriesGetData;

        public event EventHandler<CountriesManagementEventArgs> OnCountriesUpdateItem;

        public event EventHandler<CountriesManagementEventArgs> OnCountriesDeleteItem;

        public event EventHandler<CountriesManagementEventArgs> OnCountriesAddItem;

        public IQueryable<Country> CountriesGridView_GetData()
        {
            this.OnCountriesGetData?.Invoke(null, null);

            return this.Model.Countries;
        }

        public void CountriesGridView_UpdateItem(int id)
        {
            this.OnCountriesUpdateItem?.Invoke(null, new CountriesManagementEventArgs() { Id = id });   
        }

        public void CountriesGridView_DeleteItem(int id)
        {
            this.OnCountriesDeleteItem?.Invoke(null, new CountriesManagementEventArgs() { Id = id });
        }

        protected void CreateCountrytBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.OnCountriesGetData?.Invoke(sender, e);

                var doesAbbreviationExist = this.Model.Countries
                    .Any(c => c.Abbreviation.ToLower() == this.AbbreviationNameTextBox.Text.ToLower());

                if (doesAbbreviationExist)
                {
                    this.AbbreviationNameTextBox.BorderColor = Color.Red;
                    return;
                }

                var countryEventArgs = new CountriesManagementEventArgs()
                {
                    Name = this.CountryNameTextBox.Text,
                    Abbreviation = this.AbbreviationNameTextBox.Text.ToUpper()
                };

                this.OnCountriesAddItem?.Invoke(null, countryEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedCountryIdLiteral.Text = countryEventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.CountryNameTextBox.Text = string.Empty;
            this.AbbreviationNameTextBox.Text = string.Empty;
            this.AbbreviationNameTextBox.BorderColor = Color.Empty;
        }
    }
}