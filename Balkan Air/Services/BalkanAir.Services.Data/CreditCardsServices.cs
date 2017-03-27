namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class CreditCardsServices : ICreditCardsServices
    {
        private IRepository<CreditCard> creditCards;

        public CreditCardsServices(IRepository<CreditCard> creditCards)
        {
            this.creditCards = creditCards;
        }

        public void Create(CreditCard creditCard)
        {
            if (creditCard == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.creditCards.Add(creditCard);
            this.creditCards.SaveChanges();
        }
       
        public IQueryable<CreditCard> GetAll()
        {
            return this.creditCards.All();
        }

        public CreditCard GetCreditCard(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.creditCards.GetById(id);
        }

        public CreditCard Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var creditCard = this.creditCards.GetById(id);

            if (creditCard != null)
            {
                creditCard.IsDeleted = true;
                this.creditCards.SaveChanges();
            }

            return creditCard;
        }
    }
}
