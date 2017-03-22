namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class FaresManagementPresenter : Presenter<IFaresManagementView>
    {
        private readonly IFaresServices faresServices;
        private readonly IRoutesServices routesServices;

        public FaresManagementPresenter(IFaresManagementView view, IFaresServices faresServices, IRoutesServices routesServices)
            : base(view)
        {
            if (faresServices == null)
            {
                throw new ArgumentNullException(nameof(IFaresServices));
            }

            if (routesServices == null)
            {
                throw new ArgumentNullException(nameof(IRoutesServices));
            }

            this.faresServices = faresServices;
            this.routesServices = routesServices;

            this.View.OnFaresGetData += this.View_OnFaresGetData;
            this.View.OnFaresUpdateItem += this.View_OnFaresUpdateItem;
            this.View.OnFaresDeleteItem += this.View_OnFaresDeleteItem;
            this.View.OnFaresAddItem += this.View_OnFaresAddItem;
            this.View.OnRoutesGetData += this.View_OnRoutesGetData;
        }

        private void View_OnFaresGetData(object sender, EventArgs e)
        {
            this.View.Model.Fares = this.faresServices.GetAll();
        }

        private void View_OnFaresUpdateItem(object sender, FaresManagementEventArgs e)
        {
            var fare = this.faresServices.GetFare(e.Id);

            if (fare == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(fare);

            if (this.View.ModelState.IsValid)
            {
                fare.RouteId = e.RouteId;
                this.faresServices.UpdateFare(e.Id, fare);
            }
        }

        private void View_OnFaresDeleteItem(object sender, FaresManagementEventArgs e)
        {
            this.faresServices.DeleteFare(e.Id);
        }

        private void View_OnFaresAddItem(object sender, FaresManagementEventArgs e)
        {
            var fare = new Fare()
            {
                Price = e.Price,
                RouteId = e.RouteId
            };

            e.Id = this.faresServices.AddFare(fare);
        }

        private void View_OnRoutesGetData(object sender, EventArgs e)
        {
            this.View.Model.Routes = this.routesServices.GetAll()
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name)
                .Select(r => new
                {
                    Id = r.Id,
                    RouteInfo = "Id:" + r.Id + " " + r.Origin.Name + " -> " + r.Destination.Name
                });
        }
    }
}
