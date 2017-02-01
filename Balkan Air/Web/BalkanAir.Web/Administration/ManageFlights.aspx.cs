namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data;
    using Data.Helper;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageFlights : Page
    {
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

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

        public IQueryable<Airport> AirportsDropDownList_GetData()
        {
            return this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
        }

        public IQueryable<object> DepartureAirportsDropDownList_GetData()
        {
            var departureAirports = this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Country.Name)
                .ThenBy(a => a.Name)
                .Select(a => new
                {
                    Id = a.Id,
                    AirportInfo = a.Name + ", (" + a.Abbreviation + ") -> " + a.Country.Name
                });

            return departureAirports;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var selectedDepartureAirport = this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Country.Name)
                .ThenBy(a => a.Name)
                .FirstOrDefault();

                this.BindArrivalAirportsDropDownList(selectedDepartureAirport.Name);
            }
        }

        protected void GenerateFlightNumberBtn_Click(object sender, EventArgs e)
        {
            // TODO: Do this with services!!!
            this.AddFlightNumberTextBox.Text = new FlightNumber(new BalkanAirDbContext()).GetUniqueFlightNumber();
        }

        protected void DepartureAirportsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDepartureAirport = this.DepartureAirportsDropDownList.SelectedItem.Text.Split(new char[] { ',' })[0];

            this.BindArrivalAirportsDropDownList(selectedDepartureAirport);
        }

        protected void CreateFlightBtn_Click(object sender, EventArgs e)
        {
        }

        private void BindArrivalAirportsDropDownList(string departureAirport)
        {
            var arrivalAirports = this.FlightsServices.GetAll()
                .Where(a => a.DepartureAirport.Name.ToLower() == departureAirport.ToLower())
                .Select(a => new
                {
                    Id = a.ArrivalAirport.Id,
                    AirportInfo = a.ArrivalAirport.Name + ", (" + a.ArrivalAirport.Abbreviation + ") -> " + a.ArrivalAirport.Country.Name
                })
                .Distinct()
                .ToList();

            this.ArrivalAirportsDropDownList.DataSource = arrivalAirports;
            this.ArrivalAirportsDropDownList.DataBind();
        }
    }
}