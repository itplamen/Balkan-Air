namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class BookingsServices : IBookingsServices
    {
        private readonly IRepository<Booking> bookings;

        public BookingsServices(IRepository<Booking> bookings)
        {
            this.bookings = bookings;
        }

        public int AddBooking(Booking booking)
        {
            this.bookings.Add(booking);
            this.bookings.SaveChanges();

            return booking.Id;
        }

        public Booking GetBooking(int id)
        {
            return this.bookings.GetById(id);
        }

        public IQueryable<Booking> GetAll()
        {
            return this.bookings.All();
        }

        public Booking UpdateBooking(int id, Booking booking)
        {
            var bookingToUpdate = this.bookings.GetById(id);

            if (bookingToUpdate != null)
            {
                bookingToUpdate.DateOfBooking = booking.DateOfBooking;
                bookingToUpdate.Row = booking.Row;
                bookingToUpdate.SeatNumber = booking.SeatNumber;
                bookingToUpdate.TotalPrice = booking.TotalPrice;
                bookingToUpdate.TravelClassId = booking.TravelClassId;
                bookingToUpdate.UserId = booking.UserId;
                bookingToUpdate.FlightId = booking.FlightId;
                bookingToUpdate.IsDeleted = booking.IsDeleted;
                this.bookings.SaveChanges();
            }

            return bookingToUpdate;
        }

        public Booking DeleteBooking(int id)
        {
            var bookingToDelete = this.bookings.GetById(id);

            if (bookingToDelete != null)
            {
                bookingToDelete.IsDeleted = true;
                this.bookings.SaveChanges();
            }

            return bookingToDelete;
        }
    }
}
