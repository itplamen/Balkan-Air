namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ICountriesServices
    {
        int AddCountry(Country country);

        Country GetCountry(int id);

        IQueryable<Country> GetAll();

        Country UpdateCountry(int id, Country country);

        Country DeleteCountry(int id);
    }
}
