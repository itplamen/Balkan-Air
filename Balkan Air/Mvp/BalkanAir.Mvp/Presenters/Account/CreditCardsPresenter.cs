namespace BalkanAir.Mvp.Presenters.Account
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using EventArgs.Account;
    using Services.Data.Contracts;
    using ViewContracts.Account;

    public class CreditCardsPresenter : Presenter<ICreditCardsView>
    {
        private readonly ICreditCardsServices creditCardsServices;

        public CreditCardsPresenter(ICreditCardsView view, ICreditCardsServices creditCardsServices) 
            : base(view)
        {
            if (creditCardsServices == null)
            {
                throw new ArgumentNullException(nameof(ICreditCardsServices));
            }

            this.creditCardsServices = creditCardsServices;

            this.View.OnCreditCardsGetData += this.View_OnCreditCardsGetData;
            this.View.OnCreditCardsDeleteItem += this.View_OnCreditCardsDeleteItem;
        }

        private void View_OnCreditCardsGetData(object sender, EventArgs e)
        {
            this.View.Model.CreditCards = this.creditCardsServices.GetAll()
                .Where(c => !c.IsDeleted);
        }

        private void View_OnCreditCardsDeleteItem(object sender, CreditCardsEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CreditCardsEventArgs));
            }

            this.creditCardsServices.Delete(e.Id);
        }
    }
}
