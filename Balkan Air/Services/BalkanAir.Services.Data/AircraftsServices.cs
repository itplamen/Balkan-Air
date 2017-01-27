namespace BalkanAir.Services.Data
{
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class AircraftsServices : IAircraftsServices
    {
        private readonly IRepository<Aircraft> aircrafts;

        public AircraftsServices(IRepository<Aircraft> aircrafts)
        {
            this.aircrafts = aircrafts;
        }

        public int AddAircraft(Aircraft aircraft)
        {
            this.aircrafts.Add(aircraft);
            this.aircrafts.SaveChanges();

            return aircraft.Id;
        }

        public Aircraft GetAircraft(int id)
        {
            return this.aircrafts.GetById(id);
        }

        public IQueryable<Aircraft> GetAll()
        {
            return this.aircrafts.All();
        }

        public Aircraft UpdateAircraft(int id, Aircraft aircraft)
        {
            var aircraftToUpdate = this.aircrafts.GetById(id);

            if (aircrafts != null)
            {
                aircraftToUpdate.Model = aircraft.Model;
                aircraftToUpdate.TotalSeats = aircraft.TotalSeats;
                aircraftToUpdate.AircraftManufacturerId = aircraft.AircraftManufacturerId;
                aircraftToUpdate.IsDeleted = aircraft.IsDeleted;
                this.aircrafts.SaveChanges();
            }

            return aircraftToUpdate;
        }

        public Aircraft DeleteAircraft(int id)
        {
            var aircraftToDelete = this.aircrafts.GetById(id);

            if (aircraftToDelete != null)
            {
                aircraftToDelete.IsDeleted = true;
                this.aircrafts.SaveChanges();
            }

            return aircraftToDelete;
        }
    }
}
