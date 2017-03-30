namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(FlightLegsManagementPresenter))]
    public partial class FlightLegsManagement : MvpPage<FlightLegsManagementViewModel>, IFlightLegsManagementView
    {
        public event EventHandler OnFlightLegsGetData;

        public event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsUpdateItem;

        public event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsDeleteItem;

        public event EventHandler<FlightLegsManagementEventArgs> OnFlightLegsAddItem;

        public event EventHandler OnAirportsGetData;

        public event EventHandler OnFlightsGetData;

        public event EventHandler OnRoutesGetData;

        public event EventHandler OnLegInstancesGetData;

        public event EventHandler<FlightLegsManagementEventArgs> OnAirportGetItem;

        public IQueryable<FlightLeg> FlightLegsGridView_GetData()
        {
            this.OnFlightLegsGetData?.Invoke(null, null);

            return this.Model.FlightLegs;
        }

        public void FlightLegsGridView_UpdateItem(int id)
        {
            this.OnFlightLegsUpdateItem?.Invoke(
                null, 
                new FlightLegsManagementEventArgs()
                {
                    Id = id,
                    DepartureAirportId = int.Parse(this.DepartureAirportIdHiddenField.Value),
                    ArrivalAirportId = int.Parse(this.ArrivalAirportIdHiddenField.Value),
                    FlightId = int.Parse(this.FlightIdHiddenField.Value),
                    RouteId = int.Parse(this.RouteIdHiddenField.Value)
                });
        }

        public void FlightLegsGridView_DeleteItem(int id)
        {
            this.OnFlightLegsDeleteItem?.Invoke(null, new FlightLegsManagementEventArgs() { Id = id });
        }

        public IQueryable<object> AirportsDropDownList_GetData()
        {
            this.OnAirportsGetData?.Invoke(null, null);

            return this.Model.Airports; 
        }

        public IQueryable<object> FlightsDropDownList_GetData()
        {
            this.OnFlightsGetData?.Invoke(null, null);

            return this.Model.Flights; 
        }

        public IQueryable<object> RoutesDropDownList_GetData()
        {
            this.OnRoutesGetData?.Invoke(null, null);

            return this.Model.Routes;
        }

        public IQueryable<object> LegInstancesListBox_GetData()
        {
            this.OnLegInstancesGetData?.Invoke(null, null);

            return this.Model.LegInstances;
        }

        protected string GetAirport(int airportId)
        {
            this.OnAirportGetItem?.Invoke(null, new FlightLegsManagementEventArgs() { AirportId = airportId });

            return this.Model.AirportInfo;
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
                var flightLegEventArgs = new FlightLegsManagementEventArgs()
                {
                    DepartureAirportId = int.Parse(this.AddDepartureAirportDropDownList.SelectedItem.Value),
                    ScheduledDepartureDateTime = scheduledDepartureDateTime,
                    ArrivalAirportId = int.Parse(this.AddArrivalAirportDropDownList.SelectedItem.Value),
                    ScheduledArrivalDateTime = scheduledArrivalDateTime,
                    FlightId = int.Parse(this.AddFlightDropDownList.SelectedItem.Value),
                    RouteId = int.Parse(this.AddRoutesDropDownList.SelectedItem.Value)
                };

                this.OnFlightLegsAddItem?.Invoke(sender, flightLegEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedFlightLegIdLiteral.Text = flightLegEventArgs.Id.ToString();

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