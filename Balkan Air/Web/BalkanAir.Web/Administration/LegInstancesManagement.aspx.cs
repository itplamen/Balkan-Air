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

    public partial class LegInstancesManagement : Page
    {
        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFlightLegsServices FlightLegsServices { get; set; }

        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        public IQueryable<LegInstance> LegInstancesGridView_GetData()
        {
            return this.LegInstancesServices.GetAll();
        }

        public void LegInstancesGridView_UpdateItem(int id)
        {
            var legInstance = this.LegInstancesServices.GetLegInstance(id);

            if (legInstance == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(legInstance);
            if (ModelState.IsValid)
            {
                this.LegInstancesServices.UpdateLegInstance(id, legInstance);
            }
        }

        public void LegInstancesGridView_DeleteItem(int id)
        {
            this.LegInstancesServices.DeleteLegInstance(id);
        }

        public IQueryable<object> FlightLegDropDownList_GetData()
        {
            return this.FlightLegsServices.GetAll()
                .Where(l => !l.IsDeleted)
                .Select(l => new
                {
                    Id = l.Id,
                    FlightLegInfo = "Id:" + l.Id + " " + l.Flight.Number + ", " + " (" + l.ScheduledDepartureDateTime + ")" + 
                                    " -> " + " (" + l.ScheduledArrivalDateTime + ")"
                });
        }

        public IQueryable<FlightStatus> FlightStatusDropDownList_GetData()
        {
            return this.FlightStatusesServices.GetAll()
                .OrderBy(f => f.Name);
        }

        public IQueryable<object> AircraftDropDownList_GetData()
        {
            return this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = "Id:" + a.Id + ", " + a.AircraftManufacturer.Name + " " + a.Model
                });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string GetAirport(int airportId)
        {
            var airport = this.AirportsServices.GetAirport(airportId);

            if (airport == null)
            {
                return "Airport not found!";
            }

            return airport.Name + " (" + airport.Abbreviation + ")";
        }

        protected void CreateLegInstancetBtn_Click(object sender, EventArgs e)
        {
            string seconds = ":00";

            // Convert string to DateTime. The string should look like this: 01/08/2008 14:50:00
            string stringDepartureDateTime = this.DepartureDateTextBox.Text + " " + this.DepartureTimeTextBox.Text + seconds;
            string stringArrivalDateTime = this.ArrivalDateTextBox.Text + " " + this.ArrivalTimeTextBox.Text + seconds;

            DateTime departureDateTime = Convert.ToDateTime(stringDepartureDateTime);
            DateTime arrivalDateTime = Convert.ToDateTime(stringArrivalDateTime);

            if (this.AreDateTimesValid(departureDateTime, arrivalDateTime) && this.IsDateTimeAfterDateTimeNow(departureDateTime) &&
                this.IsDateTimeAfterDateTimeNow(departureDateTime) && this.Page.IsValid)
            {
                var legInstance = new LegInstance()
                {
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    FlightLegId = int.Parse(this.AddFlightLegDropDownList.SelectedItem.Value),
                    FlightStatusId = int.Parse(this.AddFlightStatusDropDownList.SelectedItem.Value),
                    AircraftId = int.Parse(this.AddAircraftDropDownList.SelectedItem.Value)
                };

                this.LegInstancesServices.AddLegInstance(legInstance);

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private bool AreDateTimesValid(DateTime departureDateTime, DateTime arrivalDateTime)
        {
            int result = DateTime.Compare(departureDateTime, arrivalDateTime);

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
            this.DepartureDateTextBox.Text = string.Empty;
            this.DepartureTimeTextBox.Text = string.Empty;
            this.ArrivalDateTextBox.Text = string.Empty;
            this.ArrivalTimeTextBox.Text = string.Empty;
            this.AddFlightLegDropDownList.SelectedIndex = 0;
            this.AddFlightStatusDropDownList.SelectedIndex = 0;
            this.AddAircraftDropDownList.SelectedIndex = 0;
        }
    }
}