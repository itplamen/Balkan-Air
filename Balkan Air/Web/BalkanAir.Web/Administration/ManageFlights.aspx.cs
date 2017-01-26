namespace BalkanAir.Web.Private
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using BalkanAir.Data;
    using BalkanAir.Data.Helper;
    using BalkanAir.Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class ManageFlights : Page
    {
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.CreateFlightPanel.Visible = false;
            }
        }

        public IQueryable<Flight> ManageFlightsGridView_GetData()
        {
            return this.FlightsServices.GetAll();
        }

        public void ManageFlightsGridView_UpdateItem(int id)
        {
            var flight = this.FlightsServices.GetFlight(id);

            if (flight == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(flight);
            if (ModelState.IsValid)
            {
                this.FlightsServices.UpdateFlight(id, flight);
            }
        }

        public void ManageFlightsGridView_DeleteItem(int id)
        {
            this.FlightsServices.DeleteFlight(id);
        }

        public IQueryable<FlightStatus> FlightStatusesDropDownList_GetData()
        {
            return this.FlightStatusesServices.GetAll()
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.Name);
        }

        public IQueryable<Aircraft> AircraftDropDownList_GetData()
        {
            return this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Model);
        }

        public IQueryable<Country> CountrytDropDownList_GetData()
        {
            return this.CountriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name);
        }

        public IQueryable<Airport> AirportsDropDownList_GetData()
        {
            return this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
        }

        protected void CreateFlightBtn_Click(object sender, EventArgs e)
        {
        }

        protected void GenerateFlightNumberBtn_Click(object sender, EventArgs e)
        {
            // TODO: Do this with services!!!
            this.AddFlightNumberTextBox.Text = new FlightNumber(new BalkanAirDbContext()).GetUniqueFlightNumber();
        }

        protected void FromCountrytDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fromCountry = this.FromCountrytDropDownList.SelectedItem.Text;

            List<Airport> countryAirports = this.AirportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ToList();

            this.FromAirportRepeater.DataSource = countryAirports;
            this.FromAirportRepeater.DataBind();
        }

        protected void OnAirportSelectButtonClicked(object sender, EventArgs e)
        {
            LinkButton selectedAirport = sender as LinkButton;

            if (selectedAirport != null)
            {
                
            }
        }

        protected void NewFlightBtn_Click(object sender, EventArgs e)
        {
            this.CreateFlightPanel.Visible = true;
        }

     
    }
}