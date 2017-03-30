namespace BalkanAir.Web.Account
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Account;
    using Mvp.Models.Account;
    using Mvp.Presenters.Account;
    using Mvp.ViewContracts.Account;

    [PresenterBinding(typeof(CreditCardsPresenter))]
    public partial class CreditCards : MvpPage<CreditCardsViewModel>, ICreditCardsView
    {
        public event EventHandler OnCreditCardsGetData;
        public event EventHandler<CreditCardsEventArgs> OnCreditCardsDeleteItem;

        public IQueryable<CreditCard> CreditCardsListView_GetData()
        {
            this.OnCreditCardsGetData?.Invoke(null, null);

            return this.Model.CreditCards;
        }

        public void CreditCardsListView_DeleteItem(int id)
        {
            this.OnCreditCardsDeleteItem?.Invoke(null, new CreditCardsEventArgs() { Id = id });
        }
    }
}