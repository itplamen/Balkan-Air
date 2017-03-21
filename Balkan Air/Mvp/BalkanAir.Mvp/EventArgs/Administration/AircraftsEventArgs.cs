namespace BalkanAir.Mvp.EventArgs.Administration
{
    public class AircraftsEventArgs
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int TotalSeats { get; set; }

        public int AircraftManufacturerId { get; set; }
    }
}
