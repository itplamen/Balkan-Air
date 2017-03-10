namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class BaggageManagement : Page
    {
        [Inject]
        public IBaggageServices BaggageServices { get; set; }

        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        public IQueryable<Baggage> BaggageGridView_GetData()
        {
            return this.BaggageServices.GetAll();   
        }

        public void BaggageGridView_UpdateItem(int id)
        {
            var baggage = this.BaggageServices.GetBaggage(id);
            
            if (baggage == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(baggage);

            if (ModelState.IsValid)
            {
                this.BaggageServices.UpdateBaggage(id, baggage);
            }
        }

        public void BaggageGridView_DeleteItem(int id)
        {
            this.BaggageServices.DeleteBaggage(id);
        }

        public IQueryable<object> BookingsDropDownList_GetData()
        {
            var bookings = this.BookingsServices.GetAll()
                .Where(b => !b.IsDeleted)
                .Select(b => new
                {
                    Id = b.Id,
                    BookingInfo = "Id: " + b.Id + ", " + b.User.UserSettings.FirstName + " " + b.User.UserSettings.LastName
                });

            return bookings;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                var baggageTypes = Enum.GetValues(typeof(BaggageType));

                this.BaggageTypeDropDownList.DataSource = baggageTypes;
                this.BaggageTypeDropDownList.DataBind();
            }
        }

        protected void CreateBaggageBtn_Click(object sender, EventArgs e)
        {
            decimal price = this.GetValidPrice();

            if (this.Page.IsValid && price != -1)
            {
                BaggageType type;
                bool isValid = Enum.TryParse(this.BaggageTypeDropDownList.SelectedItem.Text, out type);

                if (!isValid)
                {
                    return;
                }

                var baggage = new Baggage()
                {
                    Type = type,
                    MaxKilograms = int.Parse(this.MaxKilogramsTextBox.Text),
                    Size = this.SizeTextBox.Text,
                    Price = price,
                    BookingId = int.Parse(this.BookingsDropDownList.SelectedItem.Value)
                };

                int id = this.BaggageServices.AddBaggage(baggage);

                this.SuccessPanel.Visible = true;
                this.AddedBagIdLiteral.Text = id.ToString();

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
            this.BaggageTypeDropDownList.SelectedIndex = 0;
            this.MaxKilogramsTextBox.Text = string.Empty;
            this.SizeTextBox.Text = string.Empty;
            this.PriceTextBox.Text = string.Empty;
            this.BookingsDropDownList.SelectedIndex = 0;
        }
    }
}