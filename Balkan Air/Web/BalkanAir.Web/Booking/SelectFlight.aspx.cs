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
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity;

    public partial class SelectFlight : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.Session[Parameters.DEPARTURE_AIRPORT_ID] == null || this.Session[Parameters.DESTINATION_AIRPORT_ID] == null)
                {
                    this.Response.Redirect(Pages.HOME);
                }
            }
        }

        public IEnumerable<Flight> AvailableDatesRepeater_GetData()
        {
            List<Flight> flights = null;

            int departureAirprotId;
            int destinationAirportId;

            bool isDepartureAirprotIdValid = int.TryParse(this.Session[Parameters.DEPARTURE_AIRPORT_ID].ToString(), out departureAirprotId);
            bool isDestinationAirportIdValid = int.TryParse(this.Session[Parameters.DESTINATION_AIRPORT_ID].ToString(), out destinationAirportId);

            if (isDepartureAirprotIdValid && isDestinationAirportIdValid)
            {
                flights = this.FlightsServices.GetAll()
                    .Where(f => !f.IsDeleted && f.DepartureAirport.Id == departureAirprotId && f.ArrivalAirport.Id == destinationAirportId)
                    .OrderBy(f => f.Departure)
                    .ToList();
            }

            if (flights == null || flights.Count == 0)
            {
                this.Response.Redirect(Pages.HOME);
            }
            else
            {
                this.Session.Remove(Parameters.DEPARTURE_AIRPORT_ID);
                this.Session.Remove(Parameters.DESTINATION_AIRPORT_ID);
            }

            return flights;
        }

        protected void OnFlightDateLinkButtonClicked(object sender, EventArgs e)
        {
            LinkButton selectedFlightDate = sender as LinkButton;

            if (selectedFlightDate != null)
            {
                int flightId = int.Parse(selectedFlightDate.CommandArgument);

                this.SelectedFlightIdHiddenField.Value = flightId.ToString();
                this.ContinueBookingBtn.Visible = true;

                Flight flight = this.FlightsServices.GetFlight(flightId);

                this.FlightDetailsFormView.DataSource = new List<Flight>() { flight };
                this.FlightDetailsFormView.DataBind();

                this.FlightTravelClassesRepeater.DataSource = flight.TravelClasses.ToList();
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
                    FlightId = int.Parse(this.SelectedFlightIdHiddenField.Value),
                    TravelClassId = int.Parse(this.SelectedTravelClassIdHiddenField.Value)
                };

                this.Session.Add(Parameters.BOOKING, booking);
                this.Response.Redirect(Pages.EXTRAS);
            }
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