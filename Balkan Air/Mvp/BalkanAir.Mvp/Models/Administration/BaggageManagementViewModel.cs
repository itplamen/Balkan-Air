namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class BaggageManagementViewModel
    {
        public IQueryable<Baggage> Baggage { get; set; }

        public IQueryable<object> Bookings { get; set; }
    }
}
