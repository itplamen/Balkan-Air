namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;
    using App_Start;

    public partial class Confirm : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        protected string StatusMessage { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string code = IdentityHelper.GetCodeFromRequest(Request);
            string userId = IdentityHelper.GetUserIdFromRequest(Request);

            if (code != null && userId != null)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

                if (this.IsFlightConfirmation(manager, userId, code))
                {
                    return;
                }

                if (this.IsEmailConfirmation(manager, userId, code))
                {
                    return;
                }
            }

            successPanel.Visible = false;
            errorPanel.Visible = true;
        }

        private bool IsFlightConfirmation(ApplicationUserManager manager, string userId, string code)
        {
            var isFlightConfirmationValid = false;

            var unconfirmedBookings = this.BookingsServices.GetAll()
                .Where(b => b.UserId == userId && b.Status == BookingStatus.Unconfirmed)
                .ToList();

            foreach (var booking in unconfirmedBookings)
            {
                string purpose = TokenPurposes.FLIGHT_CONFIRMATION + "#" + booking.ConfirmationCode;
                isFlightConfirmationValid = manager.VerifyUserToken(userId, purpose, code);

                if (isFlightConfirmationValid)
                {
                    this.ConfirmFlight(booking);
                    break;
                }
            }

            return isFlightConfirmationValid;
        }

        private void ConfirmFlight (Booking booking)
        {
            booking.Status = BookingStatus.Confirmed;
            this.BookingsServices.UpdateBooking(booking.Id, booking);

            this.Page.Title = "Flight Confirmation";

            string flight = booking.LegInstance.FlightLeg.Flight.Number;
            string from = this.GetOrigin(booking);
            string to = this.GetDestination(booking);

            this.StatusMessage = string.Format("Thank you for confirming your flight {0} from " +
                "{1} to {2}, confirmation code {3}!", flight, from, to, booking.ConfirmationCode);
        }

        private string GetOrigin(Booking booking)
        {
            return booking.LegInstance.FlightLeg.Route.Origin.Name + " (" + 
                   booking.LegInstance.FlightLeg.Route.Origin.Abbreviation + ")";
        }

        private string GetDestination(Booking booking)
        {
            return booking.LegInstance.FlightLeg.Route.Destination.Name + " (" +
                   booking.LegInstance.FlightLeg.Route.Destination.Abbreviation + ")";
        }

        private bool IsEmailConfirmation(ApplicationUserManager manager, string userId, string code)
        {
            var result = manager.ConfirmEmail(userId, code);

            if (result.Succeeded)
            {
                this.Page.Title = "Account Confirmation";
                this.StatusMessage = "Thank you for confirming your account.";
                successPanel.Visible = true;

                bool didUserReceivedSetAccountNotification = this.UserNotificationsServices.GetAll()
                        .Where(un => un.UserId.Equals(userId) && un.Notification.Type == NotificationType.AccountConfirmation)
                        .Any();

                if (!didUserReceivedSetAccountNotification)
                {
                    var accountConfirmationNotification = this.NotificationsServices.GetAll()
                        .FirstOrDefault(n => n.Type == NotificationType.AccountConfirmation);

                    this.UserNotificationsServices.SendNotification(accountConfirmationNotification.Id, userId);
                }
            }

            return result.Succeeded;
        }
    }
}