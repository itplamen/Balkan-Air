namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ISeatsServices
    {
        int AddSeat(Seat seat);

        Seat GetSeat(int id);

        IQueryable<Seat> GetAll();

        Seat UpdateSeat(int id, Seat seat);

        Seat DeleteSeat(int id);
    }
}
