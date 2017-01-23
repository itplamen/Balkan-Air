namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IAirportsServices
    {
        int AddAirport(Airport airport);

        Airport GetAirport(int id);

        IQueryable<Airport> GetAll();

        Airport UpdateAirport(int id, Airport airport);

        Airport DeleteAirport(int id);
    }
}
