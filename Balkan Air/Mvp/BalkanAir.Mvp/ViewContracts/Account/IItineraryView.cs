namespace BalkanAir.Mvp.ViewContracts.Account
{
    using System;

    using WebFormsMvp;

    using EventArgs.Account;
    using Models.Account;

    public interface IItineraryView : IView<ItineraryViewModel>
    {
        event EventHandler<ItineraryEventArgs> OnItinerariesGetItem;

        event EventHandler<ItineraryEventArgs> OnCabinBagsInfoShow;

        event EventHandler<ItineraryEventArgs> OnCheckedInBagsInfoShow;

        event EventHandler<ItineraryEventArgs> OnEquipmentBagsInfoShow;

        event EventHandler<ItineraryEventArgs> OnTravelClassInfoShow;
    }
}
