namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class AirportsServices : IAirportsServices
    {
        private readonly IRepository<Airport> airports;

        public AirportsServices(IRepository<Airport> airports)
        {
            this.airports = airports;
        }

        public int AddAirport(Airport airport)
        {
            if (airport == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.airports.Add(airport);
            this.airports.SaveChanges();

            return airport.Id;
        }

        public Airport GetAirport(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.airports.GetById(id);
        }

        public IQueryable<Airport> GetAll()
        {
            return this.airports.All();
        }

        public Airport UpdateAirport(int id, Airport airport)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (airport == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var airportToUpdate = this.airports.GetById(id);

            if (airportToUpdate != null)
            {
                airportToUpdate.Name = airport.Name;
                airportToUpdate.Abbreviation = airport.Abbreviation;
                airportToUpdate.CountryId = airport.CountryId;
                airportToUpdate.IsDeleted = airport.IsDeleted;

                this.airports.SaveChanges();
            }

            return airportToUpdate;
        }

        public Airport DeleteAirport(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var airportToDelete = this.airports.GetById(id);

            if (airportToDelete != null)
            {
                airportToDelete.IsDeleted = true;
                this.airports.SaveChanges();
            }

            return airportToDelete;
        }
    }
}
