namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class RoutesManagementPresenter : Presenter<IRoutesManagementView>
    {
        private readonly IRoutesServices routesServices;
        private readonly IAirportsServices airportsServices;

        public RoutesManagementPresenter(IRoutesManagementView view, IRoutesServices routesServices, 
            IAirportsServices airportsServices) 
            : base(view)
        {
            if (routesServices == null)
            {
                throw new ArgumentNullException(nameof(IRoutesServices));
            }

            if (airportsServices == null)
            {
                throw new ArgumentNullException(nameof(IAirportsServices));
            }

            this.routesServices = routesServices;
            this.airportsServices = airportsServices;

            this.View.OnRoutesGetData += this.View_OnRoutesGetData;
            this.View.OnRoutesUpdateItem += this.View_OnRoutesUpdateItem;
            this.View.OnRoutesDeleteItem += this.View_OnRoutesDeleteItem;
            this.View.OnRoutesAddItem += this.View_OnRoutesAddItem;
            this.View.OnAirportsGetData += this.View_OnAirportsGetData;
        }

        private void View_OnRoutesGetData(object sender, EventArgs e)
        {
            this.View.Model.Routes = this.routesServices.GetAll()
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name);
        }

        private void View_OnRoutesUpdateItem(object sender, RoutesManagementEventArgs e)
        {
            var route = this.routesServices.GetRoute(e.Id);

            if (route == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(route);

            if (this.View.ModelState.IsValid)
            {
                route.OriginId = e.OriginId;
                route.DestinationId = e.DestinationId;

                this.routesServices.UpdateRoute(e.Id, route);
            }
        }

        private void View_OnRoutesDeleteItem(object sender, RoutesManagementEventArgs e)
        {
            this.routesServices.DeleteRoute(e.Id);
        }

        private void View_OnRoutesAddItem(object sender, RoutesManagementEventArgs e)
        {
            var route = new Route()
            {
                OriginId = e.OriginId,
                DestinationId = e.DestinationId,
                DistanceInKm = e.DistanceInKm
            };

            e.Id = this.routesServices.AddRoute(route);
        }

        private void View_OnAirportsGetData(object sender, EventArgs e)
        {
            this.View.Model.Airports = this.airportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation)
                .Select(a => new
                {
                    Id = a.Id,
                    AirportInfo = "Id:" + a.Id + ", " + a.Name + " (" + a.Abbreviation + ")"
                });
        }
    }
}
