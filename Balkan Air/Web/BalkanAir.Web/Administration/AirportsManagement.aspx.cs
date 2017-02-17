namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class AirportsManagement : Page
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        public IQueryable<Airport> AirportsGridView_GetData()
        {
            return this.AirportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation);
        }

        public void AirportsGridView_UpdateItem(int id)
        {
            var airport = this.AirportsServices.GetAirport(id);

            if (airport == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(airport);
            if (ModelState.IsValid)
            {
                this.AirportsServices.UpdateAirport(id, airport);
            }
        }

        public void AirportsGridView_DeleteItem(int id)
        {
            this.AirportsServices.DeleteAirport(id);
        }

        public IQueryable<Country> CountryDropDownList_GetData()
        {
            return this.CountriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateAirportBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                bool doesAbbreviationExist = this.AirportsServices.GetAll()
                    .Any(a => a.Abbreviation.ToLower() == this.AbbreviationTextBox.Text.ToLower());

                if (doesAbbreviationExist)
                { 
                    this.AbbreviationTextBox.BorderColor = Color.Red;
                    return;
                }

                var airport = new Airport()
                {
                    Name = this.AirportNameTextBox.Text,
                    Abbreviation = this.AbbreviationTextBox.Text.ToUpper(),
                    CountryId = int.Parse(this.CountryDropDownList.SelectedItem.Value)
                };

                this.AirportsServices.AddAirport(airport);
                this.AbbreviationTextBox.BorderColor = Color.Empty;
            }
        }
    }
}