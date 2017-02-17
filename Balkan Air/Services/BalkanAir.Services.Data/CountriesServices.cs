namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class CountriesServices : ICountriesServices
    {
        private readonly IRepository<Country> countries;

        public CountriesServices(IRepository<Country> countries)
        {
            this.countries = countries;
        }

        public int AddCountry(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.countries.Add(country);
            this.countries.SaveChanges();

            return country.Id;
        }

        public Country GetCountry(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.countries.GetById(id);
        }

        public IQueryable<Country> GetAll()
        {
            return this.countries.All();
        }

        public Country UpdateCountry(int id, Country country)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (country == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var countryToUpdate = this.countries.GetById(id);

            if (countryToUpdate != null)
            {
                countryToUpdate.Name = country.Name;
                countryToUpdate.IsDeleted = country.IsDeleted;
                this.countries.SaveChanges();
            }

            return countryToUpdate;
        }

        public Country DeleteCountry(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var countryToDelete = this.countries.GetById(id);

            if (countryToDelete != null)
            {
                countryToDelete.IsDeleted = true;
                this.countries.SaveChanges();
            }

            return countryToDelete;
        }
    }
}
