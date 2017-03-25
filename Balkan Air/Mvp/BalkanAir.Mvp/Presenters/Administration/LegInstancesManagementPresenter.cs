namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using WebFormsMvp;

    using Common;
    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;
    using Web.Common;

    public class LegInstancesManagementPresenter : Presenter<ILegInstancesManagementView>
    {
        private readonly ILegInstancesServices legInstancesServices;
        private readonly IFlightLegsServices flightLegsServices;
        private readonly IFlightStatusesServices flightStatusesServices;
        private readonly IAircraftsServices aircraftsServices;
        private readonly IFaresServices faresServices;
        private readonly IUserNotificationsServices userNotificationsServices;
        private readonly INotificationsServices notificationsServices;
        private readonly IUsersServices usersServices;
        private readonly IAirportsServices airportsServices;

        public LegInstancesManagementPresenter(ILegInstancesManagementView view, ILegInstancesServices legInstancesServices,
            IFlightLegsServices flightLegsServices, IFlightStatusesServices flightStatusesServices, IFaresServices faresServices,
            IAircraftsServices aircraftsServices, IUserNotificationsServices userNotificationsServices,
            INotificationsServices notificationsServices, IUsersServices usersServices, IAirportsServices airportsServices) 
            : base(view)
        {
            if (legInstancesServices == null)
            {
                throw new ArgumentNullException(nameof(ILegInstancesServices));
            }

            if (flightLegsServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightLegsServices));
            }

            if (flightLegsServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightStatusesServices));
            }

            if (aircraftsServices == null)
            {
                throw new ArgumentNullException(nameof(IAircraftsServices));
            }

            if (faresServices == null)
            {
                throw new ArgumentNullException(nameof(IFaresServices));
            }

            if (userNotificationsServices == null)
            {
                throw new ArgumentNullException(nameof(IUserNotificationsServices));
            }

            if (notificationsServices == null)
            {
                throw new ArgumentNullException(nameof(INotificationsServices));
            }

            if (usersServices == null)
            {
                throw new ArgumentNullException(nameof(IUsersServices));
            }

            if (aircraftsServices == null)
            {
                throw new ArgumentNullException(nameof(IAirportsServices));
            }

            this.legInstancesServices = legInstancesServices;
            this.flightLegsServices = flightLegsServices;
            this.flightStatusesServices = flightStatusesServices;
            this.aircraftsServices = aircraftsServices;
            this.faresServices = faresServices;
            this.userNotificationsServices = userNotificationsServices;
            this.notificationsServices = notificationsServices;
            this.usersServices = usersServices;
            this.airportsServices = airportsServices;

            this.View.OnLegInstancesGetData += this.View_OnLegInstancesGetData;
            this.View.OnLegInstancesUpdateItem += this.View_OnLegInstancesUpdateItem;
            this.View.OnLegInstancesDeleteItem += this.View_OnLegInstancesDeleteItem;
            this.View.OnLegInstancesAddItem += this.View_OnLegInstancesAddItem;
            this.View.OnFlightLegsGetData += this.View_OnFlightLegsGetData;
            this.View.OnFlightStatusesGetData += this.View_OnFlightStatusesGetData;
            this.View.OnAircraftsGetData += this.View_OnAircraftsGetData;
            this.View.OnFaresGetData += this.View_OnFaresGetData;
            this.View.OnAirportInfoGetItem += this.View_OnAirportInfoGetItem;
            this.View.OnSendNotificationToSubscribedUsers += this.View_OnSendNotificationToSubscribedUsers;
        }

        private void View_OnLegInstancesGetData(object sender, EventArgs e)
        {
            this.View.Model.LegInstances = this.legInstancesServices.GetAll();
        }

        private void View_OnLegInstancesUpdateItem(object sender, LegInstancesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(LegInstancesManagementEventArgs));
            }

            var legInstance = this.legInstancesServices.GetLegInstance(e.Id);

            if (legInstance == null)
            {
                this.View.ModelState.AddModelError(ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));
                return;
            }

            this.View.TryUpdateModel(legInstance);

            if (this.View.ModelState.IsValid)
            {
                legInstance.FlightLegId = e.FlightLegId;
                legInstance.FlightStatusId = e.FlightStatusId;
                legInstance.AircraftId = e.AircraftId;

                this.legInstancesServices.UpdateLegInstance(e.Id, legInstance);
            }
        }

        private void View_OnLegInstancesDeleteItem(object sender, LegInstancesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(LegInstancesManagementEventArgs));
            }

            this.legInstancesServices.DeleteLegInstance(e.Id);
        }

        private void View_OnLegInstancesAddItem(object sender, LegInstancesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(LegInstancesManagementEventArgs));
            }

            decimal price = this.faresServices.GetFare(e.FareId).Price;

            var legInstance = new LegInstance()
            {
                DepartureDateTime = e.DepartureDateTime,
                ArrivalDateTime = e.ArrivalDateTime,
                Price = price,
                FlightLegId = e.FlightLegId,
                FlightStatusId = e.FlightStatusId,
                AircraftId = e.AircraftId
            };

            e.Id = this.legInstancesServices.AddLegInstance(legInstance);
        }

        private void View_OnFlightLegsGetData(object sender, EventArgs e)
        {
            this.View.Model.FlightLegs = this.flightLegsServices.GetAll()
                .Where(l => !l.IsDeleted)
                .Select(l => new
                {
                    Id = l.Id,
                    FlightLegInfo = "Id:" + l.Id + " " + l.Flight.Number + ", " + " (" + l.ScheduledDepartureDateTime + ")" + 
                                    " -> " + " (" + l.ScheduledArrivalDateTime + ")"
                });
        }

        private void View_OnFlightStatusesGetData(object sender, EventArgs e)
        {
            this.View.Model.FlightStatuses = this.flightStatusesServices.GetAll()
                .OrderBy(f => f.Name);
        }

        private void View_OnAircraftsGetData(object sender, EventArgs e)
        {
            this.View.Model.Aircrafts = this.aircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = "Id:" + a.Id + ", " + a.AircraftManufacturer.Name + " " + a.Model
                });
        }

        private void View_OnFaresGetData(object sender, EventArgs e)
        {
            this.View.Model.Fares = this.faresServices.GetAll()
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.Price)
                .Select(f => new
                {
                    Id = f.Id,
                    FareInfo = "Id:" + f.Id + ", " + f.Route.Origin.Name + " (" + f.Route.Origin.Abbreviation + ") -> " +
                               f.Route.Destination.Name + " (" + f.Route.Destination.Abbreviation + "), \u20AC" + f.Price
                });
        }

        private void View_OnAirportInfoGetItem(object sender, LegInstancesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(LegInstancesManagementEventArgs));
            }

            this.View.Model.AirportInfo = this.GetAirport(e.AirportId);
        }

        private void View_OnSendNotificationToSubscribedUsers(object sender, LegInstancesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(LegInstancesManagementEventArgs));
            }

            var legInstance = this.legInstancesServices.GetLegInstance(e.Id);

            var notificationId = this.AddNotification(legInstance);
            var usersIdToSendNotification = this.GetSubscribedUsersToReceiveNotification();

            this.userNotificationsServices.SendNotification(notificationId, usersIdToSendNotification);
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

            this.notificationsServices.AddNotification(addedNewNewsNotification);

            return addedNewNewsNotification.Id;
        }

        private IEnumerable<string> GetSubscribedUsersToReceiveNotification()
        {
            return this.usersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.ReceiveNotificationWhenNewFlight)
                .Select(u => u.Id)
                .ToList();
        }

        private IList<string> GetSubscribedUsersToReceiveEmail()
        {
            return this.usersServices.GetAll()
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
            return this.flightLegsServices.GetFlightLeg(id);
        }

        private string GetAirport(int airportId)
        {
            var airport = this.airportsServices.GetAirport(airportId);

            if (airport == null)
            {
                return "Airport not found!";
            }

            return airport.Name + " (" + airport.Abbreviation + ")";
        }
    }
}