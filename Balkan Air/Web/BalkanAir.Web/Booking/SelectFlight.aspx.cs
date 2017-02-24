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

        protected LegInstance LegInstance { get;  private set; }

        public IEnumerable<LegInstance> AvailableDatesRepeater_GetData()
        {
            List<LegInstance> legInstances = null;

            int departureAirprotId;
            bool isDepartureAirprotIdValid = int.TryParse(this.Session[NativeConstants.DEPARTURE_AIRPORT_ID].ToString(),
                out departureAirprotId);

            int destinationAirportId;
            bool isDestinationAirportIdValid = int.TryParse(this.Session[NativeConstants.DESTINATION_AIRPORT_ID].ToString(), 
                out destinationAirportId);

            DateTime departureDate;
            bool isDepartureDateValid = DateTime.TryParse(this.Session[NativeConstants.DEPARTURE_DATE].ToString(), 
                out departureDate);

            if (isDepartureAirprotIdValid && isDestinationAirportIdValid && isDepartureDateValid)
            {
                legInstances = this.LegInstancesServices.GetAll()
                    .Where(l => !l.IsDeleted && l.FlightLeg.DepartureAirportId == departureAirprotId && 
                        l.FlightLeg.ArrivalAirportId == destinationAirportId)
                    .OrderBy(l => l.DepartureDateTime)
                    .ToList();

                int initialSlideIndex = legInstances
                    .FindIndex(l => l.DepartureDateTime.Date == departureDate.Date);

                if (initialSlideIndex == -1)
                {
                    initialSlideIndex = 0;
                }

                this.InitialSlideToStartHiddenField.Value = initialSlideIndex.ToString();
            }

            if (legInstances == null || legInstances.Count == 0)
            {
                this.Response.Redirect(Pages.HOME);
            }
            else
            {
                //this.Session.Remove(Parameters.DEPARTURE_AIRPORT_ID);
                //this.Session.Remove(Parameters.DESTINATION_AIRPORT_ID);
            }

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

                this.InitialSlideToStartHiddenField.Value = "1";
            }
        }

        protected void OnFlightDateLinkButtonClicked(object sender, EventArgs e)
        {
            LinkButton selectedFlightDate = sender as LinkButton;

            if (selectedFlightDate != null)
            {
                
            }
        }

        protected void OnContinueBookingBtnClicked(object sender, EventArgs e)
        {
            if (!this.User.Identity.IsAuthenticated || string.IsNullOrEmpty(this.SelectedFlightIdHiddenField.Value) ||
                string.IsNullOrEmpty(this.SelectedTravelClassIdHiddenField.Value))
            {
                this.SignInRequiredPanel.Visible = true;
            }
            else
            {
                Booking booking = new Booking()
                {
                    LegInstanceId = int.Parse(this.SelectedFlightIdHiddenField.Value),
                    TravelClassId = int.Parse(this.SelectedTravelClassIdHiddenField.Value)
                };

                this.Session.Add(Common.NativeConstants.BOOKING, booking);
                this.Response.Redirect(Pages.EXTRAS);
            }
        }

        protected int NumberOfAvailableSeats(int travelClassId)
        {
            //var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);
            //int reservedSeatsForTravelClass = this.SeatsServices.GetAll()
            //    .Where(s => s.TravelClassId == travelClassId)
            //    .Count(s => s.IsReserved);

            //return travelClass.NumberOfSeats - reservedSeatsForTravelClass;

            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            return travelClass.NumberOfAvailableSeats;
        }

        protected void ShowFlgihtInfoButton_Click(object sender, EventArgs e)
        {
            int flightId = int.Parse(CurrentFlightInfoIdHiddenField.Value);

            this.SelectedFlightIdHiddenField.Value = flightId.ToString();
            this.ContinueBookingBtn.Visible = true;

            LegInstance legInstance = this.LegInstancesServices.GetLegInstance(flightId);
            this.LegInstance = legInstance;

            this.FlightDetailsFormView.DataSource = new List<LegInstance>() { legInstance };
            this.FlightDetailsFormView.DataBind();

            this.FlightTravelClassesRepeater.DataSource = legInstance.Aircraft.TravelClasses.ToList();
            this.FlightTravelClassesRepeater.DataBind();
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
    }
}