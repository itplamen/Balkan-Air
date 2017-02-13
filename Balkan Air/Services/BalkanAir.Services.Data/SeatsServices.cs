namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
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
            this.seats.Add(seat);
            this.seats.SaveChanges();

            return seat.Id;
        }

        public void AddSeatsToTravelClass(TravelClass travelClass)
        {
            int numberOfRows = 0;

            if (travelClass.Type == TravelClassType.First || travelClass.Type == TravelClassType.Business)
            {
                numberOfRows = 2;
            }
            else if (travelClass.Type == TravelClassType.Economy)
            {
                numberOfRows = 26;
            }
            else
            {
                throw new IndexOutOfRangeException("Invalid travel class type!");
            }

            for (int row = 1; row <= numberOfRows; row++)
            {
                this.seats.Add(new Seat() { Number = "A", Row = row, TravelClassId = travelClass.Id });
                this.seats.Add(new Seat() { Number = "B", Row = row, TravelClassId = travelClass.Id });
                this.seats.Add(new Seat() { Number = "C", Row = row, TravelClassId = travelClass.Id });
                this.seats.Add(new Seat() { Number = "D", Row = row, TravelClassId = travelClass.Id });
                this.seats.Add(new Seat() { Number = "E", Row = row, TravelClassId = travelClass.Id });
                this.seats.Add(new Seat() { Number = "F", Row = row, TravelClassId = travelClass.Id });
            }

            this.seats.SaveChanges();
        }

        public Seat GetSeat(int id)
        {
            return this.seats.GetById(id);
        }

        public IQueryable<Seat> GetAll()
        {
            return this.seats.All();
        }

        public Seat UpdateSeat(int id, Seat seat)
        {
            var seatToUpdate = this.seats.GetById(id);

            if (seatToUpdate != null)
            {
                seatToUpdate.Number = seat.Number;
                seatToUpdate.Row = seat.Row;
                seatToUpdate.IsReserved = seat.IsReserved;
                seatToUpdate.IsDeleted = seat.IsDeleted;
                seatToUpdate.TravelClassId = seat.TravelClassId;

                this.seats.SaveChanges();
            }

            return seatToUpdate;
        }

        public Seat DeleteSeat(int id)
        {
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
