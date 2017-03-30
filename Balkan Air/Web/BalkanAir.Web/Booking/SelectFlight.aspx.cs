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

    using Auth;
    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class SelectFlight : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public IRoutesServices RoutesServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        protected LegInstance LegInstance { get; private set; }

        protected Route RouteInfo
        {
            get
            {
                return this.RoutesServices.GetAll()
                    .FirstOrDefault(r => r.OriginId == this.DepartureAirprotId &&
                                    r.DestinationId == this.DestinationAirportId);
            }
        }

        private int DepartureAirprotId
        {
            get
            {
                return int.Parse(this.Session[Common.WebConstants.DEPARTURE_AIRPORT_ID].ToString());
            }
        }

        private int DestinationAirportId
        {
            get
            {
                return int.Parse(this.Session[Common.WebConstants.DESTINATION_AIRPORT_ID].ToString());
            }
        }

        private ApplicationUserManager Manager
        {
            get { return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        private DateTime DepartureDate { get; set; }

        private DateTime ArrivalDate { get; set; }

        public IEnumerable<LegInstance> OneWayRouteDepartureDatesRepeater_GetData()
        {
            var legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.DepartureDateTime > DateTime.Now && 
                            l.FlightLeg.DepartureAirportId == this.DepartureAirprotId && 
                            l.FlightLeg.ArrivalAirportId == this.DestinationAirportId)
                .OrderBy(l => l.DepartureDateTime)
                .ToList();

            this.AddDaysWithNoFlightToSlider(legInstances);

            int initialSlideIndex = legInstances
                .FindIndex(l => l.DepartureDateTime.Date == this.DepartureDate.Date);

            this.OneWayRouteInitialSlideIndexHiddenField.Value = initialSlideIndex.ToString();

            int currentFlightId = legInstances
                .FirstOrDefault(l => l.DepartureDateTime.Date == this.DepartureDate.Date)
                .Id;

            if (currentFlightId > 0)
            {
                this.ShowFlightInfo(currentFlightId, this.OneWayFlightDetailsFormView, this.OneWayFlightTravelClassesRepeater);
                this.OneWayRouteSelectedFlightIdHiddenField.Value = currentFlightId.ToString();
            }

            return legInstances;
        }

        public IEnumerable<LegInstance> ReturnRouteDepartureDatesRepeater_GetData()
        {
            var legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.DepartureDateTime > DateTime.Now && 
                            l.FlightLeg.DepartureAirportId == this.DestinationAirportId &&
                            l.FlightLeg.ArrivalAirportId == this.DepartureAirprotId)
                .OrderBy(l => l.DepartureDateTime)
                .ToList();

            this.AddDaysWithNoFlightToSlider(legInstances);

            int initialSlideIndex = legInstances
                .FindIndex(l => l.DepartureDateTime.Date == this.ArrivalDate.Date);

            this.ReturnRouteInitialSlideIndexHiddenField.Value = initialSlideIndex.ToString();

            int currentFlightId = legInstances
                .FirstOrDefault(l => l.DepartureDateTime.Date == this.ArrivalDate.Date)
                .Id;

            if (currentFlightId > 0)
            {
                this.ShowFlightInfo(currentFlightId, this.ReturnFlightDetailsFormView, this.ReturnFlightTravelClassesRepeater);
                this.ReturnRouteSelectedFlightIdHiddenField.Value = currentFlightId.ToString();
            }

            return legInstances;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.Session[Common.WebConstants.DEPARTURE_AIRPORT_ID] == null ||
                    this.Session[Common.WebConstants.DESTINATION_AIRPORT_ID] == null)
                {
                    this.Response.Redirect(Pages.HOME);
                }

                this.DepartureDate = DateTime.Parse(this.Session[Common.WebConstants.DEPARTURE_DATE].ToString());

                if (this.Session[Common.WebConstants.ARRIVAL_DATE] == null)
                {
                    this.ReturnRouteFlightsPanel.Visible = false;
                }
                else
                {
                    this.ReturnRouteFlightsPanel.Visible = true;
                    this.ArrivalDate = DateTime.Parse(this.Session[Common.WebConstants.ARRIVAL_DATE].ToString());
                }
            }
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                this.SignInRequiredPanel.Visible = true;
                return;
            }
            else if (this.User.Identity.IsAuthenticated && !this.CurrentUser.EmailConfirmed)
            {
                this.ConfirmEmailPanel.Visible = true;
                return;
            }

            if (string.IsNullOrEmpty(this.OneWayRouteSelectedFlightIdHiddenField.Value) ||
                string.IsNullOrEmpty(this.OneWayRouteSelectedTravelClassIdHiddenField.Value))
            {
                this.OneWayRouteTravelClassCustomValidator.ErrorMessage = "Departure flight and travel class are required!";
                this.OneWayRouteTravelClassCustomValidator.IsValid = false;

                return;
            }

            if (this.ReturnRouteFlightsPanel.Visible && 
                (string.IsNullOrEmpty(this.ReturnRouteSelectedFlightIdHiddenField.Value) ||
                string.IsNullOrEmpty(this.ReturnRouteSelectedTravelClassIdHiddenField.Value)))
            {
                this.ReturnRouteTravelClassCustomValidator.ErrorMessage = "Arrival flight and travel class are required!";
                this.ReturnRouteTravelClassCustomValidator.IsValid = false;

                return;
            }

            if (this.ReturnRouteFlightsPanel.Visible && !this.AreSelectedDatesValid())
            {
                this.InvalidArrivalDateCustomValidator.ErrorMessage = "Arrival flight cannot be before departure flight!";
                this.InvalidArrivalDateCustomValidator.IsValid = false;

                return;
            }

            if (this.Page.IsValid)
            {
                int legInstanceId = int.Parse(this.OneWayRouteSelectedFlightIdHiddenField.Value);

                Booking oneWayRouteBooking = new Booking()
                {
                    LegInstanceId = legInstanceId,
                    LegInstance = this.LegInstancesServices.GetLegInstance(legInstanceId),
                    TravelClassId = int.Parse(this.OneWayRouteSelectedTravelClassIdHiddenField.Value)
                };

                this.Session.Add(Common.WebConstants.ONE_WAY_ROUTE_BOOKING, oneWayRouteBooking);

                if (this.ReturnRouteFlightsPanel.Visible)
                {
                    legInstanceId = int.Parse(this.ReturnRouteSelectedFlightIdHiddenField.Value);

                    Booking returnRouteBooking = new Booking()
                    {
                        LegInstanceId = legInstanceId,
                        LegInstance = this.LegInstancesServices.GetLegInstance(legInstanceId),
                        TravelClassId = int.Parse(this.ReturnRouteSelectedTravelClassIdHiddenField.Value)
                    };

                    this.Session.Add(Common.WebConstants.RETURN_ROUTE_BOOKING, returnRouteBooking);
                }

                this.Response.Redirect(Pages.EXTRAS);
            }
        }

        protected int NumberOfAvailableSeats(int travelClassId)
        {
            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            return travelClass.NumberOfAvailableSeats;
        }

        protected void ShowOneWayFlgihtInfoHiddenButton_Click(object sender, EventArgs e)
        {
            int currentFlightId = int.Parse(this.OneWayRouteCurrentFlightInfoIdHiddenField.Value);

            if (this.OneWayRouteCurrentFlightInfoIdHiddenField.Value == string.Empty || currentFlightId <= 0)
            {
                return;
            }

            this.OneWayRouteSelectedFlightIdHiddenField.Value = this.OneWayRouteCurrentFlightInfoIdHiddenField.Value;

            this.ShowFlightInfo(currentFlightId, this.OneWayFlightDetailsFormView, this.OneWayFlightTravelClassesRepeater);
        }

        protected void ShowReturnFlgihtInfoHiddenButton_Click(object sender, EventArgs e)
        {
            int currentFlightId = int.Parse(this.ReturnRouteCurrentFlightInfoIdHiddenField.Value);

            if (this.ReturnRouteCurrentFlightInfoIdHiddenField.Value == string.Empty || currentFlightId <= 0)
            {
                return;
            }

            this.ReturnRouteSelectedFlightIdHiddenField.Value = this.ReturnRouteCurrentFlightInfoIdHiddenField.Value;

            this.ShowFlightInfo(currentFlightId, this.ReturnFlightDetailsFormView, this.ReturnFlightTravelClassesRepeater);
        }

        private void AddDaysWithNoFlightToSlider(List<LegInstance> legInstances)
        {
            int daysInMonth = DateTime.DaysInMonth(this.DepartureDate.Year, this.DepartureDate.Month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var flight = legInstances.FirstOrDefault(l => l.DepartureDateTime.Day == day);

                if (flight == null)
                {
                    int indexToInsert = day - 1;
                    LegInstance noFlightForThisDay = new LegInstance()
                    {
                        DepartureDateTime = new DateTime(this.DepartureDate.Year, this.DepartureDate.Month, day)
                    };

                    legInstances.Insert(indexToInsert, noFlightForThisDay);
                }
            }
        }

        private void ShowFlightInfo(int flightId, FormView flightDetails, Repeater travelClasses)
        {
            this.ContinueBookingBtn.Enabled = true;

            LegInstance legInstance = this.LegInstancesServices.GetLegInstance(flightId);
            this.LegInstance = legInstance;

            flightDetails.DataSource = new List<LegInstance>() { legInstance };
            flightDetails.DataBind();

            travelClasses.DataSource = legInstance.Aircraft.TravelClasses.ToList();
            travelClasses.DataBind();
        }

        private bool AreSelectedDatesValid()
        {
            int oneWayRouteFlightId = int.Parse(this.OneWayRouteSelectedFlightIdHiddenField.Value);
            int returnRouteFlightId = int.Parse(this.ReturnRouteSelectedFlightIdHiddenField.Value);

            var departureFlight = this.LegInstancesServices.GetLegInstance(oneWayRouteFlightId);
            var arrivalFlight = this.LegInstancesServices.GetLegInstance(returnRouteFlightId);

            if (departureFlight.DepartureDateTime > arrivalFlight.DepartureDateTime)
            {
                return false;
            }

            return true;
        }
    }
}