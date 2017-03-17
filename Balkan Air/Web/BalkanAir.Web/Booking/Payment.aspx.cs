namespace BalkanAir.Web.Booking
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Common;
    using Data;
    using Data.Helper;
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

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public INumberGenerator NumberGenerator { get; set; }

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
                    this.BindMontsDropDown(DateTime.Now.Month);
                    this.BindYearsDropDowns();

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

        protected void YearsPaymentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var yearsDropDown = sender as DropDownList;

            if (yearsDropDown != null)
            {
                int selectedYear = int.Parse(yearsDropDown.SelectedItem.Value);
                int startMonth = 1;

                if (selectedYear == DateTime.Now.Year)
                {
                    startMonth = DateTime.Now.Month;
                }
               
                this.BindMontsDropDown(startMonth);
            }
        }

        protected void PayAndBookNowBtn_Click(object sender, EventArgs e)
        {
            this.BookNow(this.OneWayRouteBooking);

            if (this.ReturnRouteBooking != null)
            {
                this.BookNow(this.ReturnRouteBooking);
            }

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
            if (booking == null || booking.LegInstanceId == 0 || booking.TravelClassId == 0 || 
                booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
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

        private void BindMontsDropDown(int startMonth)
        {
            int lastMonth = 12;

            this.MonthsPaymentDropDown.DataSource = Enumerable.Range(startMonth, lastMonth - startMonth + 1);
            this.MonthsPaymentDropDown.DataBind();
        }

        private void BindYearsDropDowns()
        {
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
            booking.ConfirmationCode = this.NumberGenerator.GetUniqueBookingConfirmationCode();
            booking.Status = BookingStatus.Unconfirmed;

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

            this.SendFlightBookedNotification();
            this.SendFlightConfirmationMail(booking);
        }

        private void SendFlightBookedNotification()
        {
            var flightBookedNotification = this.NotificationsServices.GetAll()
                .FirstOrDefault(n => n.Type == NotificationType.FlightBooked);

            if (flightBookedNotification != null)
            {
                this.UserNotificationsServices.SendNotification(flightBookedNotification.Id, this.CurrentUser.Id);
            }
        }

        private void SendFlightConfirmationMail(Booking booking)
        {
            string purpose = TokenPurposes.FLIGHT_CONFIRMATION + "#" + booking.ConfirmationCode;
            string code = this.Manager.GenerateUserToken(purpose, this.CurrentUser.Id);
            string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, this.CurrentUser.Id, this.Request);
            string messageBody = this.GetMessageBody(callbackUrl, booking);

            var mailSender = MailSender.Instance;
            mailSender.SendMail(this.CurrentUser.Email, "Confirm your flight!", messageBody);
        }
        
        private string GetMessageBody(string callbackUrl, Booking booking)
        {
            var passenger = this.CurrentUser.UserSettings.FirstName + " " + this.CurrentUser.UserSettings.LastName;

            StringBuilder messageBody = new StringBuilder();
            messageBody.Append("Dear, " + passenger.Trim() + ",");
            messageBody.Append("<br /><br />Thank you for booking with Balkan Air. " +
                "The departure of your flight is fast approaching. You can find in your " +
                "profile some useful information as well as a reminder of your flight dates and times!");
            messageBody.Append("<br /><br /><strong>FLIGHT DETAILS</strong>");
            messageBody.Append(this.GetFlightDetails(booking));
            messageBody.Append("<br /><br /><strong>Please, click the following link to confirm your flight!</strong>");
            messageBody.Append("<br /><a href =\"" + callbackUrl + "\">Click here to confirm your flight.</a>");

            return messageBody.ToString();
        }

        private string GetFlightDetails(Booking booking)
        {
            var legInstance = this.LegInstancesServices.GetLegInstance(booking.LegInstanceId);

            StringBuilder flightDetails = new StringBuilder();
            flightDetails.Append("<br />Confirmation Code: " + booking.ConfirmationCode);
            flightDetails.Append("<br />Flight: " + legInstance.FlightLeg.Flight.Number);
            flightDetails.Append("<br />Origin: " + legInstance.FlightLeg.Route.Origin.Name + " (" + 
                                                    legInstance.FlightLeg.Route.Origin.Abbreviation + ")");
            flightDetails.Append("<br />Destination: " + legInstance.FlightLeg.Route.Destination.Name + " (" + 
                                                         legInstance.FlightLeg.Route.Destination.Abbreviation + ")");
            flightDetails.Append("<br />Departure: " + legInstance.DepartureDateTime
                                                       .ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture));

            return flightDetails.ToString();
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