namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class SeatsServices : ISeatsServices
    {
        private readonly IRepository<Seat> seats;

        public SeatsServices(IRepository<Seat> seats)
        {
            this.seats = seats;
        }

        public int AddSeat(Seat seat)
        {
            if (seat == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.seats.Add(seat);
            this.seats.SaveChanges();

            return seat.Id;
        }

        public Seat GetSeat(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.seats.GetById(id);
        }

        public IQueryable<Seat> GetAll()
        {
            return this.seats.All();
        }

        public Seat UpdateSeat(int id, Seat seat)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (seat == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var seatToUpdate = this.seats.GetById(id);

            if (seatToUpdate != null)
            {
                seatToUpdate.Row = seat.Row;
                seatToUpdate.Number = seat.Number;
                seatToUpdate.IsReserved = seat.IsReserved;
                seatToUpdate.TravelClassId = seat.TravelClassId;
                seatToUpdate.LegInstanceId = seat.LegInstanceId;
                seatToUpdate.IsDeleted = seat.IsDeleted;
 
                this.seats.SaveChanges();
            }

            return seatToUpdate;
        }

        public Seat DeleteSeat(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var seatToDelete = this.seats.GetById(id);

            if (seatToDelete != null)
            {
                seatToDelete.IsDeleted = true;
                this.seats.SaveChanges();
            }

            return seatToDelete;
        }
    }
}
