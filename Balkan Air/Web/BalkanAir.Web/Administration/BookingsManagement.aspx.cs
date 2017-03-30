namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(BookingsManagementPresenter))]
    public partial class BookingsManagement : MvpPage<BookingsManagementViewModel>, IBookingsManagementView
    {
        public event EventHandler OnBookingsGetData;

        public event EventHandler<BookingsManagementEventArgs> OnBookingsUpdateItem;

        public event EventHandler<BookingsManagementEventArgs> OnBookingsDeleteItem;

        public IQueryable<Booking> BookingsGridView_GetData()
        {
            this.OnBookingsGetData?.Invoke(null, null);

            return this.Model.Bookings;
        }

        public void BookingsGridView_UpdateItem(int id)
        {
            this.OnBookingsUpdateItem?.Invoke(null, new BookingsManagementEventArgs() { Id = id });
        }

        public void BookingsGridView_DeleteItem(int id)
        {
            this.OnBookingsDeleteItem?.Invoke(null, new BookingsManagementEventArgs() { Id = id });
        }
    }
}