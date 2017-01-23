namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class AirportsServices : IAirportsServices
    {
        private readonly IRepository<Airport> airports;

        public AirportsServices(IRepository<Airport> airports)
        {
            this.airports = airports;
        }

        public int AddAirport(Airport airport)
        {
            this.airports.Add(airport);
            this.airports.SaveChanges();

            return airport.Id;
        }

        public Airport GetAirport(int id)
        {
            return this.airports.GetById(id);
        }

        public IQueryable<Airport> GetAll()
        {
            return this.airports.All();
        }

        public Airport UpdateAirport(int id, Airport airport)
        {
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
