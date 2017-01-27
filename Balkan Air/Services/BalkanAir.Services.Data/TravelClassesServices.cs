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

        public IQueryable<TravelClass> GetAll()
        {
            return this.travelClasses.All();
        }

        public TravelClass GetTravelClass(int id)
        {
            return this.travelClasses.GetById(id);
        }

        public void BookSeat(int travelClassId, int row, string seatNumber)
        {
            Seat seat = this.travelClasses.GetById(travelClassId)
                .Seats
                .FirstOrDefault(s => s.Row == row && s.Number == seatNumber);

            if (seat != null)
            {
                seat.IsReserved = true;
                this.travelClasses.SaveChanges();
            }
        }
    }
}
