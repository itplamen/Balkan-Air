namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

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

            return "Id:" + airport.Id + " " + airport.Name + " (" + airport.Abbreviation + ")";
        }

        protected void CreateFlightLegBtn_Click(object sender, EventArgs e)
        {
            string seconds = ":00";

            // Convert string to DateTime. The string should look like this: 01/08/2008 14:50:00
            string departureDateTime = this.ScheduledDepartureDateTextBox.Text + " " + 
                this.ScheduledDepartureTimeTextBox.Text + seconds;

            string arrivalDateTime = this.ScheduledArrivalDateTextBox.Text + " " + 
                this.ScheduledArrivalTimeTextBox.Text + seconds;

            DateTime scheduledDepartureDateTime = Convert.ToDateTime(departureDateTime);
            DateTime scheduledArrivalDateTime = Convert.ToDateTime(arrivalDateTime);

            if (this.AreDateTimesValid(scheduledDepartureDateTime, scheduledArrivalDateTime) && 
                this.IsDateTimeAfterDateTimeNow(scheduledDepartureDateTime) && 
                this.IsDateTimeAfterDateTimeNow(scheduledDepartureDateTime) && this.Page.IsValid)
            {
                var newFlightLeg = new FlightLeg()
                {
                    DepartureAirportId = int.Parse(this.AddDepartureAirportDropDownList.SelectedItem.Value),
                    ScheduledDepartureDateTime = scheduledDepartureDateTime,
                    ArrivalAirportId = int.Parse(this.AddArrivalAirportDropDownList.SelectedItem.Value),
                    ScheduledArrivalDateTime = scheduledArrivalDateTime,
                    FlightId = int.Parse(this.AddFlightDropDownList.SelectedItem.Value),
                    RouteId = int.Parse(this.AddRoutesDropDownList.SelectedItem.Value)
                };

                int id = this.FlightLegsServices.AddFlightLeg(newFlightLeg);

                this.SuccessPanel.Visible = true;
                this.AddedFlightLegIdLiteral.Text = id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private bool AreDateTimesValid(DateTime scheduledDepartureDateTime, DateTime scheduledArrivalDateTime)
        {
            int result = DateTime.Compare(scheduledDepartureDateTime, scheduledArrivalDateTime);

            if (result == 0)
            {
                this.AreDateTimesValidCustomValidator.ErrorMessage = "Departure and arrival datetime cannot be equal!";
                this.AreDateTimesValidCustomValidator.IsValid = false;
                return false;
            }
            else if (result > 0)
            {
                this.AreDateTimesValidCustomValidator.ErrorMessage = "Arrival datetime cannot be earlier than departure datetime!";
                this.AreDateTimesValidCustomValidator.IsValid = false;
                return false;
            }

            this.AreDateTimesValidCustomValidator.ErrorMessage = string.Empty;
            this.AreDateTimesValidCustomValidator.IsValid = true;
            return true;
        }

        private bool IsDateTimeAfterDateTimeNow(DateTime dateTime)
        {
            int departureDateCompareResult = DateTime.Compare(dateTime, DateTime.Now);

            if (departureDateCompareResult == 0)
            {
                this.AreDateTimesAfterDateTimeNowCustomValidator.ErrorMessage = "Departure and arrival datetime " +
                    "cannot be equal with datetime now!";
                this.AreDateTimesAfterDateTimeNowCustomValidator.IsValid = false;
                return false;
            }
            else if (departureDateCompareResult < 0)
            {
                this.AreDateTimesAfterDateTimeNowCustomValidator.ErrorMessage = "Departure and arrival " + 
                    " datetime cannot be before datetime now!";
                this.AreDateTimesAfterDateTimeNowCustomValidator.IsValid = false;
                return false;
            }

            this.AreDateTimesAfterDateTimeNowCustomValidator.ErrorMessage = string.Empty;
            this.AreDateTimesAfterDateTimeNowCustomValidator.IsValid = true;
            return true;
        }

        private void ClearFields()
        {
            this.AddDepartureAirportDropDownList.SelectedIndex = 0;
            this.ScheduledDepartureDateTextBox.Text = string.Empty;
            this.ScheduledDepartureTimeTextBox.Text = string.Empty;
            this.AddArrivalAirportDropDownList.SelectedIndex = 0;
            this.ScheduledArrivalDateTextBox.Text = string.Empty;
            this.ScheduledArrivalTimeTextBox.Text = string.Empty;
            this.AddFlightDropDownList.SelectedIndex = 0;
            this.AddRoutesDropDownList.SelectedIndex = 0;
        }
    }
}