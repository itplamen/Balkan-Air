namespace BalkanAir.Data.Services
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Services.Contracts;
    using Models;
    using Repositories.Contracts;

    public class CreditCardsServices : ICreditCardsServices
    {
        private IRepository<CreditCard> creditCards;

        public CreditCardsServices(IRepository<CreditCard> creditCards)
        {
            this.creditCards = creditCards;
        }

        public void Create(CreditCard creditCard)
        {
            this.creditCards.Add(creditCard);
            this.creditCards.SaveChanges();
        }

        public CreditCard Delete(int id)
        {
            var creditCard = this.creditCards.GetById(id);

            if (creditCards != null)
            {
                creditCard.IsDeleted = true;
                this.creditCards.SaveChanges();
            }

            return creditCard;
        }

        public IQueryable<CreditCard> GetAll()
        {
            return this.creditCards.All();
        }

        public CreditCard GetCreditCard(int id)
        {
            return this.creditCards.GetById(id);
        }
    }
}
