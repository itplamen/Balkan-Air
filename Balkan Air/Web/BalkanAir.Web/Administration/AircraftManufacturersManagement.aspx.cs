namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class AircraftManufacturersManagement : Page
    {
        [Inject]
        public IAircraftManufacturersServices AircraftManufacturersServices { get; set; }

        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        public IQueryable<AircraftManufacturer> AircraftsManufacturersGridView_GetData()
        {
            return this.AircraftManufacturersServices.GetAll()
                .OrderBy(a => a.Name);
        }

        public void AircraftsManufacturersGridView_UpdateItem(int id)
        {
            var manufacturer = this.AircraftManufacturersServices.GetManufacturer(id);

            if (manufacturer == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(manufacturer);
            if (ModelState.IsValid)
            {
                this.AircraftManufacturersServices.UpdateManufacturer(id, manufacturer);
            }
        }

        public void AircraftsManufacturersGridView_DeleteItem(int id)
        {
            this.AircraftManufacturersServices.DeleteManufacturer(id);
        }

        public IQueryable<object> AircraftsListBox_GetData()
        {
            var aircrafts = this.AircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = "Id:" + a.Id + " " + a.AircraftManufacturer.Name + " " + a.Model
                });

            return aircrafts;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateAircraftManufacturerBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var manufacturer = new AircraftManufacturer() { Name = this.AircraftManufacturerNameTextBox.Text };
                int id = this.AircraftManufacturersServices.AddManufacturer(manufacturer);

                this.SuccessPanel.Visible = true;
                this.AddedManufacturerIdLiteral.Text = id.ToString();

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