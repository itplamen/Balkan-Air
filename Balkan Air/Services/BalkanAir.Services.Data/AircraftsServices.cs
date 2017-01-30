namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
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
            if (aircraft == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.aircrafts.Add(aircraft);
            this.aircrafts.SaveChanges();

            return aircraft.Id;
        }

        public Aircraft GetAircraft(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.aircrafts.GetById(id);
        }

        public IQueryable<Aircraft> GetAll()
        {
            return this.aircrafts.All();
        }

        public Aircraft UpdateAircraft(int id, Aircraft aircraft)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (aircraft == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
