namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ITravelClassesServices
    {
        TravelClass GetTravelClass(int id);

        IQueryable<TravelClass> GetAll();

        void BookSeat(int travelClassId, int row, string seatNumber);
    }
}
