namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class LegInstancesManagementViewModel
    {
        public IQueryable<LegInstance> LegInstances { get; set; }

        public IQueryable<object> FlightLegs { get; set; }

        public IQueryable<FlightStatus> FlightStatuses { get; set; }

        public IQueryable<object> Aircrafts { get; set; }

        public IQueryable<object> Fares { get; set; }

        public string AirportInfo { get; set; }
    }
}
