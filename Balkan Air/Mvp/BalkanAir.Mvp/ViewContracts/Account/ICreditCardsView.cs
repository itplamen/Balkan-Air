namespace BalkanAir.Mvp.ViewContracts.Account
{
    using System;

    using WebFormsMvp;

    using EventArgs.Account;
    using Models.Account;

    public interface ICreditCardsView : IView<CreditCardsViewModel>
    {
        event EventHandler OnCreditCardsGetData;

        event EventHandler<CreditCardsEventArgs> OnCreditCardsDeleteItem;
    }
}
