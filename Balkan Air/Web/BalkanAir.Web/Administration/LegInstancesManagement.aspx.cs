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
    
    [PresenterBinding(typeof(LegInstancesManagementPresenter))]
    public partial class LegInstancesManagement : MvpPage<LegInstancesManagementViewModel>, ILegInstancesManagementView
    {
        public event EventHandler OnLegInstancesGetData;

        public event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesUpdateItem;

        public event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesDeleteItem;

        public event EventHandler<LegInstancesManagementEventArgs> OnLegInstancesAddItem;

        public event EventHandler OnFlightLegsGetData;

        public event EventHandler OnFlightStatusesGetData;

        public event EventHandler OnAircraftsGetData;

        public event EventHandler OnFaresGetData;

        public event EventHandler<LegInstancesManagementEventArgs> OnAirportInfoGetItem;

        public event EventHandler<LegInstancesManagementEventArgs> OnSendNotificationToSubscribedUsers;

        public event EventHandler<LegInstancesManagementEventArgs> OnSendMailToSubscribedUsers;

        public IQueryable<LegInstance> LegInstancesGridView_GetData()
        {
            this.OnLegInstancesGetData?.Invoke(null, null);

            return this.Model.LegInstances;
        }

        public void LegInstancesGridView_UpdateItem(int id)
        {
            var legInstanceEventArgs = new LegInstancesManagementEventArgs()
            {
                Id = id,
                FlightLegId = int.Parse(this.FlightLegIdHiddenField.Value),
                FlightStatusId = int.Parse(this.FlightStatusIdHiddenField.Value),
                AircraftId = int.Parse(this.AircraftIdHiddenField.Value)
            };

            this.OnLegInstancesUpdateItem?.Invoke(null, legInstanceEventArgs);
        }

        public void LegInstancesGridView_DeleteItem(int id)
        {
            this.OnLegInstancesDeleteItem?.Invoke(null, new LegInstancesManagementEventArgs() { Id = id });
        }

        public IQueryable<object> FlightLegDropDownList_GetData()
        {
            this.OnFlightLegsGetData?.Invoke(null, null);

            return this.Model.FlightLegs; 
        }

        public IQueryable<FlightStatus> FlightStatusDropDownList_GetData()
        {
            this.OnFlightStatusesGetData?.Invoke(null, null);

            return this.Model.FlightStatuses;
        }

        public IQueryable<object> AircraftDropDownList_GetData()
        {
            this.OnAircraftsGetData?.Invoke(null, null);

            return this.Model.Aircrafts;
        }

        public IQueryable<object> FaresDropDownList_GetData()
        {
            this.OnFaresGetData?.Invoke(null, null);

            return this.Model.Fares;
        }

        protected string GetAirport(int airportId)
        {
            this.OnAirportInfoGetItem.Invoke(null, new LegInstancesManagementEventArgs() { AirportId = airportId });

            return this.Model.AirportInfo;
        }

        protected void CreateLegInstancetBtn_Click(object sender, EventArgs e)
        {
            string seconds = ":00";

            // Convert string to DateTime. The string should look like this: 01/08/2008 14:50:00
            string stringDepartureDateTime = this.LegInstanceDepartureDateTextBox.Text + " " + 
                this.DepartureTimeTextBox.Text + seconds;

            string stringArrivalDateTime = this.LegInstanceArrivalDateTextBox.Text + " " + 
                this.ArrivalTimeTextBox.Text + seconds;

            DateTime departureDateTime = Convert.ToDateTime(stringDepartureDateTime);
            DateTime arrivalDateTime = Convert.ToDateTime(stringArrivalDateTime);

            if (this.AreDateTimesValid(departureDateTime, arrivalDateTime) && this.IsDateTimeAfterDateTimeNow(departureDateTime) &&
                this.IsDateTimeAfterDateTimeNow(departureDateTime) && this.Page.IsValid)
            {
                var legInstanceEventArgs = new LegInstancesManagementEventArgs()
                {
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    FlightLegId = int.Parse(this.AddFlightLegDropDownList.SelectedItem.Value),
                    FlightStatusId = int.Parse(this.AddFlightStatusDropDownList.SelectedItem.Value),
                    AircraftId = int.Parse(this.AddAircraftDropDownList.SelectedItem.Value),
                    FareId = int.Parse(this.AddFareDropDownList.SelectedItem.Value)
                };

                this.OnLegInstancesAddItem?.Invoke(sender, legInstanceEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedLegInstanceIdLiteral.Text = legInstanceEventArgs.Id.ToString();

                this.ClearFields();

                this.OnSendNotificationToSubscribedUsers?.Invoke(null, legInstanceEventArgs);
                this.OnSendMailToSubscribedUsers?.Invoke(null, legInstanceEventArgs);
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
            this.LegInstanceDepartureDateTextBox.Text = string.Empty;
            this.DepartureTimeTextBox.Text = string.Empty;
            this.LegInstanceArrivalDateTextBox.Text = string.Empty;
            this.ArrivalTimeTextBox.Text = string.Empty;
            this.AddFlightLegDropDownList.SelectedIndex = 0;
            this.AddFlightStatusDropDownList.SelectedIndex = 0;
            this.AddAircraftDropDownList.SelectedIndex = 0;
        }
    }
}