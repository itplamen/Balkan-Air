namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IFlightsServices
    {
        int AddFlight(Flight flight);

        Flight GetFlight(int id);

        IQueryable<Flight> GetAll();

        Flight UpdateFlight(int id, Flight flight);

        Flight DeleteFlight(int id);
    }
}
