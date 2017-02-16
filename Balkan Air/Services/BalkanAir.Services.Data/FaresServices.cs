namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class FaresServices : IFaresServices
    {
        private readonly IRepository<Fare> fares;

        public FaresServices(IRepository<Fare> fares)
        {
            this.fares = fares;
        }

        public int AddFare(Fare fare)
        {
            if (fare == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.fares.Add(fare);
            this.fares.SaveChanges();

            return fare.Id;
        }

        public IQueryable<Fare> GetAll()
        {
            return this.fares.All();
        }

        public Fare GetFare(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.fares.GetById(id);
        }

        public Fare UpdateFare(int id, Fare fare)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (fare == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var fareToUpdate = this.fares.GetById(id);

            if (fareToUpdate != null)
            {
                fareToUpdate.Price = fare.Price;
                fareToUpdate.RouteId = fare.RouteId;
                fareToUpdate.IsDeleted = fare.IsDeleted;

                this.fares.SaveChanges();
            }

            return fareToUpdate;
        }

        public Fare DeleteFare(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var fareToDelete = this.fares.GetById(id);

            if (fareToDelete != null)
            {
                fareToDelete.IsDeleted = true;

                this.fares.SaveChanges();
            }

            return fareToDelete;
        }
    }
}
