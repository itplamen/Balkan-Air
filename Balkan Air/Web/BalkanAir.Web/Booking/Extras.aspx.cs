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
                    this.ManageSeatSelection(this.OneWayRouteBooking, this.OneWayRouteSelectedSeat,
                        this.OneWayRouteSelectedSeatImage, this.OneWayRouteSelectSeatBtn);

                    if (this.ReturnRouteBooking != null)
                    {
                        this.ReturnRouteExtrasPanel.Visible = true;

                        this.ManageSeatSelection(this.ReturnRouteBooking, this.ReturnRouteSelectedSeat,
                            this.ReturnRouteSelectedSeatImage, this.ReturnRouteSelectSeatBtn);
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var isOneWayRouteSelectedSeat = this.Session[NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT];
                var isReturnRouteSelectedSeat = this.Session[NativeConstants.RETURN_ROUTE_SELECT_SEAT];

                if (isOneWayRouteSelectedSeat != null && isReturnRouteSelectedSeat != null && 
                    ((bool)isOneWayRouteSelectedSeat || (bool)isReturnRouteSelectedSeat))
                {
                    this.SaveBagSelectionAfterSeatSelection(this.OneWayRouteCabinBaggagePanel,
                        NativeConstants.ONE_WAY_ROUTE_SELECTED_CABIN_BAG);

                    this.SaveOtherBagsSelectionAfterSeatSelection(this.OneWayRouteOtherBaggagePanel);

                    if (this.Session[this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID] != null)
                    {
                        this.OneWayRouteNumberOfCheckedInBagsHiddenField.Value =
                            this.Session[this.OneWayRouteNumberOfCheckedInBagsHiddenField.ID].ToString();

                        this.SaveBagSelectionAfterSeatSelection(this.OneWayRouteCheckedInBaggagePanel,
                            NativeConstants.ONE_WAY_ROUTE_SELECTED_CHECKED_IN_BAG);
                    }

                    if (this.ReturnRouteBooking != null)
                    {
                        this.SaveBagSelectionAfterSeatSelection(this.ReturnRouteCabinBaggagePanel,
                            NativeConstants.RETURN_ROUTE_SELECTED_CABIN_BAG);

                        this.SaveOtherBagsSelectionAfterSeatSelection(this.ReturnRouteOtherBaggagePanel);

                        if (this.Session[this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID] != null)
                        {
                            this.ReturnRouteNumberOfCheckedInBagsHiddenField.Value =
                               this.Session[this.ReturnRouteNumberOfCheckedInBagsHiddenField.ID].ToString();

                            this.SaveBagSelectionAfterSeatSelection(this.ReturnRouteCheckedInBaggagePanel,
                                NativeConstants.RETURN_ROUTE_SELECTED_CHECKED_IN_BAG);
                        }
                    }
                }
            }
        }

        protected void OneWayRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.SetOneWayRouteSeatSelectionToSession();

            this.SaveAllBagsToSessionBeforeSeatSelection();

            this.SaveNumberOfCheckedInBagsToSession(this.OneWayRouteNumberOfCheckedInBagsHiddenField,
                this.ReturnRouteNumberOfCheckedInBagsHiddenField);

            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void ReturnRouteSelectSeatBtn_Click(object sender, EventArgs e)
        {
            this.SetOneWayRouteSeatSelectionToSession(false);

            this.SaveAllBagsToSessionBeforeSeatSelection();

            this.SaveNumberOfCheckedInBagsToSession(this.ReturnRouteNumberOfCheckedInBagsHiddenField,
                this.OneWayRouteNumberOfCheckedInBagsHiddenField);

            this.Response.Redirect(Pages.SELECT_SEAT);
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.AddAllBagsToBooking(this.OneWayRouteBooking, this.OneWayRouteCabinBaggagePanel, 
                    this.OneWayRouteCheckedInBaggagePanel, this.OneWayRouteNumberOfCheckedInBagsHiddenField, 
                    this.OneWayRouteBabyEquipmentCheckBox, this.OneWayRouteMusicEquipmentCheckBox, 
                    this.OneWayRouteSportsEquipmentCheckBox);

                this.Session.Add(NativeConstants.ONE_WAY_ROUTE_BOOKING, this.OneWayRouteBooking);

                if (this.ReturnRouteBooking != null)
                {
                    this.AddAllBagsToBooking(this.ReturnRouteBooking, this.ReturnRouteCabinBaggagePanel,
                    this.ReturnRouteCheckedInBaggagePanel, this.ReturnRouteNumberOfCheckedInBagsHiddenField,
                    this.ReturnRouteBabyEquipmentCheckBox, this.ReturnRouteMusicEquipmentCheckBox,
                    this.ReturnRouteSportsEquipmentCheckBox);

                    this.Session.Add(NativeConstants.RETURN_ROUTE_BOOKING, this.ReturnRouteBooking);
                }

                this.Response.Redirect(Pages.PAYMENT);
            }
        }

        private void AddAttributesToRadioButtons()
        {
            // Checked-in baggage
            this.OneWayRouteNoneCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR, 
                NativeConstants.NONE_CHECKED_IN_BAG_PRICE.ToString());

            this.OneWayRoute23KgCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                NativeConstants.MEDIUM_CHECKED_IN_BAG_PRICE.ToString());

            this.OneWayRoute32KgCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                NativeConstants.LARGE_CHECKED_IN_BAG_PRICE.ToString());

            // Cabin baggage
            this.OneWayRouteSmallCabinBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                NativeConstants.SMALL_CABIN_BAG_PRICE.ToString());

            this.OneWayRouteLargeCabinBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                NativeConstants.LARGE_CABIN_BAG_PRICE.ToString());

            if (this.ReturnRouteBooking != null)
            {
                // Checked-in baggage
                this.ReturnRouteNoneCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                    NativeConstants.NONE_CHECKED_IN_BAG_PRICE.ToString());

                this.ReturnRoute23KgCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                    NativeConstants.MEDIUM_CHECKED_IN_BAG_PRICE.ToString());

                this.ReturnRoute32KgCheckedInBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                    NativeConstants.LARGE_CHECKED_IN_BAG_PRICE.ToString());

                // Cabin baggage
                this.ReturnRouteSmallCabinBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR, 
                    NativeConstants.SMALL_CABIN_BAG_PRICE.ToString());

                this.ReturnRouteLargeCabinBag.Attributes.Add(NativeConstants.BAG_PRICE_ATTR,
                    NativeConstants.LARGE_CABIN_BAG_PRICE.ToString());
            }
        }

        private void ManageSeatSelection(Booking booking, Label selectedSeatLabel, Image selectedSeatImg, Button selectedSeatBtn)
        {
            bool isVisible = false;

            if (booking.Row == 0 || string.IsNullOrEmpty(booking.SeatNumber))
            {
                selectedSeatBtn.Text = "SELECT SEAT";
            }
            else
            {
                isVisible = true;
                selectedSeatLabel.Text = booking.Row + booking.SeatNumber;
                selectedSeatBtn.Text = "CHANGE";
            }

            selectedSeatLabel.Visible = isVisible;
            selectedSeatImg.Visible = isVisible;
        }

        private void SetOneWayRouteSeatSelectionToSession(bool isSeatSelectionForOneWayRoute = true)
        {
            this.Session.Add(NativeConstants.ONE_WAY_ROUTE_SELECT_SEAT, isSeatSelectionForOneWayRoute);
            this.Session.Add(NativeConstants.RETURN_ROUTE_SELECT_SEAT, !isSeatSelectionForOneWayRoute);
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

        private void SaveNumberOfCheckedInBagsToSession(HiddenField checkedInBags, HiddenField otherRouteCheckedInBags)
        {
            if (!string.IsNullOrEmpty(checkedInBags.Value))
            {
                this.Session.Add(checkedInBags.ID, checkedInBags.Value);
            }

            if (!string.IsNullOrEmpty(otherRouteCheckedInBags.Value))
            {
                this.Session.Add(otherRouteCheckedInBags.ID, otherRouteCheckedInBags.Value);
            }
        }

        private void AddAllBagsToBooking(Booking booking, Control cabinBagsContext, Control checkedInBagsContext, 
            HiddenField checkedInBagsCount, CheckBox babyEquipment, CheckBox musicEquipment, CheckBox sportsEquipment)
        {
            this.AddCabinBagToBooking(booking, cabinBagsContext);

            if (!string.IsNullOrEmpty(checkedInBagsCount.Value))
            {
                int numberOfCheckedInBags = int.Parse(checkedInBagsCount.Value);

                if (numberOfCheckedInBags > 0)
                {
                    this.AddCheckedInBagsToBooking(booking, checkedInBagsContext, numberOfCheckedInBags);
                }
            }

            if (babyEquipment.Checked)
            {
                this.AddOtherBaggageToBooking(booking, BaggageType.BabyEquipment, 10);
            }

            if (musicEquipment.Checked)
            {
                this.AddOtherBaggageToBooking(booking, BaggageType.MusicEquipment, 30);
            }

            if (sportsEquipment.Checked)
            {
                this.AddOtherBaggageToBooking(booking, BaggageType.SportsEquipment, 50);
            }
        }

        private void AddCabinBagToBooking(Booking booking, Control cabinBagsContext)
        {
            var selectedCabinBag = this.GetSelectedBag(cabinBagsContext);

            booking.Baggage.Add(new Baggage()
            {
                Type = BaggageType.Cabin,
                Size = selectedCabinBag.Attributes[NativeConstants.BAG_SIZE_ATTR],
                Price = decimal.Parse(selectedCabinBag.Attributes[NativeConstants.BAG_PRICE_ATTR]),
            });
        }

        private void AddCheckedInBagsToBooking(Booking booking, Control checkedInBagsContext, int numberOfBags)
        {
            var selectedCheckedInBag = this.GetSelectedBag(checkedInBagsContext);

            for (int i = 1; i <= numberOfBags; i++)
            {
                var aaa = int.Parse(selectedCheckedInBag.Attributes[NativeConstants.BAG_KG_ATTR]);
                var ddd = decimal.Parse(selectedCheckedInBag.Attributes[NativeConstants.BAG_PRICE_ATTR]);

                booking.Baggage.Add(new Baggage()
                {
                    Type = BaggageType.CheckedIn,
                    MaxKilograms = int.Parse(selectedCheckedInBag.Attributes[NativeConstants.BAG_KG_ATTR]),
                    Price = decimal.Parse(selectedCheckedInBag.Attributes[NativeConstants.BAG_PRICE_ATTR])
                });
            }
        }

        private void AddOtherBaggageToBooking(Booking booking, BaggageType type, decimal price)
        {
            booking.Baggage.Add(new Baggage()
            {
                Type = type,
                Price = price
            });
        }

        private RadioButton GetSelectedBag(Control container)
        {
            return container.Controls
                .OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);
        }
    }
}