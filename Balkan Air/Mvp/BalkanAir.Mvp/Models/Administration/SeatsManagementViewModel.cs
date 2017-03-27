namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class SeatsManagementViewModel
    {
        public IQueryable<Seat> Seats { get; set; }

        public IQueryable<object> TravelClasses { get; set; }

        public IQueryable<object> LegInstances { get; set; }

        public string TravelClassInfo { get; set; }
    }
}
