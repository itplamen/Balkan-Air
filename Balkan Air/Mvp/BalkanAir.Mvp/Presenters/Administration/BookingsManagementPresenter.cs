namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;

    using WebFormsMvp;

    using Common;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class BookingsManagementPresenter : Presenter<IBookingsManagementView>
    {
        private readonly IBookingsServices bookingsServices;

        public BookingsManagementPresenter(IBookingsManagementView view, IBookingsServices bookingsServices) 
            : base(view)
        {
            if (bookingsServices == null)
            {
                throw new ArgumentNullException(nameof(IBookingsServices));
            }

            this.bookingsServices = bookingsServices;

            this.View.OnBookingsGetData += this.View_OnBookingsGetData;
            this.View.OnBookingsUpdateItem += this.View_OnBookingsUpdateItem;
            this.View.OnBookingsDeleteItem += this.View_OnBookingsDeleteItem;
        }

        private void View_OnBookingsGetData(object sender, EventArgs e)
        {
            this.View.Model.Bookings = this.bookingsServices.GetAll();
        }

        private void View_OnBookingsUpdateItem(object sender, BookingsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(BookingsManagementEventArgs));
            }

            var booking = this.bookingsServices.GetBooking(e.Id);

            if (booking == null)
            {
                this.View.ModelState.AddModelError(
                    ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));

                return;
            }

            this.View.TryUpdateModel(booking);

            if (this.View.ModelState.IsValid)
            {
                this.bookingsServices.UpdateBooking(e.Id, booking);
            }
        }

        private void View_OnBookingsDeleteItem(object sender, BookingsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(BookingsManagementEventArgs));
            }

            this.bookingsServices.DeleteBooking(e.Id);
        }
    }
}
