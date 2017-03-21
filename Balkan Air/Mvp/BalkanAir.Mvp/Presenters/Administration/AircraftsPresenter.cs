namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class AircraftsPresenter : Presenter<IAircraftsView>
    {
        private readonly IAircraftsServices aircraftsServices;
        private readonly IAircraftManufacturersServices aircraftManufacturersServices;

        public AircraftsPresenter(IAircraftsView view, IAircraftsServices aircraftsServices, 
            IAircraftManufacturersServices aircraftManufacturersServices)
            : base(view)
        {
            if (aircraftsServices == null)
            {
                throw new ArgumentNullException(nameof(IAircraftsServices));
            }

            if (aircraftManufacturersServices == null)
            {
                throw new ArgumentNullException(nameof(IAircraftManufacturersServices));
            }

            this.aircraftsServices = aircraftsServices;
            this.aircraftManufacturersServices = aircraftManufacturersServices;

            this.View.OnAircraftsGetData += this.View_OnAircraftsGetData;
            this.View.OnAircraftsUpdateItem += this.View_OnAircraftsUpdateItem;
            this.View.OnAircraftsDeleteItem += this.View_OnAircraftsDeleteItem;
            this.View.OnAircraftsAddItem += this.View_OnAircraftsAddItem;
            this.View.OnAircraftManufacturersGetData += this.View_OnAircraftManufacturersGetData;
        }

        private void View_OnAircraftsGetData(object sender, EventArgs e)
        {
            this.View.Model.Aircrafts = this.aircraftsServices.GetAll()
                .OrderBy(a => a.AircraftManufacturer.Name)
                .ThenBy(a => a.Model);
        }

        private void View_OnAircraftsUpdateItem(object sender, AircraftsEventArgs e)
        {
            var aircraft = this.aircraftsServices.GetAircraft(e.Id);

            if (aircraft == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(aircraft);

            if (this.View.ModelState.IsValid)
            {
                this.aircraftsServices.UpdateAircraft(e.Id, aircraft);
            }
        }

        private void View_OnAircraftsDeleteItem(object sender, AircraftsEventArgs e)
        {
            this.aircraftsServices.DeleteAircraft(e.Id);
        }

        private void View_OnAircraftsAddItem(object sender, AircraftsEventArgs e)
        {
            var aircraft = new Aircraft()
            {
                Model = e.Model,
                TotalSeats = e.TotalSeats,
                AircraftManufacturerId = e.AircraftManufacturerId
            };

            e.Id = this.aircraftsServices.AddAircraft(aircraft);
        }

        private void View_OnAircraftManufacturersGetData(object sender, EventArgs e)
        {
            this.View.Model.AircraftManufacturer = this.aircraftManufacturersServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
        }
    }
}
