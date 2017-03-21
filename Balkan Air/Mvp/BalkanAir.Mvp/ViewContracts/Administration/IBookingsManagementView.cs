namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using Models.Administration;
    using EventArgs.Administration;

    public interface IBookingsManagementView : IView<BookingsManagementViewModel>
    {
        event EventHandler OnBookingsGetData;

        event EventHandler<BookingsManagementEventArgs> OnBookingsUpdateItem;

        event EventHandler<BookingsManagementEventArgs> OnBookingsDeleteItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
