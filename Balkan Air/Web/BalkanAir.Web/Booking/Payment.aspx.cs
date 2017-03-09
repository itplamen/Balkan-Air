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

        private Booking OneWayRouteBooking
        {
            get
            {
                return (Booking)this.Session[Common.Constants.ONE_WAY_ROUTE_BOOKING];
            }
        }

        private Booking ReturnRouteBooking
        {
            get
            {
                return (Booking)this.Session[Common.Constants.RETURN_ROUTE_BOOKING];
            }
        }

        private ApplicationUserManager Manager
        {
            get
            {
                return Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private User CurrentUser
        {
            get
            {
                return this.Manager.FindById(this.Context.User.Identity.GetUserId());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!this.IsBookingValid(this.OneWayRouteBooking) || this.ReturnRouteBooking != null && 
                    !this.IsBookingValid(this.ReturnRouteBooking))
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

                    decimal totalPrice = 0;

                    this.CalculateTotalPrice(this.OneWayRouteBooking);
                    totalPrice += this.OneWayRouteBooking.TotalPrice;
                        
                    if (this.ReturnRouteBooking != null)
                    {
                        this.CalculateTotalPrice(this.ReturnRouteBooking);
                        totalPrice += this.ReturnRouteBooking.TotalPrice;
                    }

                    this.TotalPriceLabel.Text = "Total price: &#8364; " + totalPrice;
                }
            }
        }

        protected void PayAndBookNowBtn_Click(object sender, EventArgs e)
        {
            this.BookNow(this.OneWayRouteBooking);

            if (this.ReturnRouteBooking != null)
            {
                this.BookNow(this.ReturnRouteBooking);
            }

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

        private bool IsBookingValid(Booking booking)
        {
            if (this.OneWayRouteBooking == null || this.OneWayRouteBooking.LegInstanceId == 0 ||
                    this.OneWayRouteBooking.TravelClassId == 0 || this.OneWayRouteBooking.Row == 0 ||
                    string.IsNullOrEmpty(this.OneWayRouteBooking.SeatNumber))
            {
                return false;
            }

            return true;
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
                    Common.Constants.NATIONALITY_NOT_SELECTED_INDEX,
                    new ListItem(
                        Common.Constants.NATIONALITY_NOT_SELECTED_TEXT,
                        Common.Constants.NATIONALITY_NOT_SELECTED_INDEX.ToString()
                    )
                );
            }
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

        private void CalculateTotalPrice(Booking booking)
        {
            var travelClassPrice = booking.LegInstance.Aircraft.TravelClasses
                .FirstOrDefault(t => t.Id == booking.TravelClassId)
                .Price;

            decimal totalPrice = booking.LegInstance.Price + travelClassPrice;

            foreach (var bag in booking.Baggage)
            {
                totalPrice += bag.Price;
            }

            booking.TotalPrice = totalPrice;
        }

        private void BookNow(Booking booking)
        {
            // LegInstance must be null before adding booking into database, in order to avoid the exception: 
            // "Entity object cannot be referenced by multiple instances of IEntityChangeTracker."

            booking.LegInstance = null;
            booking.DateOfBooking = DateTime.Now;
            booking.UserId = this.CurrentUser.Id;
           
            this.BookingsServices.AddBooking(booking);

            var seat = new Seat()
            {
                Row = booking.Row,
                Number = booking.SeatNumber,
                TravelClassId = booking.TravelClassId,
                IsReserved = true,
                LegInstanceId = booking.LegInstanceId
            };

            this.SeatsServices.AddSeat(seat);
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
    }
}