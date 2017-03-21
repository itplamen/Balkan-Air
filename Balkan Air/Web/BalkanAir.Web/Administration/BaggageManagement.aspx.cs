namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(BaggageManagementPresenter))]
    public partial class BaggageManagement : MvpPage<BaggageManagementViewModel>, IBaggageManagementView
    {
        public event EventHandler OnBaggageGetData;
        public event EventHandler<BaggageManagementEventArgs> OnBaggageUpdateItem;
        public event EventHandler<BaggageManagementEventArgs> OnBaggageDeleteItem;
        public event EventHandler<BaggageManagementEventArgs> OnBaggageAddItem;
        public event EventHandler OnBookingsGetData;

        public IQueryable<Baggage> BaggageGridView_GetData()
        {
            this.OnBaggageGetData?.Invoke(null, null);

            return this.Model.Baggage;
        }

        public void BaggageGridView_UpdateItem(int id)
        {
            this.OnBaggageUpdateItem?.Invoke(null, new BaggageManagementEventArgs() { Id = id });
        }

        public void BaggageGridView_DeleteItem(int id)
        {
            this.OnBaggageDeleteItem?.Invoke(null, new BaggageManagementEventArgs() { Id = id });
        }

        public IQueryable<object> BookingsDropDownList_GetData()
        {
            this.OnBookingsGetData?.Invoke(null, null);

            return this.Model.Bookings;
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

                var bagEventArgs = new BaggageManagementEventArgs()
                {
                    Type = type,
                    MaxKilograms = int.Parse(this.MaxKilogramsTextBox.Text),
                    Size = this.SizeTextBox.Text,
                    Price = price,
                    BookingId = int.Parse(this.BookingsDropDownList.SelectedItem.Value)
                };

                this.OnBaggageAddItem?.Invoke(sender, bagEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedBagIdLiteral.Text = bagEventArgs.Id.ToString();

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