namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Services.Data.Contracts;
    using Data.Models;

    public partial class ManageAircraftManufacturers : Page
    {
        [Inject]
        public IAircraftManufacturersServices AircraftManufacturersServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<AircraftManufacturer> ManageAircraftsManufacturersGridView_GetData()
        {
            return this.AircraftManufacturersServices.GetAll()
                .OrderBy(a => a.Name);
        }

        public void ManageAircraftsManufacturersGridView_UpdateItem(int id)
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

        public void ManageAircraftsManufacturersGridView_DeleteItem(int id)
        {
            this.AircraftManufacturersServices.DeleteManufacturer(id);
        }

        protected void CreateAircraftManufacturerBtn_Click(object sender, EventArgs e)
        {
            var manufacturer = new AircraftManufacturer() { Name = this.AircraftManufacturerNameTextBox.Text };
            this.AircraftManufacturersServices.AddManufacturer(manufacturer);
        }
    }
}