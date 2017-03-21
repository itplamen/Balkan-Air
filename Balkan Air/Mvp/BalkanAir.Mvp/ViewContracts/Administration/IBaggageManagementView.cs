namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IBaggageManagementView : IView<BaggageManagementViewModel>
    {
        event EventHandler OnBaggageGetData;

        event EventHandler<BaggageManagementEventArgs> OnBaggageUpdateItem;

        event EventHandler<BaggageManagementEventArgs> OnBaggageDeleteItem;

        event EventHandler<BaggageManagementEventArgs> OnBaggageAddItem;

        event EventHandler OnBookingsGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
