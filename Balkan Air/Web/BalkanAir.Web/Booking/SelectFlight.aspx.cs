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

        public IEnumerable<LegInstance> AvailableDatesRepeater_GetData()
        {
            List<LegInstance> legInstances = null;

            int departureAirprotId;
            int destinationAirportId;

            bool isDepartureAirprotIdValid = int.TryParse(this.Session[NativeConstants.DEPARTURE_AIRPORT_ID].ToString(), out departureAirprotId);
            bool isDestinationAirportIdValid = int.TryParse(this.Session[NativeConstants.DESTINATION_AIRPORT_ID].ToString(), out destinationAirportId);

            if (isDepartureAirprotIdValid && isDestinationAirportIdValid)
            {
                legInstances = this.LegInstancesServices.GetAll()
                    .Where(l => !l.IsDeleted && l.FlightLeg.DepartureAirportId == departureAirprotId && 
                        l.FlightLeg.ArrivalAirportId == destinationAirportId)
                    .OrderBy(l => l.DepartureDateTime)
                    .ToList();
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
                if (this.Session[Common.NativeConstants.DEPARTURE_AIRPORT_ID] == null || 
                    this.Session[Common.NativeConstants.DESTINATION_AIRPORT_ID] == null)
                {
                    this.Response.Redirect(Pages.HOME);
                }
            }
        }

        protected void OnFlightDateLinkButtonClicked(object sender, EventArgs e)
        {
            LinkButton selectedFlightDate = sender as LinkButton;

            if (selectedFlightDate != null)
            {
                int flightId = int.Parse(selectedFlightDate.CommandArgument);

                this.SelectedFlightIdHiddenField.Value = flightId.ToString();
                this.ContinueBookingBtn.Visible = true;

                LegInstance legInstance = this.LegInstancesServices.GetLegInstance(flightId);

                this.FlightDetailsFormView.DataSource = new List<LegInstance>() { legInstance };
                this.FlightDetailsFormView.DataBind();

                this.FlightTravelClassesRepeater.DataSource = legInstance.Aircraft.TravelClasses.ToList();
                this.FlightTravelClassesRepeater.DataBind();
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