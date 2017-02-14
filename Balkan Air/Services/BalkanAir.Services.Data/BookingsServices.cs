namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class BookingsServices : IBookingsServices
    {
        private readonly IRepository<Booking> bookings;

        public BookingsServices(IRepository<Booking> bookings)
        {
            this.bookings = bookings;
        }

        public int AddBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.bookings.Add(booking);
            this.bookings.SaveChanges();

            return booking.Id;
        }

        public Booking GetBooking(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.bookings.GetById(id);
        }

        public IQueryable<Booking> GetAll()
        {
            return this.bookings.All();
        }

        public Booking UpdateBooking(int id, Booking booking)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (booking == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
