namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class LegInstancesServices : ILegInstancesServices
    {
        private readonly IRepository<LegInstance> legInstances;

        public LegInstancesServices(IRepository<LegInstance> legInstances)
        {
            this.legInstances = legInstances;
        }

        public int AddLegInstance(LegInstance legInstance)
        {
            if (legInstance == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.legInstances.Add(legInstance);
            this.legInstances.SaveChanges();

            return legInstance.Id;
        }

        public IQueryable<LegInstance> GetAll()
        {
            return this.legInstances.All();
        }

        public LegInstance GetLegInstance(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.legInstances.GetById(id);
        }

        public LegInstance UpdateLegInstance(int id, LegInstance legInstance)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (legInstance == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var legInstanceToUpdate = this.legInstances.GetById(id);

            if (legInstanceToUpdate != null)
            {
                legInstanceToUpdate.DateOfTravel = legInstance.DateOfTravel;
                legInstanceToUpdate.DepartureTime = legInstance.DepartureTime;
                legInstanceToUpdate.ArrivalTime = legInstance.ArrivalTime;
                legInstanceToUpdate.IsDeleted = legInstance.IsDeleted;
                legInstanceToUpdate.FlightLegId = legInstance.FlightLegId;
                legInstanceToUpdate.FlightStatusId = legInstance.FlightStatusId;
                legInstanceToUpdate.AircraftId = legInstance.AircraftId;

                this.legInstances.SaveChanges();
            }

            return legInstanceToUpdate;
        }

        public LegInstance DeleteLegInstance(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var legInstanceToDelete = this.legInstances.GetById(id);

            if (legInstanceToDelete != null)
            {
                legInstanceToDelete.IsDeleted = true;

                this.legInstances.SaveChanges();
            }

            return legInstanceToDelete;
        }
    }
}
