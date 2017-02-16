﻿namespace BalkanAir.Services.Data
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

        public void AddSeatsToTravelClass(TravelClass travelClass)
        {
            if (travelClass == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
                 
            }

            this.seats.SaveChanges();
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
                seatToUpdate.Number = seat.Number;
                 
                seatToUpdate.IsReserved = seat.IsReserved;
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
