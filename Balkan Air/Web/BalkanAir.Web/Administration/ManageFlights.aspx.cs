namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data;
    using Data.Helper;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageFlights : Page
    {
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        public IQueryable<Flight> ManageFlightsGridView_GetData()
        {
            return this.FlightsServices.GetAll();
        }

        public void ManageFlightsGridView_UpdateItem(int id)
        {
            var flight = this.FlightsServices.GetFlight(id);

            if (flight == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(flight);
            if (ModelState.IsValid)
            {
                this.FlightsServices.UpdateFlight(id, flight);
            }
        }

        public void ManageFlightsGridView_DeleteItem(int id)
        {
            this.FlightsServices.DeleteFlight(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void GenerateFlightNumberBtn_Click(object sender, EventArgs e)
        {
            this.AddFlightNumberTextBox.Text = new FlightNumber(new BalkanAirDbContext()).GetUniqueFlightNumber();
        }

        protected void CreateFlightBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var newFlight = new Flight() { Number = this.AddFlightNumberTextBox.Text.ToUpper() };
                this.FlightsServices.AddFlight(newFlight);
            }
        }
    }
}