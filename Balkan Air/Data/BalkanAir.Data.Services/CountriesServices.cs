namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class CountriesServices : ICountriesServices
    {
        private readonly IRepository<Country> countries;

        public CountriesServices(IRepository<Country> countries)
        {
            this.countries = countries;
        }

        public int AddCountry(Country country)
        {
            this.countries.Add(country);
            this.countries.SaveChanges();

            return country.Id;
        }

        public Country GetCountry(int id)
        {
            return this.countries.GetById(id);
        }

        public IQueryable<Country> GetAll()
        {
            return this.countries.All();
        }

        public Country UpdateCountry(int id, Country country)
        {
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
