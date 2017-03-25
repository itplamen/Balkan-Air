namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class TravelClassesManagement : Page
    {
        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        public IQueryable<TravelClass> TravelClassesGridView_GetData()
        {
            return this.TravelClassesServices.GetAll();
        }
        public void TravelClassesGridView_UpdateItem(int id)
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

        public void TravelClassesGridView_DeleteItem(int id)
        {
            this.TravelClassesServices.DeleteTravelClass(id);
        }

        public IQueryable<object> AircraftsDropDownList_GetData()
        {
            var aircrafts = this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = "Id: " + a.Id + ", " + a.AircraftManufacturer.Name + " " + a.Model + " " + a.TotalSeats + " seats"
                });

            return aircrafts;
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
                    NumberOfRows = int.Parse(this.NumberOfRowsTextBox.Text),
                    NumberOfSeats = int.Parse(this.NumberOfSeatsTextBox.Text),
                    Price = price,
                    AircraftId = int.Parse(this.AddAircraftsDropDownList.SelectedItem.Value)
                };

                int id = this.TravelClassesServices.AddTravelClass(travelClass);

                this.SuccessPanel.Visible = true;
                this.AddedTravelClassIdLiteral.Text = id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private decimal GetValidPrice()
        {
            decimal price;
            bool isValid = decimal.TryParse(this.PriceTextBox.Text, out price);

            if (isValid && price >= ValidationConstants.TRAVEL_CLASS_MIN_PRICE && 
                price <= ValidationConstants.TRAVEL_CLASS_MAX_PRICE)
            {
                this.PriceTextBox.BorderColor = Color.Empty;
                return price;
            }

            this.PriceTextBox.BorderColor = Color.Red;
            return -1;
        }

        private void ClearFields()
        {
            this.TravelClassTypeDropDownList.SelectedIndex = 0;
            this.MealTextBox.Text = string.Empty;
            this.PriorityBoardingCheckBox.Checked = false;
            this.ReservedSeatCheckBox.Checked = true;
            this.EarnMilesCheckBox.Checked = false;
            this.NumberOfRowsTextBox.Text = string.Empty;
            this.NumberOfSeatsTextBox.Text = string.Empty;
            this.PriceTextBox.Text = string.Empty;
            this.AddAircraftsDropDownList.SelectedIndex = 0;
        }
    }
}