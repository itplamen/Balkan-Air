namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

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

        public IQueryable<object> AircraftsDropDownList_GetData()
        {
            var aircrafts = this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Model)
                .ThenBy(a => a.TotalSeats)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = a.AircraftManufacturer.Name + " " + a.Model + ", " + a.TotalSeats + " seats"
                });

            return aircrafts;
        }

        public IQueryable<object> AirportsDropDownList_GetData()
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

        public IQueryable<object> TravelClassesListBox_GetData()
        {
            var travelClasses = this.TravelClassesServices.GetAll()
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.Type == TravelClassType.Economy)
                .ThenBy(t => t.Type == TravelClassType.Business)
                .ThenBy(t => t.Type == TravelClassType.First)
                .Select(t => new
                {
                    Id = t.Id,
                    TravelClassInfo = t.Type.ToString() + ", Price: " + t.Price + ", Meal: " + t.Meal
                });

            return travelClasses;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GenerateFlightNumberBtn_Click(object sender, EventArgs e)
        {
            this.AddFlightNumberTextBox.Text = new FlightNumber(new BalkanAirDbContext()).GetUniqueFlightNumber();
        }

        protected void CreateFlightBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string seconds = ":00";
                
                // Convert string to DateTime. The string should look like this: 01/08/2008 14:50:00
                var departureDateTime = this.DepartureDatepickerTextBox.Text + " " + this.DepartureTimeTextBox.Text + seconds;
                var arrivalDateTime = this.ArrivalDatepickerTextBox.Text + " " + this.ArrivalTimeTextBox.Text + seconds;

                var newFlight = new Flight()
                {
                    Number = this.AddFlightNumberTextBox.Text.ToUpper(),
                    Departure = Convert.ToDateTime(departureDateTime),
                    Arrival = Convert.ToDateTime(arrivalDateTime),
                    FlightStatusId = int.Parse(this.FlightStatusesDropDownList.SelectedItem.Value),
                    AircraftId = int.Parse(this.AircraftsDropDownList.SelectedItem.Value),
                    DepartureAirportId = int.Parse(this.DepartureAirportsDropDownList.SelectedItem.Value),
                    ArrivalAirportId = int.Parse(this.ArrivalAirportsDropDownList.SelectedItem.Value),
                    TravelClasses = this.GetSelectedTravelClasses()
                };

                this.FlightsServices.AddFlight(newFlight);
            }
        }

        private ICollection<TravelClass> GetSelectedTravelClasses()
        {
            var selectedTravelClasses = new List<TravelClass>();

            foreach (ListItem item in this.TravelClassesListBox.Items)
            {
                if (item.Selected)
                {
                    var id = int.Parse(item.Value);
                    var travelClass = this.TravelClassesServices.GetTravelClass(id);
                    selectedTravelClasses.Add(travelClass);
                }
            }

            return selectedTravelClasses;
        }
    }
}