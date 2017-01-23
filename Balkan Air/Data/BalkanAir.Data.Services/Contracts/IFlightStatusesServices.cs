namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IFlightStatusesServices
    {
        int AddFlightStatus(FlightStatus flightStatus);

        FlightStatus GetFlightStatus(int id);

        IQueryable<FlightStatus> GetAll();

        FlightStatus UpdateFlightStatus(int id, FlightStatus flightStatus);

        FlightStatus DeleteFlightStatus(int id);
    }
}
