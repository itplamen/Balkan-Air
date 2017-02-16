namespace BalkanAir.Web 
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Common;
    using Services.Data.Contracts;

    public partial class _Default : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public INewsServices NewsServices { get; set; }

        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.NoFlightsLiteral.Visible = false;
            }
        }

        public IEnumerable<Airport> DepartureAirportsRepeater_GetData()
        {
            return this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Country.Name)
                .ThenBy(a => a.Name)
                .ToList();
        }

        public IEnumerable<Flight> TopCheapestFlightsRepeater_GetData()
        {
            return this.FlightsServices.GetAll()
                .Where(f => !f.IsDeleted)
                //.OrderBy(f => f.TravelClasses.FirstOrDefault(tr => tr.Type == TravelClassType.Economy).Price)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Data.Models.News> LatestNewsRepeater_GetData()
        {
            return this.NewsServices.GetAll().
                Where(a => !a.IsDeleted)
                .Take(3)
                .ToList();
        }

        protected void OnSelectDepartureAirportButtonClicked(object sender, EventArgs e)
        {
            var selectedAirport = sender as LinkButton;

            if (selectedAirport != null)
            {
                this.DepartureAirportTextBox.Text = selectedAirport.Text;

                int departureAirprotID;
                bool isValidID = int.TryParse(selectedAirport.CommandArgument, out departureAirprotID);

                if (isValidID)
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
            if (!string.IsNullOrEmpty(this.DepartureAirportIdHiddenField.Value) &&
                !string.IsNullOrEmpty(this.DestinationAirportIdHiddenField.Value))
            {
                this.SearchFlight(this.DepartureAirportIdHiddenField.Value, this.DestinationAirportIdHiddenField.Value);
            }
        }

        protected void OnCheapFlightLinkButtonClicked(object sender, EventArgs e)
        {
            var cheapFlight = sender as LinkButton;

            if (cheapFlight != null)
            {
                int cheapFlightId = int.Parse(cheapFlight.CommandArgument);
                //string departureAirportId = this.FlightsServices.GetFlight(cheapFlightId).DepartureAirport.Id.ToString();
                //string destinationAirportId = this.FlightsServices.GetFlight(cheapFlightId).ArrivalAirport.Id.ToString();

                //this.SearchFlight(departureAirportId, destinationAirportId);
            }
        }

        protected void OnLatestArticleLinkButtonClicked(object sender, EventArgs e)
        {
            var selectedArticle = sender as LinkButton;

            if (selectedArticle != null)
            {
                int articleId = int.Parse(selectedArticle.CommandArgument);
                //string departureAirportId = this.FlightsServices.GetFlight(articleId).DepartureAirport.Id.ToString();
                //string destinationAirportId = this.FlightsServices.GetFlight(articleId).ArrivalAirport.Id.ToString();

                //this.SearchFlight(departureAirportId, destinationAirportId);
            }
        }

        private void SearchFlight(string departureAirportId, string destinationAirportId)
        {
            this.Session.Add(NativeConstants.DEPARTURE_AIRPORT_ID, departureAirportId);
            this.Session.Add(NativeConstants.DESTINATION_AIRPORT_ID, destinationAirportId);
            this.Response.Redirect(Pages.SELECT_FLIGHT);
        }

        private void BindDestinationAirports(int departureAirportID)
        {
            var destinationAirports = this.FlightsServices.GetAll()
                //.Where(f => !f.IsDeleted && f.DepartureAirport.Id == departureAirportID)
                //.Select(f => f.ArrivalAirport)
                .Distinct()
                //.OrderBy(a => a.Country.Name)
                .ToList();

            //if (destinationAirports == null || destinationAirports.Count == 0)
            //{
            //    this.NoFlightsLiteral.Visible = true;
            //}
            //else
            //{
            //    this.NoFlightsLiteral.Visible = false;
            //}

            this.DestinationAirportsRepeater.DataSource = destinationAirports;
            this.DestinationAirportsRepeater.DataBind();
        }

        
    }
}