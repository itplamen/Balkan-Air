namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Data.Services.Contracts;

    public partial class ManageBookings : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Booking> ManageBookingsGridView_GetData()
        {
            return this.BookingsServices.GetAll();
        }

        public void ManageBookingsGridView_UpdateItem(int id)
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

        public void ManageBookingsGridView_DeleteItem(int id)
        {
            this.BookingsServices.DeleteBooking(id);
        }
    }
}