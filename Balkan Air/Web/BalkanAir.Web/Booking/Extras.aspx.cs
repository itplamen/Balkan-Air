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
                this.AddAttributesToRadioButtons();
                this.SaveAllBagsToSessionBeforeSeatSelection();

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

        protected void Page_PreRender(Object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.SaveBagSelectionAfterSeatSelection(this.OneWayRouteCheckedInBaggagePanel,
                    NativeConstants.ONE_WAY_ROUTE_SELECTED_CHECKED_IN_BAG);

                this.SaveBagSelectionAfterSeatSelection(this.OneWayRouteCabinBaggagePanel,
                    NativeConstants.ONE_WAY_ROUTE_SELECTED_CABIN_BAG);

                this.SaveOtherBagsSelectionAfterSeatSelection(this.OneWayRouteOtherBaggagePanel);

                if (this.Session[this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID] != null)
                {
                    this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value =
                        this.Session[this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID].ToString();
                }

                if (this.ReturnRouteBooking != null)
                {
                    this.SaveBagSelectionAfterSeatSelection(this.ReturnRouteCheckedInBaggagePanel,
                        NativeConstants.RETURN_ROUTE_SELECTED_CHECKED_IN_BAG);

                    this.SaveBagSelectionAfterSeatSelection(this.ReturnRouteCabinBaggagePanel,
                        NativeConstants.RETURN_ROUTE_SELECTED_CABIN_BAG);

                    this.SaveOtherBagsSelectionAfterSeatSelection(this.ReturnRouteOtherBaggagePanel);

                    if (this.Session[this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID] != null)
                    {
                        this.ReturnRouteNumberOfCheckedInBagsHiddenField.Value =
                           this.Session[this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID].ToString();

                    }
                }
            }
        }

        protected void OneWayRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT, true);
            this.Session.Add(NativeConstants.RETURN_ROUTE_SELECT_SEAT, false);

            this.SaveAllBagsToSessionBeforeSeatSelection();
            this.Session.Add(this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID,
                this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value);

            if (!string.IsNullOrEmpty(this.ReturnRouteNumberOfCheckedInBagsHiddenField.Value))
            {
                this.Session.Add(this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID,
                this.ReturnRouteNumberOfCheckedInBagsHiddenField.Value);
            }

            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void ReturnRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.Session.Add(NativeConstants.RETURN_ROUTE_SELECT_SEAT, true);
            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT, false);

            this.SaveAllBagsToSessionBeforeSeatSelection();
            this.Session.Add(this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID,
                this.ReturnRouteNumberOfCheckedInBagsHiddenField.Value);

            if (!string.IsNullOrEmpty(this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value))
            {
                this.Session.Add(this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID,
                this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value);
            }

            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            //this.OneWayRouteBooking.Baggages.Add(new Baggage()
            //{
            //    Type = BaggageType.Cabin,
            //    Size = this.OneWayRouteSelectedCabinBagSizeHiddenField.Value,
            //    Price = decimal.Parse(this.OneWayRouteSelectedCabinBagPriceHiddenField.Value)
            //});

            //int numberOfCheckedInBags = int.Parse(this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value);

            //if (numberOfCheckedInBags > 0)
            //{
            //    for (int i = 1; i <= numberOfCheckedInBags; i++)
            //    {
            //        this.OneWayRouteBooking.Baggages.Add(new Baggage()
            //        {
            //            Type = BaggageType.CheckedIn,
            //            MaxKilograms = int.Parse(this.OneWayRouteSelectedCheckedInBagWeightHiddenField.Value),
            //            Price = decimal.Parse(this.OneWayRouteSelectedCheckedInBagPriceHiddenField.Value)
            //        });
            //    }
            //}

            //if (this.OneWayRouteBabyEquipmentCheckBox.Checked)
            //{
            //    this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.BabyEquipment, 10);
            //}

            //if (this.OneWayRouteSportsEquipmentCheckBox.Checked)
            //{
            //    this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.SportsEquipment, 30);
            //}

            //if (this.OneWayRouteMusicEquipmentCheckBox.Checked)
            //{
            //    this.AddOtherBaggagesToBooking(this.OneWayRouteBooking, BaggageType.MusicEquipment, 50);
            //}

            //this.CalculateTotalPriceOfBooking(this.OneWayRouteBooking);

            //this.Session.Add(NativeConstants.ONE_WAY_ROUTE_BOOKING, this.OneWayRouteBooking);
            //this.Response.Redirect(Pages.PAYMENT);
        }

        private void AddAttributesToRadioButtons()
        {
            // Checked-in baggage
            this.OneWayRouteNoneCheckedInBag.InputAttributes.Add("data-price", "0");
            this.OneWayRoute23KgCheckedInBag.InputAttributes.Add("data-price", "26");
            this.OneWayRoute32KgCheckedInBag.InputAttributes.Add("data-price", "36");

            // Cabin baggage
            this.OneWayRouteSmallCabinBag.InputAttributes.Add("data-price", "0");
            this.OneWayRouteLargeCabinBag.InputAttributes.Add("data-price", "14");

            if (this.ReturnRouteBooking != null)
            {
                // Checked-in baggage
                this.ReturnRouteNoneCheckedInBag.InputAttributes.Add("data-price", "0");
                this.ReturnRoute23KgCheckedInBag.InputAttributes.Add("data-price", "26");
                this.ReturnRoute32KgCheckedInBag.InputAttributes.Add("data-price", "36");

                // Cabin baggage
                this.ReturnRouteSmallCabinBag.InputAttributes.Add("data-price", "0");
                this.ReturnRouteLargeCabinBag.InputAttributes.Add("data-price", "14");
            }
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

        private void SaveAllBagsToSessionBeforeSeatSelection()
        {
            this.SaveSelectedBagToSessionBeforeSeatSelection(this.OneWayRouteCheckedInBaggagePanel, 
                NativeConstants.ONE_WAY_ROUTE_SELECTED_CHECKED_IN_BAG);

            this.SaveSelectedBagToSessionBeforeSeatSelection(this.OneWayRouteCabinBaggagePanel, 
                NativeConstants.ONE_WAY_ROUTE_SELECTED_CABIN_BAG);

            this.SaveOtherBagsToSessionBeforeSeatSelection(this.OneWayRouteOtherBaggagePanel);

            if (this.ReturnRouteBooking != null)
            {
                this.SaveSelectedBagToSessionBeforeSeatSelection(this.ReturnRouteCheckedInBaggagePanel,
                    NativeConstants.RETURN_ROUTE_SELECTED_CHECKED_IN_BAG);

                this.SaveSelectedBagToSessionBeforeSeatSelection(this.ReturnRouteCabinBaggagePanel,
                    NativeConstants.RETURN_ROUTE_SELECTED_CABIN_BAG);

                this.SaveOtherBagsToSessionBeforeSeatSelection(this.ReturnRouteOtherBaggagePanel);
            }
        }

        private void SaveSelectedBagToSessionBeforeSeatSelection(Control container, string sessionVariable)
        {
            var selectedBag = container.Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);

            if (selectedBag != null)
            {
                this.Session.Add(sessionVariable, selectedBag.ID);
            }
        }

        private void SaveBagSelectionAfterSeatSelection(Control container, string sessionVariable)
        {
            var selectedBagId = this.Session[sessionVariable];

            if (selectedBagId != null)
            {
                var selectedBag = container.Controls.OfType<RadioButton>()
                    .FirstOrDefault(r => r.ID == selectedBagId.ToString());

                if (selectedBag != null)
                {
                    selectedBag.Checked = true;
                }
            }
        }

        private void SaveOtherBagsToSessionBeforeSeatSelection(Control container)
        {
            var selectedOtherBags = container.Controls
                .OfType<CheckBox>()
                .Where(c => c.Checked)
                .ToList();

            if (selectedOtherBags.Count > 0)
            {
                selectedOtherBags.ForEach(c =>
                {
                    this.Session.Add(c.ID, c.Checked);
                });
            }
        }

        private void SaveOtherBagsSelectionAfterSeatSelection(Control container)
        {
            container.Controls
                .OfType<CheckBox>()
                .ToList()
                .ForEach(c =>
                {
                    if (this.Session[c.ID] != null)
                    {
                        c.Checked = true;
                        this.Session[c.ID] = null;
                    }
                });
        }


    }
}