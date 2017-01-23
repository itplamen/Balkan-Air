namespace BalkanAir.Web
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Services.Contracts;
    
    public partial class FlightInfo : Page
    {
        private IDictionary<string, string> orderFlightsBy = new Dictionary<string, string>()
        {
            { "number", "Number" },
            { "fromAirport", "FromAirport.Name" },
            { "toAirport", "ToAirport.Name" }
        };

        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindGridViewWithAllFlights();
            }
        }

        protected void SearchFligthNumberBtn_Click(object sender, EventArgs e)
        {
            string flightNumber = this.SearchedFlightNumberTextBox.Text.ToLower();

            List<Flight> matchedFlights = this.FlightsServices.GetAll()
                .Where(f => !f.IsDeleted && f.Number.ToLower().Contains(flightNumber))
                .OrderBy(f => f.Number)
                .ToList();

            this.FlightInfoGridView.DataSource = matchedFlights;
            this.FlightInfoGridView.DataBind();
        }

        protected void FlightInfoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.FlightInfoGridView.PageIndex = e.NewPageIndex;
            this.BindGridViewWithAllFlights();
        }

        private void BindGridViewWithAllFlights()
        {
            string orderBy = this.Request.QueryString["orderBy"];
            List<Flight> flights;

            if (!string.IsNullOrEmpty(orderBy) && this.orderFlightsBy.ContainsKey(orderBy))
            {
                flights = this.FlightsServices.GetAll()
                .Where(f => !f.IsDeleted)
                .OrderBy(this.orderFlightsBy[orderBy] + " Ascending")
                .ToList();
            }
            else
            {
                flights = this.FlightsServices.GetAll()
                .Where(f => !f.IsDeleted)
                .ToList();
            }

            if (!string.IsNullOrEmpty(orderBy) && !this.orderFlightsBy.ContainsKey(orderBy))
            {
                HttpUtility.ParseQueryString(this.Request.Url.Query).Remove(orderBy);
                this.Response.Redirect(this.Request.Url.AbsolutePath);
            }

            this.FlightInfoGridView.DataSource = flights;
            this.FlightInfoGridView.DataBind();
        }   
    }
}