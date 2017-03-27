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

    public class CountriesManagementPresenter : Presenter<ICountriesManagementView>
    {
        private readonly ICountriesServices countriesServices;

        public CountriesManagementPresenter(ICountriesManagementView view, ICountriesServices countriesServices) 
            : base(view)
        {
            if (countriesServices == null)
            {
                throw new ArgumentNullException(nameof(ICountriesServices));
            }

            this.countriesServices = countriesServices;

            this.View.OnCountriesGetData += this.View_OnCountriesGetData;
            this.View.OnCountriesUpdateItem += this.View_OnCountriesUpdateItem;
            this.View.OnCountriesDeleteItem += this.View_OnCountriesDeleteItem;
            this.View.OnCountriesAddItem += this.View_OnCountriesAddItem;
        }

        private void View_OnCountriesGetData(object sender, EventArgs e)
        {
            this.View.Model.Countries = this.countriesServices.GetAll()
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Abbreviation);
        }

        private void View_OnCountriesUpdateItem(object sender, CountriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CountriesManagementEventArgs));
            }

            var country = this.countriesServices.GetCountry(e.Id);

            if (country == null)
            {
                this.View.ModelState.AddModelError(ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));
                return;
            }

            this.View.TryUpdateModel(country);

            if (this.View.ModelState.IsValid)
            {
                this.countriesServices.UpdateCountry(e.Id, country);
            }
        }

        private void View_OnCountriesDeleteItem(object sender, CountriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CountriesManagementEventArgs));
            }

            this.countriesServices.DeleteCountry(e.Id);
        }

        private void View_OnCountriesAddItem(object sender, CountriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CountriesManagementEventArgs));
            }

            var country = new Country()
            {
                Name = e.Name,
                Abbreviation = e.Abbreviation
            };

            e.Id = this.countriesServices.AddCountry(country);
        }
    }
}
