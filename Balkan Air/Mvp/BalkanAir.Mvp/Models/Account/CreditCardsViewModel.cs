namespace BalkanAir.Mvp.Models.Account
{
    using System.Linq;

    using Data.Models;

    public class CreditCardsViewModel
    {
        public IQueryable<CreditCard> CreditCards { get; set; }
    }
}
