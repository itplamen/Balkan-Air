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

    public class FlightLegsManagementPresenter : Presenter<IFlightLegsManagementView>
    {
        private readonly IFlightLegsServices flightLegsServices;
        private readonly IAirportsServices airportsServices;
        private readonly IFlightsServices flightsServices;
        private readonly IRoutesServices routesServices;
        private readonly ILegInstancesServices legInstancesServices;

        public FlightLegsManagementPresenter(
            IFlightLegsManagementView view, 
            IFlightLegsServices flightLegsServices,
            IAirportsServices airportsServices, 
            IFlightsServices flightsServices, 
            IRoutesServices routesServices, 
            ILegInstancesServices legInstancesServices)
            : base(view)
        {
            if (flightLegsServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightLegsServices));
            }

            if (airportsServices == null)
            {
                throw new ArgumentNullException(nameof(IAirportsServices));
            }

            if (flightsServices == null)
            {
                throw new ArgumentNullException(nameof(IFlightsServices));
            }

            if (routesServices == null)
            {
                throw new ArgumentNullException(nameof(IRoutesServices));
            }

            if (legInstancesServices == null)
            {
                throw new ArgumentNullException(nameof(ILegInstancesServices));
            }

            this.flightLegsServices = flightLegsServices;
            this.airportsServices = airportsServices;
            this.flightLegsServices = flightLegsServices;
            this.flightsServices = flightsServices;
            this.routesServices = routesServices;
            this.legInstancesServices = legInstancesServices;

            this.View.OnFlightLegsGetData += this.View_OnFlightLegsGetData;
            this.View.OnFlightLegsUpdateItem += this.View_OnFlightLegsUpdateItem;
            this.View.OnFlightLegsDeleteItem += this.View_OnFlightLegsDeleteItem;
            this.View.OnFlightLegsAddItem += this.View_OnFlightLegsAddItem;
            this.View.OnAirportsGetData += this.View_OnAirportsGetData;
            this.View.OnFlightsGetData += this.View_OnFlightsGetData;
            this.View.OnRoutesGetData += this.View_OnRoutesGetData;
            this.View.OnLegInstancesGetData += this.View_OnLegInstancesGetData;
            this.View.OnAirportGetItem += this.View_OnAirportGetItem;
        }

        private void View_OnFlightLegsGetData(object sender, EventArgs e)
        {
            this.View.Model.FlightLegs = this.flightLegsServices.GetAll();
        }

        private void View_OnFlightLegsUpdateItem(object sender, FlightLegsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightLegsManagementEventArgs));
            }

            var flightLeg = this.flightLegsServices.GetFlightLeg(e.Id);

            if (flightLeg == null)
            {
                this.View.ModelState.AddModelError(
                    ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));

                return;
            }

            this.View.TryUpdateModel(flightLeg);

            if (this.View.ModelState.IsValid)
            {
                flightLeg.DepartureAirportId = e.DepartureAirportId;
                flightLeg.ArrivalAirportId = e.ArrivalAirportId;
                flightLeg.FlightId = e.FlightId;
                flightLeg.RouteId = e.RouteId;

                this.flightLegsServices.UpdateFlightLeg(e.Id, flightLeg);
            }
        }

        private void View_OnFlightLegsDeleteItem(object sender, FlightLegsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightLegsManagementEventArgs));
            }

            this.flightLegsServices.DeleteFlightLeg(e.Id);
        }

        private void View_OnFlightLegsAddItem(object sender, FlightLegsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightLegsManagementEventArgs));
            }

            var newFlightLeg = new FlightLeg()
            {
                DepartureAirportId = e.DepartureAirportId,
                ScheduledDepartureDateTime = e.ScheduledDepartureDateTime,
                ArrivalAirportId = e.ArrivalAirportId,
                ScheduledArrivalDateTime = e.ScheduledArrivalDateTime,
                FlightId = e.FlightId,
                RouteId = e.RouteId
            };

            e.Id = this.flightLegsServices.AddFlightLeg(newFlightLeg);
        }

        private void View_OnAirportsGetData(object sender, EventArgs e)
        {
            this.View.Model.Airports = this.airportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation)
                .Select(a => new
                {
                    Id = a.Id,
                    AirportInfo = "Id:" + a.Id + ", " + a.Name + " (" + a.Abbreviation + ")"
                });
        }

        private void View_OnFlightsGetData(object sender, EventArgs e)
        {
            this.View.Model.Flights = this.flightsServices.GetAll()
                .OrderBy(f => f.Id)
                .Select(f => new
                {
                    Id = f.Id,
                    FlightInfo = "Id:" + f.Id + " " + f.Number
                });
        }

        private void View_OnRoutesGetData(object sender, EventArgs e)
        {
            this.View.Model.Routes = this.routesServices.GetAll()
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name)
                .Select(r => new
                {
                    Id = r.Id,
                    RouteInfo = "Id:" + r.Id + " " + r.Origin.Name + " (" + r.Origin.Abbreviation + ")  -> " +
                                r.Destination.Name + " (" + r.Destination.Abbreviation + ")"
                });
        }

        private void View_OnLegInstancesGetData(object sender, EventArgs e)
        {
            this.View.Model.LegInstances = this.legInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.DepartureDateTime)
                .Select(l => new
                {
                    Id = l.Id,
                    LegInstanceInfo = "Id:" + l.Id + " " + l.DepartureDateTime + " -> " +
                                        l.ArrivalDateTime + ", " + l.FlightStatus.Name
                });
        }

        private void View_OnAirportGetItem(object sender, FlightLegsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(FlightLegsManagementEventArgs));
            }

            var airport = this.airportsServices.GetAirport(e.AirportId);

            if (airport == null)
            {
                this.View.Model.AirportInfo = ErrorMessages.AIRPORT_NOT_FOUND;
            }
            else
            {
                this.View.Model.AirportInfo = "Id:" + airport.Id + " " + airport.Name + " (" + airport.Abbreviation + ")";
            }
        }
    }
}
