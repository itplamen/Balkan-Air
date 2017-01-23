namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface ICountriesServices
    {
        int AddCountry(Country country);

        Country GetCountry(int id);

        IQueryable<Country> GetAll();

        Country UpdateCountry(int id, Country country);

        Country DeleteCountry(int id);
    }
}
