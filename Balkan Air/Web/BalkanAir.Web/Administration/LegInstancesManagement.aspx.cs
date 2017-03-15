namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web.UI;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class LegInstancesManagement : Page
    {
        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFaresServices FaresServices { get; set; }

        [Inject]
        public IFlightLegsServices FlightLegsServices { get; set; }

        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

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

        public IQueryable<object> FaresDropDownList_GetData()
        {
            return this.FaresServices.GetAll()
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.Price)
                .Select(f => new
                {
                    Id = f.Id,
                    FareInfo = "Id:" + f.Id + ", " + f.Route.Origin.Name + " (" + f.Route.Origin.Abbreviation + ") -> " +
                               f.Route.Destination.Name + " (" + f.Route.Destination.Abbreviation + "), \u20AC" + f.Price
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
            string stringDepartureDateTime = this.LegInstanceDepartureDateTextBox.Text + " " + 
                this.DepartureTimeTextBox.Text + seconds;

            string stringArrivalDateTime = this.LegInstanceArrivalDateTextBox.Text + " " + 
                this.ArrivalTimeTextBox.Text + seconds;

            DateTime departureDateTime = Convert.ToDateTime(stringDepartureDateTime);
            DateTime arrivalDateTime = Convert.ToDateTime(stringArrivalDateTime);

            if (this.AreDateTimesValid(departureDateTime, arrivalDateTime) && this.IsDateTimeAfterDateTimeNow(departureDateTime) &&
                this.IsDateTimeAfterDateTimeNow(departureDateTime) && this.Page.IsValid)
            {
                int fareId = int.Parse(this.AddFareDropDownList.SelectedItem.Value);
                decimal price = this.FaresServices.GetFare(fareId).Price;

                var legInstance = new LegInstance()
                {
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    Price = price,
                    FlightLegId = int.Parse(this.AddFlightLegDropDownList.SelectedItem.Value),
                    FlightStatusId = int.Parse(this.AddFlightStatusDropDownList.SelectedItem.Value),
                    AircraftId = int.Parse(this.AddAircraftDropDownList.SelectedItem.Value)
                };

                int id = this.LegInstancesServices.AddLegInstance(legInstance);

                this.SuccessPanel.Visible = true;
                this.AddedLegInstanceIdLiteral.Text = id.ToString();

                this.ClearFields();

                this.SendNotificationToSubscribedUsers(legInstance);
                this.SendMailToSubscribedUsers(legInstance);
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

        private void SendNotificationToSubscribedUsers(LegInstance legInstance)
        {
            var notificationId = this.AddNotification(legInstance);
            var usersIdToSendNotification = this.GetSubscribedUsersToReceiveNotification();

            this.UserNotificationsServices.SendNotification(notificationId, usersIdToSendNotification);
        }

        private void SendMailToSubscribedUsers(LegInstance legInstance)
        {
            var userEmails = this.GetSubscribedUsersToReceiveEmail();

            StringBuilder messageBody = new StringBuilder();
            messageBody.Append("Hello, Passenger,");
            messageBody.Append("<br/><br/>" + this.GetContent(legInstance));
            messageBody.Append("<br/><br/><small>If you don't want to receive emails for new flights, go to your account settings and " +
                "uncheck <strong>'Receive email when there is a new flight'</strong> option!</small>");

            var mailSender = MailSender.Instance;
            mailSender.SendMail(userEmails[0], "Added a new flight!", messageBody.ToString(), userEmails);
        }

        private int AddNotification(LegInstance legInstance)
        {
            StringBuilder content = new StringBuilder();
            content.Append(this.GetContent(legInstance));
            content.Append("<br/><small>If you don't want to receive notifications for new flights, go to your account settings and " +
                "uncheck <strong>'Receive notification when there is a new flight'</strong> option!</small>");

            var addedNewNewsNotification = new Notification()
            {
                Content = content.ToString(),
                DateCreated = DateTime.Now,
                Type = NotificationType.AddedNewNews
            };

            this.NotificationsServices.AddNotification(addedNewNewsNotification);

            return addedNewNewsNotification.Id;
        }

        private IEnumerable<string> GetSubscribedUsersToReceiveNotification()
        {
            return this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.ReceiveNotificationWhenNewFlight)
                .Select(u => u.Id)
                .ToList();
        }

        private IList<string> GetSubscribedUsersToReceiveEmail()
        {
            return this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.ReceiveEmailWhenNewFlight)
                .Select(u => u.Email)
                .ToList();
        }

        private string GetContent(LegInstance legInstance)
        {
            var flightLeg = this.GetFlightLeg(legInstance.FlightLegId);
            string origin = this.GetAirport(flightLeg.DepartureAirportId);
            string destination = this.GetAirport(flightLeg.ArrivalAirportId);
            string date = legInstance.DepartureDateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string time = legInstance.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture);

            return string.Format(@"Added a new flight ""{0}, from {1} to {2}, departure on {3} at {4}""!",
                flightLeg.Flight.Number, origin, destination, date, time);
        }

        private FlightLeg GetFlightLeg(int id)
        {
            return this.FlightLegsServices.GetFlightLeg(id);
        }
    }
}