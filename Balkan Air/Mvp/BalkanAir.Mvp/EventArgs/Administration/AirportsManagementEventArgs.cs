namespace BalkanAir.Mvp.EventArgs.Administration
{
    public class AirportsManagementEventArgs
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public int CountryId { get; set; }
    }
}
