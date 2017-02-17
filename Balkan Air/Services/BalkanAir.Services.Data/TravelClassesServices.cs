namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class TravelClassesServices : ITravelClassesServices
    {
        private IRepository<TravelClass> travelClasses;

        public TravelClassesServices(IRepository<TravelClass> travelClasses)
        {
            this.travelClasses = travelClasses;
        }

        public int AddTravelClass(TravelClass travelClass)
        {
            if (travelClass == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.travelClasses.Add(travelClass);
            this.travelClasses.SaveChanges();

            return travelClass.Id;
        }

        public IQueryable<TravelClass> GetAll()
        {
            return this.travelClasses.All();
        }

        public TravelClass GetTravelClass(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.travelClasses.GetById(id);
        }

        public TravelClass UpdateTravelClass(int id, TravelClass travelClass)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (travelClass == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var travelClassToUpdate = this.travelClasses.GetById(id);

            if (travelClassToUpdate != null)
            {
                travelClassToUpdate.Type = travelClass.Type;
                travelClassToUpdate.Meal = travelClass.Meal;
                travelClassToUpdate.PriorityBoarding = travelClass.PriorityBoarding;
                travelClassToUpdate.ReservedSeat = travelClass.ReservedSeat;
                travelClassToUpdate.EarnMiles = travelClass.EarnMiles;
                travelClassToUpdate.NumberOfRows = travelClass.NumberOfRows;
                travelClassToUpdate.NumberOfSeats = travelClass.NumberOfSeats;
                travelClassToUpdate.Price = travelClass.Price;
                travelClassToUpdate.AircraftId = travelClass.AircraftId;
                travelClassToUpdate.IsDeleted = travelClass.IsDeleted;

                this.travelClasses.SaveChanges();
            }

            return travelClassToUpdate;
        }

        public TravelClass DeleteTravelClass(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var travelClassToDelete = this.travelClasses.GetById(id);

            if (travelClassToDelete != null)
            {
                travelClassToDelete.IsDeleted = true;
                this.travelClasses.SaveChanges();
            }

            return travelClassToDelete;
        }
    }
}
