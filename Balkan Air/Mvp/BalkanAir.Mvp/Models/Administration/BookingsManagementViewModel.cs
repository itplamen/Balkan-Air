namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class BookingsManagementViewModel
    {
        public IQueryable<Booking> Bookings { get; set; }
    }
}
