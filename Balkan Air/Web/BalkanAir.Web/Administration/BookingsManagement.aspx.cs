namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class BookingsManagement : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        public IQueryable<Booking> BookingsGridView_GetData()
        {
            return this.BookingsServices.GetAll();
        }

        public void BookingsGridView_UpdateItem(int id)
        {
            var booking = this.BookingsServices.GetBooking(id);

            if (booking == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(booking);
            if (ModelState.IsValid)
            {
                this.BookingsServices.UpdateBooking(id, booking);
            }
        }

        public void BookingsGridView_DeleteItem(int id)
        {
            this.BookingsServices.DeleteBooking(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}