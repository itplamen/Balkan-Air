namespace BalkanAir.Web.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Ninject;

    using BalkanAir.Web.Common;
    using Data.Models;
    using BalkanAir.Services.Data.Contracts;

    public partial class Payment : Page
    {
        private Booking booking;

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        [Inject]
        public ICreditCardsServices CreditCardsServices { get; set; }

        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.booking = (Booking)this.Session[Parameters.BOOKING];

            if (!this.Page.IsPostBack)
            {
                if (this.booking == null || this.booking.FlightId == 0 || this.booking.TravelClassId == 0 || 
                    this.booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
                {
                    this.Response.Redirect(Pages.HOME);
                }

                if (this.CurrentUser == null || !this.CurrentUser.AreProfileDetailsFilled)
                {
                    this.FilledProfileDetailsRequiredPanel.Visible = true;
                    this.PaymentDetailsPanel.Visible = false;
                }
                else
                {
                    this.FilledProfileDetailsRequiredPanel.Visible = false;
                    this.PaymentDetailsPanel.Visible = true;
                    this.FillPaymentDetailsFields(this.CurrentUser);
                    this.BindCardDateExpirationDropDowns();
                    this.TotalPriceLabel.Text = "Total price: &#8364; " + this.booking.TotalPrice;
                }
            }
        }

        protected void PayAndBookNowBtn_Click(object sender, EventArgs e)
        {
            this.TravelClassesServices.BookSeat(this.booking.TravelClassId, this.booking.Row, this.booking.SeatNumber);

            this.booking.DateOfBooking = DateTime.Now;
            this.booking.UserId = this.CurrentUser.Id;
            this.BookingsServices.AddBooking(this.booking);

            var passenger = this.CurrentUser.UserSettings.FirstName + " " + this.CurrentUser.UserSettings.LastName;
            this.SendFlightConfirmationMail(passenger);

            this.SendFlightBookedNotification(this.CurrentUser.Id);

            if (this.SaveCreditCardCheckBox.Checked)
            {
                this.SaveCreditCard();
            }

            this.Session.Clear();
            this.PaymentDonePanel.Visible = true;
            this.PaymentDetailsPanel.Visible = false;
            this.Response.AddHeader("REFRESH", "3;URL=" + Pages.HOME);
        }

        private void BindCardDateExpirationDropDowns()
        {
            int startingMonth = 1;
            int numberOfMonths = 12;
            this.MonthsPaymentDropDown.DataSource = Enumerable.Range(startingMonth, numberOfMonths);
            this.MonthsPaymentDropDown.DataBind();

            int startingYear = DateTime.Now.Year;
            int numberOfYears = 10;
            this.YearsPaymentDropDown.DataSource = Enumerable.Range(startingYear, numberOfYears);
            this.YearsPaymentDropDown.DataBind();
        }

        private void FillPaymentDetailsFields(User user)
        {
            this.FirstNamePaymentTextBox.Text = user.UserSettings.FirstName;
            this.LastNamePaymentTextBox.Text = user.UserSettings.LastName;
            this.EmailPaymentTextBox.Text = user.Email;
            this.AddressPaymentTextBox.Text = user.UserSettings.FullAddress;
            this.PhoneNumberPaymentTextBox.Text = user.PhoneNumber;
        }

        private void SaveCreditCard()
        {
            CreditCard newCreditCard = new CreditCard()
            {
                Number = this.CardNumberPaymentTextBox.Text,
                NameOnCard = this.NameOnCardPaymentTextBox.Text,
                ExpirationMonth = int.Parse(this.MonthsPaymentDropDown.SelectedItem.Value),
                ExpirationYear = int.Parse(this.YearsPaymentDropDown.SelectedItem.Value),
                CvvNumber = this.CvvPaymentTextBox.Text,
                UserId = this.CurrentUser.Id
            };

            this.CreditCardsServices.Create(newCreditCard);
        }

        private void SendFlightConfirmationMail(string passenger)
        {
            string messageBody = "Dear, " + passenger.Trim() + ",";
            messageBody += "<br /><br />Thank you for booking with Bulgaria Air. The departure of your flight is fast approaching. "
                + "You can find in your profile some useful information as well as a reminder of your flight dates and times!";

            var mailSender = MailSender.Instance;
            mailSender.SendMail(this.CurrentUser.Email, "Flight reminder!", messageBody);
        }

        private void SendFlightBookedNotification(string userId)
        {
            var flightBookedNotification = this.NotificationsServices.GetAll()
                .FirstOrDefault(n => n.Type == NotificationType.FlightBooked);

            if (flightBookedNotification != null)
            {
                this.UserNotificationsServices.SendNotification(flightBookedNotification.Id, userId);
            }
        }
    }
}