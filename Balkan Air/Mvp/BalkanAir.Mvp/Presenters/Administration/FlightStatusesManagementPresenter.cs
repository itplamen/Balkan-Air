namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;
    using Data.Models;

    public class FlightStatusesManagementPresenter : Presenter<IFlightStatusesManagementView>
    {
        private readonly IFlightStatusesServices flightStatusesServices;

        public FlightStatusesManagementPresenter(IFlightStatusesManagementView view, IFlightStatusesServices flightStatusesServices) 
            : base(view)
        {
            if (flightStatusesServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightStatusesServices));
            }

            this.flightStatusesServices = flightStatusesServices;

            this.View.OnFlightStatusesGetData += this.View_OnFlightStatusesGetData;
            this.View.OnFlightStatusesUpdateItem += this.View_OnFlightStatusesUpdateItem;
            this.View.OnFlightStatusesDeleteItem += this.View_OnFlightStatusesDeleteItem;
            this.View.OnFlightStatusesAddItem += this.View_OnFlightStatusesAddItem;
        }

        private void View_OnFlightStatusesGetData(object sender, EventArgs e)
        {
            this.View.Model.FlightStatuses = this.flightStatusesServices.GetAll()
                .OrderBy(fs => fs.Name);
        }

        private void View_OnFlightStatusesUpdateItem(object sender, FlightStatusesManagementEventArgs e)
        {
            var flightStatus = this.flightStatusesServices.GetFlightStatus(e.Id);

            if (flightStatus == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(flightStatus);

            if (this.View.ModelState.IsValid)
            {
                this.flightStatusesServices.UpdateFlightStatus(e.Id, flightStatus);
            }
        }

        private void View_OnFlightStatusesDeleteItem(object sender, FlightStatusesManagementEventArgs e)
        {
            this.flightStatusesServices.DeleteFlightStatus(e.Id);
        }

        private void View_OnFlightStatusesAddItem(object sender, FlightStatusesManagementEventArgs e)
        {
            var flightStatus = new FlightStatus()
            {
                Name = e.Name
            };

            e.Id = this.flightStatusesServices.AddFlightStatus(flightStatus);
        }
    }
}
