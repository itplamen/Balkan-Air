namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class BaggageServices : IBaggageServices
    {
        private readonly IRepository<Baggage> baggageRepository;

        public BaggageServices(IRepository<Baggage> baggage)
        {
            this.baggageRepository = baggage;
        }

        public int AddBaggage(Baggage baggage)
        {
            if (baggage == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.baggageRepository.Add(baggage);
            this.baggageRepository.SaveChanges();

            return baggage.Id;
        }

        public Baggage GetBaggage(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.baggageRepository.GetById(id);
        }

        public IQueryable<Baggage> GetAll()
        {
            return this.baggageRepository.All();
        }

        public Baggage UpdateBaggage(int id, Baggage baggage)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (baggage == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var baggageToUpdate = this.baggageRepository.GetById(id);

            if (baggageToUpdate != null)
            {
                baggageToUpdate.Type = baggage.Type;
                baggageToUpdate.MaxKilograms = baggage.MaxKilograms;
                baggageToUpdate.Size = baggage.Size;
                baggageToUpdate.Price = baggage.Price;
                baggageToUpdate.IsDeleted = baggage.IsDeleted;
                baggageToUpdate.BookingId = baggage.BookingId;

                this.baggageRepository.SaveChanges();
            }

            return baggageToUpdate;
        }

        public Baggage DeleteBaggage(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var baggageToDelete = this.baggageRepository.GetById(id);

            if (baggageToDelete != null)
            {
                baggageToDelete.IsDeleted = true;
                this.baggageRepository.SaveChanges();
            }

            return baggageToDelete;
        }
    }
}
