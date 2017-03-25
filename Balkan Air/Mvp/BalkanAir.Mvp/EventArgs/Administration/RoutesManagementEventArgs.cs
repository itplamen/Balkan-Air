namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class RoutesManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public double DistanceInKm { get; set; }
    }
}
