namespace BalkanAir.Web.Booking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using BalkanAir.Data.Models;
    using BalkanAir.Services.Data.Contracts;
    using BalkanAir.Web.Common;

    public partial class SelectSeat : Page
    {
        private Flight selectedFlight;

        [Inject]
        public IAirportsServices AirportsServices { get; set; }
        
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

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
                    this.selectedFlight = this.FlightsServices.GetFlight(booking.FlightId);
                    this.FromAirportLabel.Text = this.selectedFlight.DepartureAirport.Name;
                    this.ToAirportLabel.Text = this.selectedFlight.ArrivalAirport.Name;
                    this.SelectedTravelClassLabel.Text = this.selectedFlight.TravelClasses
                        .FirstOrDefault(t => t.Id == booking.TravelClassId)
                        .Type
                        .ToString();

                    if (booking.Row != 0 && !string.IsNullOrEmpty(booking.SeatNumber))
                    {
                        this.SelectedRowAndSeatLabel.Text = "Seat: " + booking.Row + booking.SeatNumber;
                        this.SelectedRowHiddenField.Value = booking.Row.ToString();
                        this.SelectedSeatHiddenField.Value = booking.SeatNumber;
                    }
                    else
                    {
                        this.SelectedRowAndSeatLabel.Text = "Seat: No seat selected!";
                    }
                }
            }
        }

        public IEnumerable<Seat> SeatRepeater_GetData()
        {
            return this.selectedFlight.TravelClasses
                .SelectMany(a => a.Seats)
                .OrderByDescending(a => a.TravelClass.Type == TravelClassType.First)
                .ThenByDescending(a => a.TravelClass.Type == TravelClassType.Business)
                .ToList();
        }

        protected void ContinueBookingBtn_Click(object sender, EventArgs e)
        {
            Booking booking = (Booking)this.Session[Parameters.BOOKING];
            booking.Row = int.Parse(this.SelectedRowHiddenField.Value);
            booking.SeatNumber = this.SelectedSeatHiddenField.Value;

            this.Session.Add(Parameters.BOOKING, booking);
            this.Response.Redirect(Pages.EXTRAS);
        }
    }
}