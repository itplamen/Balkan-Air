namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class FlightsManagementViewModel
    {
        public IQueryable<Flight> Flights { get; set; }
    }
}
