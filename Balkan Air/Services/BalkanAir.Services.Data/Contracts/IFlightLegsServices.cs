namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;
    
    public interface IFlightLegsServices
    {
        int AddFlightLeg(FlightLeg flightLeg);

        IQueryable<FlightLeg> GetAll();

        FlightLeg GetFlightLeg(int id);

        FlightLeg UpdateFlightLeg(int id, FlightLeg flightLeg);

        FlightLeg DeleteFlightLeg(int id);
    }
}
