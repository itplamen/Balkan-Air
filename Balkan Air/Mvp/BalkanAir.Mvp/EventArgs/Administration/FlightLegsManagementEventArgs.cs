namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class FlightLegsManagementEventArgs
    {
        public int Id { get; set; }

        public int DepartureAirportId { get; set; }

        public DateTime ScheduledDepartureDateTime { get; set; }

        public int ArrivalAirportId { get; set; }

        public DateTime ScheduledArrivalDateTime { get; set; }

        public int FlightId { get; set; }

        public int RouteId { get; set; }

        public int AirportId { get; set; }
    }
}
