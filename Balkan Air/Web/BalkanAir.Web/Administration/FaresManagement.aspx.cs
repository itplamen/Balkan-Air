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

    [PresenterBinding(typeof(FaresManagementPresenter))]
    public partial class FaresManagement : MvpPage<FaresManagementViewModel>, IFaresManagementView
    {
        public event EventHandler OnFaresGetData;
        public event EventHandler<FaresManagementEventArgs> OnFaresUpdateItem;
        public event EventHandler<FaresManagementEventArgs> OnFaresDeleteItem;
        public event EventHandler<FaresManagementEventArgs> OnFaresAddItem;
        public event EventHandler OnRoutesGetData;

        public IQueryable<Fare> FaresGridView_GetData()
        {
            this.OnFaresGetData?.Invoke(null, null);

            return this.Model.Fares;    
        }

        public void FaresGridView_UpdateItem(int id)
        {
            this.OnFaresUpdateItem?.Invoke(
                null, 
                new FaresManagementEventArgs()
                {
                    Id = id,
                    RouteId = int.Parse(this.RouteIdHiddenField.Value)
                });
        }

        public void FaresGridView_DeleteItem(int id)
        {
            this.OnFaresDeleteItem?.Invoke(null, new FaresManagementEventArgs() { Id = id });
        }

        public IQueryable<object> RoutesDropDownList_GetData()
        {
            this.OnRoutesGetData?.Invoke(null, null);

            return this.Model.Routes;
        }

        protected void CreateFaretBtn_Click(object sender, EventArgs e)
        {
            decimal price = this.GetValidPrice();

            if (this.Page.IsValid && price != -1)
            {
                var fareEventArgs = new FaresManagementEventArgs()
                {
                    Price = price,
                    RouteId = int.Parse(this.AddRoutesDropDownList.SelectedItem.Value)
                };

                this.OnFaresAddItem?.Invoke(sender, fareEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedFareIdLiteral.Text = fareEventArgs.Id.ToString();

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