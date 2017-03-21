namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class AirportsManagementPresenter : Presenter<IAirportsManagementView>
    {
        private readonly IAirportsServices airportsServices;
        private readonly ICountriesServices countriesServices;

        public AirportsManagementPresenter(IAirportsManagementView view, IAirportsServices airportsServices, ICountriesServices countriesServices)
            : base(view)
        {
            if (airportsServices == null)
            {
                throw new ArgumentNullException(nameof(IAirportsServices));
            }

            if (countriesServices == null)
            {
                throw new ArgumentNullException(nameof(ICountriesServices));
            }

            this.airportsServices = airportsServices;
            this.countriesServices = countriesServices;

            this.View.OnAirprotsGetData += this.View_OnAirprotsGetData;
            this.View.OnAirportsUpdateItem += this.View_OnAirprotsUpdateItem;
            this.View.OnAirportsDeleteItem += this.View_OnAirportsDeleteItem;
            this.View.OnAirprotsAddItem += this.View_OnAirprotsAddItem;
            this.View.OnCountriesGetData += this.View_OnCountriesGetData;
        }

        private void View_OnAirprotsGetData(object sender, EventArgs e)
        {
            this.View.Model.Airports = this.airportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation);
        }

        private void View_OnAirprotsUpdateItem(object sender, AirportsManagementEventArgs e)
        {
            var airport = this.airportsServices.GetAirport(e.Id);

            if (airport == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(airport);

            if (this.View.ModelState.IsValid)
            {
                airport.CountryId = e.CountryId;
                this.airportsServices.UpdateAirport(e.Id, airport);
            }
        }

        private void View_OnAirportsDeleteItem(object sender, AirportsManagementEventArgs e)
        {
            this.airportsServices.DeleteAirport(e.Id);
        }

        private void View_OnAirprotsAddItem(object sender, AirportsManagementEventArgs e)
        {
            var airport = new Airport()
            {
                Name = e.Name,
                Abbreviation = e.Abbreviation,
                CountryId = e.CountryId
            };

            e.Id = this.airportsServices.AddAirport(airport);
        }

        private void View_OnCountriesGetData(object sender, EventArgs e)
        {
            this.View.Model.Countries = this.countriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name);
        }
    }
}
