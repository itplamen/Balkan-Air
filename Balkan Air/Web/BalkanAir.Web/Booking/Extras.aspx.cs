namespace BalkanAir.Web.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using BalkanAir.Web.Common;
    using BalkanAir.Data.Models;
    using Data.Services.Contracts;
    using Ninject;

    public partial class Extras : Page
    {
        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                Booking booking = (Booking)this.Session[Parameters.BOOKING];

                if (booking == null || (booking != null && booking.FlightId == 0 || booking.TravelClassId == 0))
                {
                    this.Response.Redirect(Pages.HOME);
                }
                else
                {
                    if (booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
                    {
                        this.SelectedSeatLabel.Visible = false;
                        this.SelectedSeatImage.Visible = false;
                        this.SelectSeatBtn.Text = "SELECT SEAT";
                        this.ContinueBookingBtn.Visible = false;
                    }
                    else
                    {
                        this.SelectedSeatLabel.Visible = true;
                        this.SelectedSeatImage.Visible = true;
                        this.SelectedSeatLabel.Text = booking.Row + booking.SeatNumber;
                        this.SelectSeatBtn.Text = "CHANGE";
                        this.ContinueBookingBtn.Visible = true;
                    }
                }
            }
        }

        protected void SelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            Booking booking = (Booking)this.Session[Parameters.BOOKING];
            booking.Baggages.Add(new Baggage()
            {
                Type = BaggageType.Cabin,
                Size = this.SelectedCabinBagSize.Value,
                Price = decimal.Parse(this.SelectedCabinBagPrice.Value)
            });

            int numberOfCheckedInBags = int.Parse(this.NumberOfCheckedInBags.Value);

            if (numberOfCheckedInBags > 0)
            {
                for (int i = 1; i <= numberOfCheckedInBags; i++)
                {
                    booking.Baggages.Add(new Baggage()
                    {
                        Type = BaggageType.CheckedIn,
                        MaxKilograms = int.Parse(this.SelectedCheckedInBagWeight.Value),
                        Price = decimal.Parse(this.SelectedCheckedInBagPrice.Value)
                    });
                }
            }

            if (this.BabyEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(booking, BaggageType.BabyEquipment, 10);
            }

            if (this.SportsEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(booking, BaggageType.SportsEquipment, 30);
            }

            if (this.MusicEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(booking, BaggageType.MusicEquipment, 50);
            }

            this.CalculateTotalPriceOfBooking(booking);

            this.Session.Add(Parameters.BOOKING, booking);
            this.Response.Redirect(Pages.PAYMENT);
        }

        private void AddOtherBaggagesToBooking(Booking booking, BaggageType type, decimal price)
        {
            booking.Baggages.Add(new Baggage()
            {
                Type = type,
                Price = price
            });
        }

        private void CalculateTotalPriceOfBooking(Booking booking)
        {
            booking.TotalPrice = booking.Baggages.Sum(b => b.Price) + this.TravelClassesServices
                .GetTravelClass(booking.TravelClassId)
                .Price;
        }
    }
}