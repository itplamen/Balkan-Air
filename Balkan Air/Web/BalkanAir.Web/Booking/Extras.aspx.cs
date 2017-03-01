namespace BalkanAir.Web.Booking
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class Extras : Page
    {
        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }
        
        protected Route RouteInfo
        {
            get
            {
                return this.LegInstancesServices.GetLegInstance(this.OneWayRouteBooking.LegInstanceId)
                    .FlightLeg
                    .Route;
            }
        }

        private Booking OneWayRouteBooking
        {
            get
            {
                return (Booking)this.Session[NativeConstants.ONE_WAY_ROUTE_BOOKING];
            }
        }

        private Booking ReturnRouteBooking
        {
            get
            {
                return (Booking)this.Session[NativeConstants.RETURN_ROUTE_BOOKING];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.OneWayRouteBooking == null || (this.OneWayRouteBooking != null && 
                    (this.OneWayRouteBooking.LegInstanceId == 0 || this.OneWayRouteBooking.TravelClassId == 0)))
                {
                    this.Response.Redirect(Pages.HOME);
                }
                else if (this.ReturnRouteBooking != null && (this.ReturnRouteBooking.LegInstanceId == 0 ||
                    this.ReturnRouteBooking.TravelClassId == 0))
                {
                    this.Response.Redirect(Pages.HOME);
                }
                else
                {
                    this.ManageSeatSelection(this.OneWayRouteBooking, this.OneWayRouteSelectedSeatLabel,
                        this.OneWayRouteSelectedSeatImage, this.OneWayRouteSelectSeatBtn);

                    if (this.ReturnRouteBooking != null)
                    {
                        this.ReturnRouteExtrasPanel.Visible = true;

                        this.ManageSeatSelection(this.ReturnRouteBooking, this.ReturnRouteSelectedSeatLabel,
                            this.ReturnRouteSelectedSeatImage, this.ReturnRouteSelectSeatBtn);
                    }
                }
            }
        }

        protected void OneWayRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT, true);
            this.Session.Add(NativeConstants.RETURN_ROUTE_SELECT_SEAT, false);

            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void ReturnRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.Session.Add(NativeConstants.RETURN_ROUTE_SELECT_SEAT, true);
            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT, false);
            
            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            this.OneWayRouteBooking.Baggages.Add(new Baggage()
            {
                Type = BaggageType.Cabin,
                Size = this.OneWayRouteSelectedCabinBagSizeHiddenField.Value,
                Price = decimal.Parse(this.OneWayRouteSelectedCabinBagPriceHiddenField.Value)
            });

            int numberOfCheckedInBags = int.Parse(this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value);

            if (numberOfCheckedInBags > 0)
            {
                for (int i = 1; i <= numberOfCheckedInBags; i++)
                {
                    this.OneWayRouteBooking.Baggages.Add(new Baggage()
                    {
                        Type = BaggageType.CheckedIn,
                        MaxKilograms = int.Parse(this.OneWayRouteSelectedCheckedInBagWeightHiddenField.Value),
                        Price = decimal.Parse(this.OneWayRouteSelectedCheckedInBagPriceHiddenField.Value)
                    });
                }
            }

            if (this.OneWayRouteBabyEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.BabyEquipment, 10);
            }

            if (this.OneWayRouteSportsEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.SportsEquipment, 30);
            }

            if (this.OneWayRouteMusicEquipmentCheckBox.Checked)
            {
                this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.MusicEquipment, 50);
            }

            this.CalculateTotalPriceOfBooking(this.OneWayRouteBooking);

            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_BOOKING, this.OneWayRouteBooking);
            this.Response.Redirect(Pages.PAYMENT);
        }

        private void ManageSeatSelection(Booking booking, Label selectedSeatLabel, Image selectedSeatImg, Button selectedSeatBtn)
        {
            if (booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
            {
                selectedSeatLabel.Visible = false;
                selectedSeatImg.Visible = false;
                selectedSeatBtn.Text = "SELECT SEAT";
                this.ContinueBookingBtn.Visible = false;
            }
            else
            {
                selectedSeatLabel.Visible = true;
                selectedSeatImg.Visible = true;
                selectedSeatLabel.Text = booking.Row + booking.SeatNumber;
                selectedSeatBtn.Text = "CHANGE";
                this.ContinueBookingBtn.Visible = true;
            }
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