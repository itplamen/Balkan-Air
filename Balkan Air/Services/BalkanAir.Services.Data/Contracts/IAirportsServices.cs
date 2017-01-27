namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IAirportsServices
    {
        int AddAirport(Airport airport);

        Airport GetAirport(int id);

        IQueryable<Airport> GetAll();

        Airport UpdateAirport(int id, Airport airport);

        Airport DeleteAirport(int id);
    }
}
