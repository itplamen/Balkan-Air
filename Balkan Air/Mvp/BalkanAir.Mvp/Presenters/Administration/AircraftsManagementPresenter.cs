namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class AircraftsManagementPresenter : Presenter<IAircraftsManagementView>
    {
        private readonly IAircraftsServices aircraftsServices;
        private readonly IAircraftManufacturersServices aircraftManufacturersServices;

        public AircraftsManagementPresenter(IAircraftsManagementView view, IAircraftsServices aircraftsServices, 
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

        private void View_OnAircraftsUpdateItem(object sender, AircraftsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(AircraftsManagementEventArgs));
            }

            var aircraft = this.aircraftsServices.GetAircraft(e.Id);

            if (aircraft == null)
            {
                this.View.ModelState.AddModelError(ErrorMessages.MODEL_ERROR_KEY, string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));
                return;
            }

            this.View.TryUpdateModel(aircraft);

            if (this.View.ModelState.IsValid)
            {
                if (e.AircraftManufacturerId <= 0)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
                }

                aircraft.AircraftManufacturerId = e.AircraftManufacturerId;
                this.aircraftsServices.UpdateAircraft(e.Id, aircraft);
            }
        }

        private void View_OnAircraftsDeleteItem(object sender, AircraftsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(AircraftsManagementEventArgs));
            }

            this.aircraftsServices.DeleteAircraft(e.Id);
        }

        private void View_OnAircraftsAddItem(object sender, AircraftsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(AircraftsManagementEventArgs));
            }

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
            this.View.Model.AircraftManufacturers = this.aircraftManufacturersServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name);
        }
    }
}
