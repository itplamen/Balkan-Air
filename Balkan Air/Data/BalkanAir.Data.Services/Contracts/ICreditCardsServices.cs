namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ICreditCardsServices
    {
        void Create(CreditCard creditCard);

        CreditCard GetCreditCard(int id);

        IQueryable<CreditCard> GetAll();

        CreditCard Delete(int id);
    }
}
