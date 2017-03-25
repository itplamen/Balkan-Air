namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class AircraftsManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int TotalSeats { get; set; }

        public int AircraftManufacturerId { get; set; }
    }
}
