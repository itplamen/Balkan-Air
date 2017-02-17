namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class FlightsServices : IFlightsServices
    {
        private readonly IRepository<Flight> flights;

        public FlightsServices(IRepository<Flight> flights)
        {
            this.flights = flights;
        }

        public int AddFlight(Flight flight)
        {
            if (flight == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.flights.Add(flight);
            this.flights.SaveChanges();

            return flight.Id;
        }

        public Flight GetFlight(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.flights.GetById(id);
        }

        public IQueryable<Flight> GetAll()
        {
            return this.flights.All();
        }

        public Flight UpdateFlight(int id, Flight flight)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (flight == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var flightToUpdate = this.flights.GetById(id);

            if (flightToUpdate != null)
            {
                flightToUpdate.Number = flight.Number;
                flightToUpdate.IsDeleted = flight.IsDeleted;

                this.flights.SaveChanges();
            }

            return flightToUpdate;
        }

        public Flight DeleteFlight(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
