namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;
    
    public partial class FlightLegsManagement : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFlightLegsServices FlightLegsServices { get; set; }

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public IRoutesServices RoutesServices { get; set; }

        public IQueryable<FlightLeg> FlightLegsGridView_GetData()
        {
            return this.FlightLegsServices.GetAll();
        }

        public void FlightLegsGridView_UpdateItem(int id)
        {
            var flightLeg = this.FlightLegsServices.GetFlightLeg(id);
            
            if (flightLeg == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(flightLeg);

            if (ModelState.IsValid)
            {
                this.FlightLegsServices.UpdateFlightLeg(id, flightLeg);
            }
        }

        public void FlightLegsGridView_DeleteItem(int id)
        {
            this.FlightLegsServices.DeleteFlightLeg(id);
        }

        public IQueryable<object> AirportsDropDownList_GetData()
        {
            return this.AirportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation)
                .Select(a => new
                {
                    Id = a.Id,
                    AirportInfo = "Id:" + a.Id + ", " + a.Name + " (" + a.Abbreviation + ")"
                });
        }

        public IQueryable<object> FlightsDropDownList_GetData()
        {
            return this.FlightsServices.GetAll()
                .OrderBy(f => f.Id)
                .Select(f => new
                {
                    Id = f.Id,
                    FlightInfo = "Id:" + f.Id + " " + f.Number
                });
        }

        public IQueryable<object> RoutesDropDownList()
        {
            return this.RoutesServices.GetAll()
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name)
                .Select(r => new
                {
                    Id = r.Id,
                    RouteInfo = "Id:" + r.Id + " " + r.Origin.Name + " (" + r.Origin.Abbreviation + ")  -> " + 
                                r.Destination.Name + " (" + r.Destination.Abbreviation + ")"
                });
        }

        public IQueryable<object> LegInstancesListBox_GetData()
        {
            var legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.DepartureDateTime)
                .Select(l => new
                {
                    Id = l.Id,
                    LegInstanceInfo = "Id:" + l.Id + " " + l.DepartureDateTime + " -> " + 
                                        l.ArrivalDateTime + ", " + l.FlightStatus.Name
                });

            return legInstances;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string GetAirport(int airportId)
        {
            var airport = this.AirportsServices.GetAirport(airportId);

            if (airport == null)
            {
                return "Airport not foud!";
            }

            return airport.Name + " (" + airport.Abbreviation + ")";
        }

        protected void CreateFlightLegBtn_Click(object sender, EventArgs e)
        {
            {
                string seconds = ":00";

                // Convert string to DateTime. The string should look like this: 01/08/2008 14:50:00
                var departureDateTime = this.ScheduledDepartureDateTextBox.Text + " " + this.ScheduledDepartureTimeTextBox.Text + seconds;
                var arrivalDateTime = this.ScheduledArrivalDateTextBox.Text + " " + this.ScheduledArrivalTimeTextBox.Text + seconds;

                var newFlightLeg = new FlightLeg()
                {
                    DepartureAirportId = int.Parse(this.AddDepartureAirportDropDownList.SelectedItem.Value),
                    ScheduledDepartureDateTime = Convert.ToDateTime(departureDateTime),
                    ArrivalAirportId = int.Parse(this.AddArrivalAirportDropDownList.SelectedItem.Value),
                    ScheduledArrivalDateTime = Convert.ToDateTime(arrivalDateTime),
                    FlightId = int.Parse(this.AddFlightDropDownList.SelectedItem.Value),
                    RouteId = int.Parse(this.AddRoutesDropDownList.SelectedItem.Value),
                    LegInstances = this.GetSelectedLegInstances()
                };

                this.FlightLegsServices.AddFlightLeg(newFlightLeg);
            }
        }

        private ICollection<LegInstance> GetSelectedLegInstances()
        {
            var selectedLegInstances = new List<LegInstance>();

            foreach (ListItem item in this.LegInstancesListBox.Items)
            {
                if (item.Selected)
                {
                    var id = int.Parse(item.Value);
                    var legInstance = this.LegInstancesServices.GetLegInstance(id);
                    selectedLegInstances.Add(legInstance);
                }
            }

            return selectedLegInstances;
        }
    }
}