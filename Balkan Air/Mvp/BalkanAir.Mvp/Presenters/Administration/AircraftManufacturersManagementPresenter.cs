namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class AircraftManufacturersManagementPresenter : Presenter<IAircraftManufacturersManagementView>
    {
        private readonly IAircraftManufacturersServices aircraftManufacturersServices;
        private readonly IAircraftsServices aircraftsServices;

        public AircraftManufacturersManagementPresenter(IAircraftManufacturersManagementView view,
            IAircraftManufacturersServices aircraftManufacturersServices, IAircraftsServices aircraftsServices)
            : base(view)
        {
            if (aircraftManufacturersServices == null)
            {
                throw new ArgumentNullException(nameof(IAircraftManufacturersServices));
            }

            if (aircraftsServices == null)
            {
                throw new ArgumentNullException(nameof(IAircraftsServices));
            }

            this.aircraftManufacturersServices = aircraftManufacturersServices;
            this.aircraftsServices = aircraftsServices;

            this.View.OnAircraftManufacturersGetData += this.View_OnAircraftManufacturersGetData;
            this.View.OnAircraftManufacturersUpdateItem += this.View_OnAircraftManufacturersUpdateItem;
            this.View.OnAircraftManufacturersDeleteItem += this.View_OnAircraftManufacturersDeleteItem;
            this.View.OnAircraftManufacturersAddItem += this.View_OnAircraftManufacturersAddItem;
            this.View.OnAircraftsGetData += this.View_OnAircraftsGetData;
        }

        private void View_OnAircraftManufacturersGetData(object sender, EventArgs e)
        {
            this.View.Model.AircraftManufacturers = this.aircraftManufacturersServices.GetAll()
                .OrderBy(a => a.Name);
        }

        private void View_OnAircraftManufacturersUpdateItem(object sender, AircraftManufacturersManagementEventArgs e)
        {
            var manufacturer = this.aircraftManufacturersServices.GetManufacturer(e.Id);

            if (manufacturer == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(manufacturer);

            if (this.View.ModelState.IsValid)
            {
                this.aircraftManufacturersServices.UpdateManufacturer(e.Id, manufacturer);
            }
        }

        private void View_OnAircraftManufacturersDeleteItem(object sender, AircraftManufacturersManagementEventArgs e)
        {
            this.aircraftManufacturersServices.DeleteManufacturer(e.Id);
        }

        private void View_OnAircraftManufacturersAddItem(object sender, AircraftManufacturersManagementEventArgs e)
        {
            var manufacturer = new AircraftManufacturer() { Name = e.Name };
            e.Id = this.aircraftManufacturersServices.AddManufacturer(manufacturer);
        }

        private void View_OnAircraftsGetData(object sender, EventArgs e)
        {
            this.View.Model.Aircrafts = this.aircraftsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .Select(a => new
                {
                    Id = a.Id,
                    AircraftInfo = "Id:" + a.Id + " " + a.AircraftManufacturer.Name + " " + a.Model
                });
        }
    }
}
