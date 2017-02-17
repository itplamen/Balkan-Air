namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class FlightStatusesServices : IFlightStatusesServices
    {
        private readonly IRepository<FlightStatus> flightStatuses;

        public FlightStatusesServices(IRepository<FlightStatus> flightStatuses)
        {
            this.flightStatuses = flightStatuses;
        }

        public int AddFlightStatus(FlightStatus flightStatus)
        {
            if (flightStatus == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.flightStatuses.Add(flightStatus);
            this.flightStatuses.SaveChanges();

            return flightStatus.Id;
        }

        public FlightStatus GetFlightStatus(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.flightStatuses.GetById(id);
        }

        public IQueryable<FlightStatus> GetAll()
        {
            return this.flightStatuses.All();
        }

        public FlightStatus UpdateFlightStatus(int id, FlightStatus flightStatus)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (flightStatus == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var flightStatusToUpdate = this.flightStatuses.GetById(id);

            if (flightStatusToUpdate != null)
            {
                flightStatusToUpdate.Name = flightStatus.Name;
                flightStatusToUpdate.IsDeleted = flightStatus.IsDeleted;
                this.flightStatuses.SaveChanges();
            }

            return flightStatusToUpdate;
        }

        public FlightStatus DeleteFlightStatus(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var flightStatusToDelete = this.flightStatuses.GetById(id);

            if (flightStatusToDelete != null)
            {
                flightStatusToDelete.IsDeleted = true;
                this.flightStatuses.SaveChanges();
            }

            return flightStatusToDelete;
        }
    }
}
