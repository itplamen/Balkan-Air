namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface ITravelClassesServices
    {
        TravelClass GetTravelClass(int id);

        IQueryable<TravelClass> GetAll();

        void BookSeat(int travelClassId, int row, string seatNumber);
    }
}
