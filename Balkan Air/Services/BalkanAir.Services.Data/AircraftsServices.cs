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
        private readonly IRepository<Aircraft> aircraftsRepository;

        public AircraftsServices(IRepository<Aircraft> aircrafts)
        {
            this.aircraftsRepository = aircrafts;
        }

        public int AddAircraft(Aircraft aircraft)
        {
            if (aircraft == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.aircraftsRepository.Add(aircraft);
            this.aircraftsRepository.SaveChanges();

            return aircraft.Id;
        }

        public Aircraft GetAircraft(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.aircraftsRepository.GetById(id);
        }

        public IQueryable<Aircraft> GetAll()
        {
            return this.aircraftsRepository.All();
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

            var aircraftToUpdate = this.aircraftsRepository.GetById(id);

            if (aircraftsRepository != null)
            {
                aircraftToUpdate.Model = aircraft.Model;
                aircraftToUpdate.TotalSeats = aircraft.TotalSeats;
                aircraftToUpdate.AircraftManufacturerId = aircraft.AircraftManufacturerId;
                aircraftToUpdate.IsDeleted = aircraft.IsDeleted;

                this.aircraftsRepository.SaveChanges();
            }

            return aircraftToUpdate;
        }

        public Aircraft DeleteAircraft(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var aircraftToDelete = this.aircraftsRepository.GetById(id);

            if (aircraftToDelete != null)
            {
                aircraftToDelete.IsDeleted = true;
                this.aircraftsRepository.SaveChanges();
            }

            return aircraftToDelete;
        }
    }
}
         