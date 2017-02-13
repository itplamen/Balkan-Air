namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageTravelClasses : Page
    {
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        public IQueryable<TravelClass> ManageTravelClassesGridView_GetData()
        {
            return this.TravelClassesServices.GetAll()
                .OrderBy(t => t.FlightId);
        }
        public void ManageTravelClassesGridView_UpdateItem(int id)
        {
            var travelClass = this.TravelClassesServices.GetTravelClass(id);

            if (travelClass == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(travelClass);

            if (ModelState.IsValid)
            {
                this.TravelClassesServices.UpdateTravelClass(id, travelClass);
            }
        }

        public void ManageTravelClassesGridView_DeleteItem(int id)
        {
            this.TravelClassesServices.DeleteTravelClass(id);
        }

        public IQueryable<object> FlightsDropDownList_GetData()
        {
            var flights = this.FlightsServices.GetAll()
                .Where(f => !f.IsDeleted)
                .OrderBy(f => f.DepartureAirport.Name)
                .ThenBy(f => f.ArrivalAirport.Name)
                .Select(f => new
                {
                    Id = f.Id,
                    FlightInfo = f.Number + ", " + f.DepartureAirport.Name + " -> " + f.ArrivalAirport.Name
                });

            return flights;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var travelClassTypes = Enum.GetValues(typeof(TravelClassType));
 
                this.TravelClassTypeDropDownList.DataSource = travelClassTypes;
                this.TravelClassTypeDropDownList.DataBind();
            }
        }

        protected void CreatTravelClassBtn_Click(object sender, EventArgs e)
        {
            decimal price = this.GetValidPrice();

            if (this.Page.IsValid && price != -1)
            {
                TravelClassType type;
                bool isValid = Enum.TryParse(this.TravelClassTypeDropDownList.SelectedItem.Text, out type);

                if (!isValid)
                {
                    return;
                }

                var travelClass = new TravelClass()
                {
                    Type = type,
                    Meal = this.MealTextBox.Text,
                    PriorityBoarding = this.PriorityBoardingCheckBox.Checked,
                    ReservedSeat = this.ReservedSeatCheckBox.Checked,
                    EarnMiles = this.EarnMilesCheckBox.Checked,
                    Price = price,
                    FlightId = int.Parse(this.FlightsDropDownList.SelectedItem.Value)
                };

                this.TravelClassesServices.AddTravelClass(travelClass);
                this.SeatsServices.AddSeatsToTravelClass(travelClass);
            }
        }

        private decimal GetValidPrice()
        {
            decimal price;
            bool isValid = decimal.TryParse(this.PriceTextBox.Text, out price);

            if (isValid && price >= ValidationConstants.MIN_PRICE && price <= ValidationConstants.MAX_PRICE)
            {
                this.PriceTextBox.BorderColor = Color.Empty;
                return price;
            }

            this.PriceTextBox.BorderColor = Color.Red;
            return -1;
        }
    }
}