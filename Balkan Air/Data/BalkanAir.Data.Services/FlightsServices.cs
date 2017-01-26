namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;
    
    public class FlightsServices : IFlightsServices
    {
        private readonly IRepository<Flight> flights;

        public FlightsServices(IRepository<Flight> flights)
        {
            this.flights = flights;
        }

        public int AddFlight(Flight flight)
        {
            this.flights.Add(flight);
            this.flights.SaveChanges();

            return flight.Id;
        }

        public Flight GetFlight(int id)
        {
            return this.flights.GetById(id);
        }

        public IQueryable<Flight> GetAll()
        {
            return this.flights.All();
        }

        public Flight UpdateFlight(int id, Flight flight)
        {
            var flightToUpdate = this.flights.GetById(id);

            if (flightToUpdate != null)
            {
                flightToUpdate.Number = flight.Number;
                flightToUpdate.Departure = flight.Departure;
                flightToUpdate.Arrival = flight.Arrival;
                flightToUpdate.FlightStatusId = flight.FlightStatusId;
                flightToUpdate.AircraftId = flight.AircraftId;
                flightToUpdate.DepartureAirport.Id = flight.DepartureAirport.Id;
                flightToUpdate.ArrivalAirport.Id = flight.ArrivalAirport.Id;
                flightToUpdate.IsDeleted = flight.IsDeleted;
                this.flights.SaveChanges();
            }

            return flightToUpdate;
        }

        public Flight DeleteFlight(int id)
        {
            var flightToDelete = this.flights.GetById(id);

            if (flightToDelete != null)
            {
                flightToDelete.IsDeleted = true;
                this.flights.SaveChanges();
            }

            return flightToDelete;
        }
    }
}
