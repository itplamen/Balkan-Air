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

    using Data.Models;
    using Services.Data.Contracts;

    public partial class FlightInfo : Page
    {
        private IDictionary<string, string> orderFlightsBy = new Dictionary<string, string>()
        {
            { "number", "FlightLeg.Flight.Number" },
            { "fromAirport", "FlightLeg.Route.Origin.Name" },
            { "toAirport", "FlightLeg.Route.Destination.Name" }
        };

        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

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

            List<LegInstance> matchedFlights = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted && l.FlightLeg.Flight.Number.ToLower().Contains(flightNumber))
                .OrderBy(l => l.FlightLeg.Flight.Number)
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
            List<LegInstance> legInstances;

            if (!string.IsNullOrEmpty(orderBy) && this.orderFlightsBy.ContainsKey(orderBy))
            {
                legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(this.orderFlightsBy[orderBy] + " Ascending")
                .ToList();
            }
            else
            {
                legInstances = this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .ToList();
            }

            if (!string.IsNullOrEmpty(orderBy) && !this.orderFlightsBy.ContainsKey(orderBy))
            {
                HttpUtility.ParseQueryString(this.Request.Url.Query).Remove(orderBy);
                this.Response.Redirect(this.Request.Url.AbsolutePath);
            }

            this.FlightInfoGridView.DataSource = legInstances;
            this.FlightInfoGridView.DataBind();
        }   
    }
}