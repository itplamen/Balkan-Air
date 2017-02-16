namespace BalkanAir.Services.Data
{
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
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
            return this.travelClasses.GetById(id);
        }

        public TravelClass UpdateTravelClass(int id, TravelClass travelClass)
        {
            var travelClassToUpdate = this.travelClasses.GetById(id);

            if (travelClassToUpdate != null)
            {
                travelClassToUpdate.Type = travelClass.Type;
                travelClassToUpdate.Meal = travelClass.Meal;
                travelClassToUpdate.PriorityBoarding = travelClass.PriorityBoarding;
                travelClassToUpdate.ReservedSeat = travelClass.ReservedSeat;
                travelClassToUpdate.EarnMiles = travelClass.EarnMiles;
                travelClassToUpdate.Price = travelClass.Price;
 
                travelClassToUpdate.IsDeleted = travelClass.IsDeleted;

                this.travelClasses.SaveChanges();
            }

            return travelClassToUpdate;
        }

        public TravelClass DeleteTravelClass(int id)
        {
            var travelClassToDelete = this.travelClasses.GetById(id);

            if (travelClassToDelete != null)
            {
                travelClassToDelete.IsDeleted = true;
                this.travelClasses.SaveChanges();
            }

            return travelClassToDelete;
        }

        public void BookSeat(int travelClassId, int row, string seatNumber)
        {
            //Seat seat = this.travelClasses.GetById(travelClassId)
            //    .Seats
            //    .FirstOrDefault(s => s.Row == row && s.Number == seatNumber);

            //if (seat != null)
            //{
            //    seat.IsReserved = true;
            //    this.travelClasses.SaveChanges();
            //}
        }
    }
}
