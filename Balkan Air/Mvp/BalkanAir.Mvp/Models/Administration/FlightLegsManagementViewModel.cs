namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class FlightLegsManagementViewModel
    {
        public IQueryable<FlightLeg> FlightLegs { get; set; }

        public IQueryable<object> Airports { get; set; }

        public IQueryable<object> Flights { get; set; }

        public IQueryable<object> Routes { get; set; }

        public IQueryable<object> LegInstances { get; set; }

        public string AirportInfo { get; set; }
    }
}
