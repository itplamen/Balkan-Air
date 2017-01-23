namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Data.Models;
    using Data.Services.Contracts;

    public partial class ManageAirports : Page
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Airport> ManageAirportsGridView_GetData()
        {
            return this.AirportsServices.GetAll()
                .OrderBy(a => a.Name);
        }

        public void ManageAirportsGridView_UpdateItem(int id)
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

        public void ManageAirportsGridView_DeleteItem(int id)
        {
            this.AirportsServices.DeleteAirport(id);
        }

        public IQueryable<Country> CountryDropDownList_GetData()
        {
            return this.CountriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name);
        }

        protected void CreateAirportBtn_Click(object sender, EventArgs e)
        {
            var airport = new Airport()
            {
                Name = this.AirportNameTextBox.Text,
                Abbreviation = this.AbbreviationTextBox.Text,
                CountryId = int.Parse(this.CountryDropDownList.SelectedItem.Value)
            };

            this.AirportsServices.AddAirport(airport);
        }
    }
}