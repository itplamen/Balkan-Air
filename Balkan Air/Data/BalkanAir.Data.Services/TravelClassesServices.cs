namespace BalkanAir.Data.Services
{
    using System.Linq;

    using BalkanAir.Data.Services.Contracts;
    using Models;
    using Repositories.Contracts;

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
