namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ITravelClassesServices
    {
        int AddTravelClass(TravelClass travelClass);

        IQueryable<TravelClass> GetAll();

        TravelClass GetTravelClass(int id);

        TravelClass UpdateTravelClass(int id, TravelClass travelClass);

        TravelClass DeleteTravelClass(int id);

        void BookSeat(int travelClassId, int row, string seatNumber);
    }
}
