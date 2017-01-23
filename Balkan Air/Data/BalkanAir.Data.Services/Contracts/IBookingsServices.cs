namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IBookingsServices
    {
        int AddBooking(Booking booking);

        Booking GetBooking(int id);

        IQueryable<Booking> GetAll();

        Booking UpdateBooking(int id, Booking booking);

        Booking DeleteBooking(int id);
    }
}
