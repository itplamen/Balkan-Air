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
        public ITravelClassesServices TravelClassesServices { get; set; }

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        protected LegInstance LegInstance { get; private set; }

        private int DepartureAirprotId { get; set; }

        private int DestinationAirportId { get; set; }

        private DateTime DepartureDate { get; set; }

        private DateTime ArrivalDate { get; set; }

        public IEnumerable<LegInstance> OneWayRouteDepartureDatesRepeater_GetData()
        {
            var legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.FlightLeg.DepartureAirportId == this.DepartureAirprotId &&
                    l.FlightLeg.ArrivalAirportId == this.DestinationAirportId)
                .OrderBy(l => l.DepartureDateTime)
                .ToList();

            if (legInstances == null || legInstances.Count == 0)
            {
                this.Response.Redirect(Pages.HOME);
            }
            else
            {
                //this.Session.Remove(Parameters.DEPARTURE_AIRPORT_ID);
                //this.Session.Remove(Parameters.DESTINATION_AIRPORT_ID);
            }

            int initialSlideIndex = legInstances
                .FindIndex(l => l.DepartureDateTime.Date == this.DepartureDate.Date);

            this.OneWayRouteCurrentFlightInfoIdHiddenField.Value = legInstances
                .FirstOrDefault(l => l.DepartureDateTime.Date == this.DepartureDate.Date)
                .Id
                .ToString();

            this.OneWayRouteSelectedFlightIdHiddenField.Value = this.OneWayRouteCurrentFlightInfoIdHiddenField.Value;

            if (initialSlideIndex == -1)
            {
                initialSlideIndex = 0;
            }

            this.OneWayRouteInitialSlideIndexHiddenField.Value = initialSlideIndex.ToString();

            this.ShowFlightInfo(this.OneWayRouteCurrentFlightInfoIdHiddenField, this.OneWayFlightDetailsFormView,
                this.OneWayFlightTravelClassesRepeater);

            return legInstances;
        }

        public IEnumerable<LegInstance> ReturnRouteDepartureDatesRepeater_GetData()
        {
            var legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.FlightLeg.DepartureAirportId == this.DestinationAirportId &&
                    l.FlightLeg.ArrivalAirportId == this.DepartureAirprotId)
                .OrderBy(l => l.DepartureDateTime)
                .ToList();

            if (legInstances == null || legInstances.Count == 0)
            {
                return null;
            }

            int initialSlideIndex = legInstances
                .FindIndex(l => l.DepartureDateTime.Date == this.ArrivalDate.Date);

            this.ReturnRouteCurrentFlightInfoIdHiddenField.Value = legInstances
                .FirstOrDefault(l => l.DepartureDateTime.Date == this.ArrivalDate.Date)
                .Id
                .ToString();

            this.ReturnRouteSelectedFlightIdHiddenField.Value = this.ReturnRouteCurrentFlightInfoIdHiddenField.Value;

            if (initialSlideIndex == -1)
            {
                initialSlideIndex = 0;
            }

            this.ReturnRouteInitialSlideIndexHiddenField.Value = initialSlideIndex.ToString();

            this.ShowFlightInfo(this.ReturnRouteCurrentFlightInfoIdHiddenField, this.ReturnFlightDetailsFormView,
                this.ReturnFlightTravelClassesRepeater);

            return legInstances;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.Session[NativeConstants.DEPARTURE_AIRPORT_ID] == null ||
                    this.Session[NativeConstants.DESTINATION_AIRPORT_ID] == null)
                {
                    this.Response.Redirect(Pages.HOME);
                }

                this.DepartureAirprotId = int.Parse(this.Session[NativeConstants.DEPARTURE_AIRPORT_ID].ToString());
                this.DestinationAirportId = int.Parse(this.Session[NativeConstants.DESTINATION_AIRPORT_ID].ToString());
                this.DepartureDate = DateTime.Parse(this.Session[NativeConstants.DEPARTURE_DATE].ToString());

                if (this.Session[NativeConstants.ARRIVAL_DATE] == null)
                {
                    this.ReturnRouteFlightsPanel.Visible = false;
                }
                else
                {
                    this.ReturnRouteFlightsPanel.Visible = true;
                    this.ArrivalDate = DateTime.Parse(this.Session[NativeConstants.ARRIVAL_DATE].ToString());
                }
            }
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                this.SignInRequiredPanel.Visible = true;
            }

            if (string.IsNullOrEmpty(this.OneWayRouteSelectedFlightIdHiddenField.Value) ||
                string.IsNullOrEmpty(this.OneWayRouteSelectedTravelClassIdHiddenField.Value))
            {
                this.OneWayRouteTravelClassCustomValidator.ErrorMessage = "Departure flight and travel class are required!";
                this.OneWayRouteTravelClassCustomValidator.IsValid = false;
            }

            if(this.ReturnRouteFlightsPanel.Visible &&
                    string.IsNullOrEmpty(this.ReturnRouteSelectedFlightIdHiddenField.Value) &&
                    string.IsNullOrEmpty(this.ReturnRouteSelectedTravelClassIdHiddenField.Value))
            {
                this.ReturnRouteTravelClassCustomValidator.ErrorMessage = "Arrival flight and travel class are required!";
                this.ReturnRouteTravelClassCustomValidator.IsValid = false;
            }

            if (this.Page.IsValid)
            {
                Booking booking = new Booking()
                {
                    LegInstanceId = int.Parse(this.OneWayRouteSelectedFlightIdHiddenField.Value),
                    TravelClassId = int.Parse(this.OneWayRouteSelectedTravelClassIdHiddenField.Value)
                };

                this.Session.Add(Common.NativeConstants.BOOKING, booking);
                this.Response.Redirect(Pages.EXTRAS);
            }
        }

        protected int NumberOfAvailableSeats(int travelClassId)
        {
            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            return travelClass.NumberOfAvailableSeats;
        }

        protected void ShowOneWayFlgihtInfoButton_Click(object sender, EventArgs e)
        {
            this.OneWayRouteSelectedFlightIdHiddenField.Value = this.OneWayRouteCurrentFlightInfoIdHiddenField.Value;

            this.ShowFlightInfo(this.OneWayRouteCurrentFlightInfoIdHiddenField, this.OneWayFlightDetailsFormView, 
                this.OneWayFlightTravelClassesRepeater);
        }

        protected void ShowReturnFlgihtInfoButton_Click(object sender, EventArgs e)
        {
            this.ReturnRouteSelectedFlightIdHiddenField.Value = this.ReturnRouteCurrentFlightInfoIdHiddenField.Value;

            this.ShowFlightInfo(this.ReturnRouteCurrentFlightInfoIdHiddenField, this.ReturnFlightDetailsFormView,
                this.ReturnFlightTravelClassesRepeater);
        }

        private ApplicationUserManager GetManager()
        {
            return Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private User GetCurrentUser()
        {
            var manager = this.GetManager();
            return manager.FindById(this.Context.User.Identity.GetUserId());
        }

        private void ShowFlightInfo(HiddenField selectedFlightId, FormView flightDetails, Repeater travelClasses)
        {
            int flightId = int.Parse(selectedFlightId.Value);

            selectedFlightId.Value = flightId.ToString();
            this.ContinueBookingBtn.Visible = true;

            LegInstance legInstance = this.LegInstancesServices.GetLegInstance(flightId);
            this.LegInstance = legInstance;

            flightDetails.DataSource = new List<LegInstance>() { legInstance };
            flightDetails.DataBind();

            travelClasses.DataSource = legInstance.Aircraft.TravelClasses.ToList();
            travelClasses.DataBind();
        }
    }
}