namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class AircraftsManagement : Page
    {
        [Inject]
        public IAircraftManufacturersServices AircraftManufacturersServices { get; set; }

        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        public IQueryable<Aircraft> AircraftsGridView_GetData()
        {
            return this.AircraftsServices.GetAll()
                .OrderBy(a => a.Model);
        }

        public void AircraftsGridView_UpdateItem(int id)
        {
            var aircraft = this.AircraftsServices.GetAircraft(id);

            if (aircraft == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(aircraft);
            if (ModelState.IsValid)
            {
                this.AircraftsServices.UpdateAircraft(id, aircraft);
            }
        }

        public void AircraftsGridView_DeleteItem(int id)
        {
            this.AircraftsServices.DeleteAircraft(id);
        }

        public IQueryable<AircraftManufacturer> AircraftManufacturersDropDownList_GetData()
        {
            return this.AircraftManufacturersServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
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
            if (Page.IsValid)
            {
                var aircraft = new Aircraft()
                {
                    Model = this.AircraftModelTextBox.Text.ToUpper(),
                    TotalSeats = int.Parse(this.TotalSeatsTextBox.Text),
                    AircraftManufacturerId = int.Parse(this.AircraftManufacturersDropDownList.SelectedItem.Value)
                };

                this.AircraftsServices.AddAircraft(aircraft);
            }
        }
    }
}