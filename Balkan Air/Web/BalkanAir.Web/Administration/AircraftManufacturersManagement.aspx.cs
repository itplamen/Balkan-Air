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
  
    [PresenterBinding(typeof(AircraftManufacturersManagementPresenter))]
    public partial class AircraftManufacturersManagement : MvpPage<AircraftManufacturersManagementViewModel>, IAircraftManufacturersManagementView
    {
        public event EventHandler OnAircraftManufacturersGetData;

        public event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersUpdateItem;

        public event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersDeleteItem;

        public event EventHandler<AircraftManufacturersManagementEventArgs> OnAircraftManufacturersAddItem;

        public event EventHandler OnAircraftsGetData;

        public IQueryable<AircraftManufacturer> AircraftsManufacturersGridView_GetData()
        {
            this.OnAircraftManufacturersGetData?.Invoke(null, null);

            return this.Model.AircraftManufacturers; 
        }

        public void AircraftsManufacturersGridView_UpdateItem(int id)
        {
            this.OnAircraftManufacturersUpdateItem?.Invoke(null, new AircraftManufacturersManagementEventArgs() { Id = id });
        }

        public void AircraftsManufacturersGridView_DeleteItem(int id)
        {
            this.OnAircraftManufacturersDeleteItem?.Invoke(null, new AircraftManufacturersManagementEventArgs() { Id = id });
        }

        public IQueryable<object> AircraftsListBox_GetData()
        {
            this.OnAircraftsGetData?.Invoke(null, null);

            return this.Model.Aircrafts;
        }

        protected void CreateAircraftManufacturerBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var eventArgs = new AircraftManufacturersManagementEventArgs() { Name = this.AircraftManufacturerNameTextBox.Text };

                this.OnAircraftManufacturersAddItem?.Invoke(sender, eventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedManufacturerIdLiteral.Text = eventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.AircraftManufacturerNameTextBox.Text = string.Empty;
        }
    }
}