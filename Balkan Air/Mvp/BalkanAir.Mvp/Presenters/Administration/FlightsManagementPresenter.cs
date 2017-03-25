namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;

    using WebFormsMvp;

    using Common;
    using Data.Helper;
    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;
   
    public class FlightsManagementPresenter : Presenter<IFlightsManagementView>
    {
        private readonly IFlightsServices flightsServices;
        private readonly INumberGenerator numberGenerator;

        public FlightsManagementPresenter(IFlightsManagementView view, IFlightsServices flightsServices, 
            INumberGenerator numberGenerator) 
            : base(view)
        {
            if (flightsServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightsServices));
            }

            if (numberGenerator == null)
            {
                throw new ArgumentNullException(nameof(INumberGenerator));
            }

            this.flightsServices = flightsServices;
            this.numberGenerator = numberGenerator;

            this.View.OnFlightsGetData += this.View_OnFlightsGetData;
            this.View.OnFlightsUpdateItem += this.View_OnFlightsUpdateItem;
            this.View.OnFlightsDeleteItem += this.View_OnFlightsDeleteItem;
            this.View.OnFlightsAddItem += this.View_OnFlightsAddItem;
            this.View.OnUniqueFlightNumberGetItem += this.View_OnUniqueFlightNumberGetItem;
        }

        private void View_OnFlightsGetData(object sender, EventArgs e)
        {
            this.View.Model.Flights = this.flightsServices.GetAll();
        }

        private void View_OnFlightsUpdateItem(object sender, FlightsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightsManagementEventArgs));
            }

            var flight = this.flightsServices.GetFlight(e.Id);

            if (flight == null)
            {
                this.View.ModelState.AddModelError(ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));
                return;
            }

            this.View.TryUpdateModel(flight);

            if (this.View.ModelState.IsValid)
            {
                this.flightsServices.UpdateFlight(e.Id, flight);
            }
        }

        private void View_OnFlightsDeleteItem(object sender, FlightsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightsManagementEventArgs));
            }

            this.flightsServices.DeleteFlight(e.Id);
        }

        private void View_OnFlightsAddItem(object sender, FlightsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightsManagementEventArgs));
            }

            var flight = new Flight()
            {
                Number = e.Number
            };

            e.Id = this.flightsServices.AddFlight(flight);
        }

        private void View_OnUniqueFlightNumberGetItem(object sender, FlightsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightsManagementEventArgs));
            }

            e.Number = this.numberGenerator.GetUniqueFlightNumber();
        }
    }
}
