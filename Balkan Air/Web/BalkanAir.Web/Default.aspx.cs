namespace BalkanAir.Web 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class _Default : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IFaresServices FaresServices { get; set; }

        [Inject]
        public INewsServices NewsServices { get; set; }

        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        public IEnumerable<Airport> DepartureAirportsRepeater_GetData()
        {
            return this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Country.Name)
                .ThenBy(a => a.Name)
                .ToList();
        }

        public IEnumerable<LegInstance> TopCheapestFlightsRepeater_GetData()
        {
            return this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.Price)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Data.Models.News> LatestNewsRepeater_GetData()
        {
            return this.NewsServices.GetAll().
                Where(n => !n.IsDeleted)
                .Take(3)
                .ToList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ReturnRouteRadioButton.Checked = true;
                this.ArrivalDateTextBox.Visible = this.ReturnRouteRadioButton.Checked;
                this.ArrivalDateFancyTextBox.Visible = this.ArrivalDateTextBox.Visible;

                this.DepartureAirportTextBox.Attributes.Add("readonly", "readonly");
                this.DestinationAirportTextBox.Attributes.Add("readonly", "readonly");
                this.DepartureDateTextBox.Attributes.Add("readonly", "readonly");
                this.ArrivalDateTextBox.Attributes.Add("readonly", "readonly");

                this.NoFlightsLiteral.Visible = false;

                this.DepartureCalendar.StartDate = DateTime.Now.Date.AddDays(1);
                this.ArrivalCalendarExtender.StartDate = DateTime.Now.Date.AddDays(1);
            }
        }

        protected void OnSelectDepartureAirportButtonClicked(object sender, EventArgs e)
        {
            var selectedAirport = sender as LinkButton;

            if (selectedAirport != null)
            {
                this.DepartureAirportTextBox.Text = selectedAirport.Text;

                int departureAirprotID;
                bool isIDValid = int.TryParse(selectedAirport.CommandArgument, out departureAirprotID);

                if (isIDValid)
                {
                    this.DepartureAirportIdHiddenField.Value = departureAirprotID.ToString();
                    this.DestinationAirportIdHiddenField.Value = string.Empty;
                    this.DestinationAirportTextBox.Text = string.Empty;

                    this.BindDestinationAirports(departureAirprotID);
                }
            }
        }

        protected void OnSelectDestinationAirportButtonClicked(object sender, EventArgs e)
        {
            var selectedAirport = sender as LinkButton;

            if (selectedAirport != null)
            {
                this.DestinationAirportTextBox.Text = selectedAirport.Text;

                int destinationAirportID;
                bool isValidID = int.TryParse(selectedAirport.CommandArgument, out destinationAirportID);

                if (isValidID)
                {
                    this.DestinationAirportIdHiddenField.Value = destinationAirportID.ToString();
                }
            }
        }

        protected void OnFlightSearchButtonClicked(object sender, EventArgs e)
        {
            if (this.Page.IsValid && !string.IsNullOrEmpty(this.DepartureAirportIdHiddenField.Value) &&
                !string.IsNullOrEmpty(this.DestinationAirportIdHiddenField.Value) && 
                this.DepartureDateTextBox.Text != string.Empty)
            {
                DateTime departureDate = DateTime.Parse(this.DepartureDateTextBox.Text);
                DateTime? arrivalDate = null;

                if (this.ReturnRouteRadioButton.Checked && this.ArrivalDateTextBox.Visible && 
                    this.ArrivalDateTextBox.Text != string.Empty)
                {
                    arrivalDate = DateTime.Parse(this.ArrivalDateTextBox.Text);
                }

                if (arrivalDate != null && departureDate > arrivalDate)
                {
                    this.InvalidDatesCustomValidator.ErrorMessage = "Arrival date cannot be earlier than departure date!";
                    this.InvalidDatesCustomValidator.IsValid = false;
                    return;
                }

                this.SearchFlight(this.DepartureAirportIdHiddenField.Value, this.DestinationAirportIdHiddenField.Value,
                    departureDate, arrivalDate);
            }
        }

        protected void OnCheapFlightLinkButtonClicked(object sender, EventArgs e)
        {
            var legInstanceLinkBtn = sender as LinkButton;

            if (legInstanceLinkBtn != null)
            {
                int legInstanceId = int.Parse(legInstanceLinkBtn.CommandArgument);
                var legInstance = this.LegInstancesServices.GetLegInstance(legInstanceId);

                if (legInstance == null)
                {
                    return;
                }

                string departureAirportId = legInstance
                    .FlightLeg
                    .DepartureAirportId
                    .ToString();

                string destinationAirportId = legInstance
                    .FlightLeg
                    .ArrivalAirportId
                    .ToString();

                this.SearchFlight(departureAirportId, destinationAirportId, legInstance.DepartureDateTime, null);
            }
        }

        private void SearchFlight(string departureAirportId, string destinationAirportId,
            DateTime? departureDate, DateTime? arrivalDate)
        {
            this.Session.Add(NativeConstants.DEPARTURE_AIRPORT_ID, departureAirportId);
            this.Session.Add(NativeConstants.DESTINATION_AIRPORT_ID, destinationAirportId);
            this.Session.Add(NativeConstants.DEPARTURE_DATE, departureDate);
            this.Session.Add(NativeConstants.ARRIVAL_DATE, arrivalDate);

            this.Response.Redirect(Pages.SELECT_FLIGHT);
        }

        private void BindDestinationAirports(int departureAirportID)
        {
            var destinationAirports = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.FlightLeg.DepartureAirportId == departureAirportID)
                .Select(l => l.FlightLeg.Route.Destination)
                .Distinct()
                .OrderBy(a => a.Country.Name)
                .ToList();

            if (destinationAirports == null || destinationAirports.Count == 0)
            {
                this.NoFlightsLiteral.Visible = true;
            }
            else
            {
                this.NoFlightsLiteral.Visible = false;
            }

            this.DestinationAirportsRepeater.DataSource = destinationAirports;
            this.DestinationAirportsRepeater.DataBind();
        }

        protected void ReturnRouteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ArrivalDateTextBox.Visible = true;
            this.ArrivalDateFancyTextBox.Visible = true;
        }

        protected void OnewayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.ArrivalDateTextBox.Visible = false;
            this.ArrivalDateFancyTextBox.Visible = false;
        }
    }
}