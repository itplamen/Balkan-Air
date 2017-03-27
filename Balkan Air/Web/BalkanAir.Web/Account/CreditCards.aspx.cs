namespace BalkanAir.Web.Account
{
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class CreditCards : Page
    {
        [Inject]
        public ICreditCardsServices CreditCardsServices { get; set; }

        public IQueryable<CreditCard> CreditCardsListView_GetData()
        {
            return this.CreditCardsServices.GetAll()
                .Where(c => !c.IsDeleted);
        }

        public void CreditCardsListView_DeleteItem(int id)
        {
            this.CreditCardsServices.Delete(id);
        }
    }
}