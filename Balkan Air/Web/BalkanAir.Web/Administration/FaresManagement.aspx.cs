namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class FaresManagement : System.Web.UI.Page
    {
        [Inject]
        public IFaresServices FaresServices { get; set; }

        [Inject]
        public IRoutesServices RoutesServices { get; set; }

        public IQueryable<Fare> FaresGridView_GetData()
        {
            return this.FaresServices.GetAll();
        }

        public void FaresGridView_UpdateItem(int id)
        {
            var fare = this.FaresServices.GetFare(id);

            if (fare == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(fare);

            if (ModelState.IsValid)
            {
                this.FaresServices.UpdateFare(id, fare);
            }
        }

        public void FaresGridView_DeleteItem(int id)
        {
            this.FaresServices.DeleteFare(id);
        }

        public IQueryable<object> RoutesDropDownList_GetData()
        {
            return this.RoutesServices.GetAll()
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name)
                .Select(r => new
                {
                    Id = r.Id,
                    RouteInfo = "Id:" + r.Id + " " + r.Origin.Name + " -> " + r.Destination.Name
                });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateFaretBtn_Click(object sender, EventArgs e)
        {
            decimal price = this.GetValidPrice();

            if (this.Page.IsValid && price != -1)
            {
                var fare = new Fare()
                {
                    Price = price,
                    RouteId = int.Parse(this.AddRoutesDropDownList.SelectedItem.Value)    
                };

                int id = this.FaresServices.AddFare(fare);

                this.SuccessPanel.Visible = true;
                this.AddedFareIdLiteral.Text = id.ToString();

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

            if (isValid && price >= 0)
            {
                this.PriceTextBox.BorderColor = Color.Empty;
                return price;
            }

            this.PriceTextBox.BorderColor = Color.Red;
            return -1;
        }

        private void ClearFields()
        {
            this.PriceTextBox.Text = string.Empty;
            this.AddRoutesDropDownList.SelectedIndex = 0;
        }
    }
}