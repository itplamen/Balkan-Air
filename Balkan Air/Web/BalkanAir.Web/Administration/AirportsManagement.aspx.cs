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

    [PresenterBinding(typeof(AirportsManagementPresenter))]
    public partial class AirportsManagement : MvpPage<AirportsManagementViewModel>, IAirportsManagementView
    {
        public event EventHandler OnAirprotsGetData;
        public event EventHandler<AirportsManagementEventArgs> OnAirportsUpdateItem;
        public event EventHandler<AirportsManagementEventArgs> OnAirportsDeleteItem;
        public event EventHandler<AirportsManagementEventArgs> OnAirprotsAddItem;
        public event EventHandler OnCountriesGetData;

        public IQueryable<Airport> AirportsGridView_GetData()
        {
            this.OnAirprotsGetData?.Invoke(null, null);

            return this.Model.Airports;
        }

        public void AirportsGridView_UpdateItem(int id)
        {
            this.OnAirportsUpdateItem?.Invoke(null, new AirportsManagementEventArgs()
            {
                Id = id,
                CountryId = int.Parse(this.CountryIdHiddenField.Value)
            });
        }

        public void AirportsGridView_DeleteItem(int id)
        {
            this.OnAirportsDeleteItem?.Invoke(null, new AirportsManagementEventArgs() { Id = id });
        }

        public IQueryable<Country> CountryDropDownList_GetData()
        {
            this.OnCountriesGetData?.Invoke(null, null);
            
            return this.Model.Countries;
        }

        protected void CreateAirportBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.OnAirprotsGetData?.Invoke(sender, e);

                bool doesAbbreviationExist = this.Model.Airports
                    .Any(a => a.Abbreviation.ToLower() == this.AbbreviationTextBox.Text.ToLower());

                if (doesAbbreviationExist)
                { 
                    this.AbbreviationTextBox.BorderColor = Color.Red;
                    return;
                }

                var airportsEventArgs = new AirportsManagementEventArgs()
                {
                    Name = this.AirportNameTextBox.Text,
                    Abbreviation = this.AbbreviationTextBox.Text.ToUpper(),
                    CountryId = int.Parse(this.CountryDropDownList.SelectedItem.Value)
                };

                this.OnAirprotsAddItem?.Invoke(sender, airportsEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedAirportIdLiteral.Text = airportsEventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.AirportNameTextBox.Text = string.Empty;
            this.AbbreviationTextBox.Text = string.Empty;
            this.AbbreviationTextBox.BorderColor = Color.Empty;
            this.CountryDropDownList.SelectedIndex = 0;
        }
    }
}