namespace BalkanAir.Mvp.EventArgs.Administration
{
    public class RoutesManagementEventArgs
    {
        public int Id { get; set; }

        public int OriginId { get; set; }

        public int DestinationId { get; set; }

        public double DistanceInKm { get; set; }
    }
}
