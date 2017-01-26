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
        private User user;
        private Booking booking;

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        [Inject]
        public ICreditCardsServices CreditCardsServices { get; set; }

        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.user = GetCurrentUser();
            this.booking = (Booking)this.Session[Parameters.BOOKING];

            if (!this.Page.IsPostBack)
            {
                if (this.booking == null || this.booking.FlightId == 0 || this.booking.TravelClassId == 0 || 
                    this.booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
                {
                    this.Response.Redirect(Pages.HOME);
                }

                if (this.user == null || !user.AreProfileDetailsFilled)
                {
                    this.FilledProfileDetailsRequiredPanel.Visible = true;
                    this.PaymentDetailsPanel.Visible = false;
                }
                else
                {
                    this.FilledProfileDetailsRequiredPanel.Visible = false;
                    this.PaymentDetailsPanel.Visible = true;
                    this.FillPaymentDetailsFields(this.user);
                    this.BindCardDateExpirationDropDowns();
                    this.TotalPriceLabel.Text = "Total price: &#8364; " + this.booking.TotalPrice;
                }
            }
        }

        protected void PayAndBookNowBtn_Click(object sender, EventArgs e)
        {
            this.TravelClassesServices.BookSeat(this.booking.TravelClassId, this.booking.Row, this.booking.SeatNumber);

            this.booking.DateOfBooking = DateTime.Now;
            this.booking.UserId = this.user.Id;
            this.BookingsServices.AddBooking(this.booking);

            if (this.SaveCreditCardCheckBox.Checked)
            {
                this.SaveCreditCard();
            }

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

        private User GetCurrentUser()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return manager.FindById(this.User.Identity.GetUserId());
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
                UserId = this.user.Id
            };

            this.CreditCardsServices.Create(newCreditCard);
        }
    }
}