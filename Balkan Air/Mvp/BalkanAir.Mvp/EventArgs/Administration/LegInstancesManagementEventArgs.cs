namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class LegInstancesManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public int FlightLegId { get; set; }

        public int FlightStatusId { get; set; }

        public int AircraftId { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public decimal Price { get; set; }

        public int FareId { get; set; }

        public int AirportId { get; set; }
    }
}
