namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(FlightsManagementPresenter))]
    public partial class FlightsManagement : MvpPage<FlightsManagementViewModel>, IFlightsManagementView
    {
        public event EventHandler OnFlightsGetData;

        public event EventHandler<FlightsManagementEventArgs> OnFlightsUpdateItem;

        public event EventHandler<FlightsManagementEventArgs> OnFlightsDeleteItem;

        public event EventHandler<FlightsManagementEventArgs> OnFlightsAddItem;

        public event EventHandler<FlightsManagementEventArgs> OnUniqueFlightNumberGetItem;

        public IQueryable<Flight> FlightsGridView_GetData()
        {
            this.OnFlightsGetData?.Invoke(null, null);

            return this.Model.Flights;
        }

        public void FlightsGridView_UpdateItem(int id)
        {
            this.OnFlightsUpdateItem?.Invoke(null, new FlightsManagementEventArgs() { Id = id });
        }

        public void FlightsGridView_DeleteItem(int id)
        {
            this.OnFlightsDeleteItem?.Invoke(null, new FlightsManagementEventArgs() { Id = id });
        }

        protected void GenerateFlightNumberBtn_Click(object sender, EventArgs e)
        {
            var flightEventArgs = new FlightsManagementEventArgs();

            this.OnUniqueFlightNumberGetItem?.Invoke(sender, flightEventArgs);

            this.AddFlightNumberTextBox.Text = flightEventArgs.Number;
        }

        protected void CreateFlightBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var flightEventArgs = new FlightsManagementEventArgs() { Number = this.AddFlightNumberTextBox.Text.ToUpper() };

                this.OnFlightsAddItem?.Invoke(sender, flightEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedFlightIdLiteral.Text = flightEventArgs.Id.ToString();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.AddFlightNumberTextBox.Text = string.Empty;
        }
    }
}