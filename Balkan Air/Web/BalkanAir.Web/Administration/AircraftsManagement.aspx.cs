namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Common;
    using Data.Models;

    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(AircraftsManagementPresenter))]
    public partial class AircraftsManagement : MvpPage<AircraftsManagementViewModels>, IAircraftsManagementView
    {
        public event EventHandler OnAircraftsGetData;
        public event EventHandler<AircraftsManagementEventArgs> OnAircraftsUpdateItem;
        public event EventHandler<AircraftsManagementEventArgs> OnAircraftsDeleteItem;
        public event EventHandler<AircraftsManagementEventArgs> OnAircraftsAddItem;
        public event EventHandler OnAircraftManufacturersGetData;

        public IQueryable<Aircraft> AircraftsGridView_GetData()
        {
            this.OnAircraftsGetData?.Invoke(null, null);

            return this.Model.Aircrafts;
        }

        public void AircraftsGridView_UpdateItem(int id)
        {
            this.OnAircraftsUpdateItem?.Invoke(null, new AircraftsManagementEventArgs()
            {
                Id = id,
                AircraftManufacturerId = int.Parse(this.ManufacturerIdHiddenField.Value)
            });
        }

        public void AircraftsGridView_DeleteItem(int id)
        {
            this.OnAircraftsDeleteItem?.Invoke(null, new AircraftsManagementEventArgs() { Id = id });
        }

        public IQueryable<AircraftManufacturer> AircraftManufacturersDropDownList_GetData()
        {
            this.OnAircraftManufacturersGetData?.Invoke(null, null);

            return this.Model.AircraftManufacturer;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.TotalSeatsTextBox.Text = ValidationConstants.AIRCRAFT_MAX_SEATS.ToString();
            }
        }

        protected void CreateAircraftBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var eventArgs = new AircraftsManagementEventArgs()
                {
                    Model = this.AircraftModelTextBox.Text.ToUpper(),
                    TotalSeats = int.Parse(this.TotalSeatsTextBox.Text),
                    AircraftManufacturerId = int.Parse(this.AircraftManufacturersDropDownList.SelectedItem.Value)
                };

                this.OnAircraftsAddItem?.Invoke(sender, eventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedAircraftIdLiteral.Text = eventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.AircraftModelTextBox.Text = string.Empty;
            this.AircraftManufacturersDropDownList.SelectedIndex = 0;
        }
    }
}