namespace BalkanAir.Web.Booking
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class Payment : Page
    {
        private Booking booking;

        [Inject]
        public ICountriesServices CountriesServices { get; set; }

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

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

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
            this.booking = (Booking)this.Session[Common.NativeConstants.BOOKING];

            if (!this.Page.IsPostBack)
            {
                if (this.booking == null ||/* this.booking.FlightId == 0 ||*/ this.booking.TravelClassId == 0 || 
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
                    this.BindNationalityDropDownList();
                    this.FilledProfileDetailsRequiredPanel.Visible = false;
                    this.PaymentDetailsPanel.Visible = true;
                    this.FillPaymentDetailsFields();
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

            var seat = new Seat()
            {
                Row = booking.Row,
                Number = booking.SeatNumber,
                TravelClassId = booking.TravelClassId,
                IsReserved = true,
                LegInstanceId = booking.LegInstanceId
            };

            this.SeatsServices.AddSeat(seat);

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
            this.Response.AddHeader("REFRESH", "5;URL=" + VirtualPathUtility.ToAbsolute(Pages.ACCOUNT));
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

        private void FillPaymentDetailsFields()
        {
            this.FirstNamePaymentTextBox.Text = this.CurrentUser.UserSettings.FirstName;
            this.LastNamePaymentTextBox.Text = this.CurrentUser.UserSettings.LastName;
            this.EmailPaymentTextBox.Text = this.CurrentUser.Email;
            this.AddressPaymentTextBox.Text = this.CurrentUser.UserSettings.FullAddress;
            this.PhoneNumberPaymentTextBox.Text = this.CurrentUser.PhoneNumber;

            int countryId = this.CountriesServices.GetAll()
                    .FirstOrDefault(c => c.Name.ToLower() == this.CurrentUser.UserSettings.Nationality.ToLower())
                    .Id;

            this.NationalityDropDownList.SelectedValue = countryId.ToString();
        }

        private void BindNationalityDropDownList()
        {
            this.NationalityDropDownList.DataSource = this.CountriesServices.GetAll()
                .OrderBy(c => c.Name)
                .ToList();
            this.NationalityDropDownList.DataBind();

            if (string.IsNullOrEmpty(this.CurrentUser.UserSettings.Nationality))
            {
                this.NationalityDropDownList.Items.Insert(
                    NativeConstants.NATIONALITY_NOT_SELECTED_INDEX,
                    new ListItem(
                        NativeConstants.NATIONALITY_NOT_SELECTED_TEXT,
                        NativeConstants.NATIONALITY_NOT_SELECTED_INDEX.ToString()
                    )
                );
            }
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