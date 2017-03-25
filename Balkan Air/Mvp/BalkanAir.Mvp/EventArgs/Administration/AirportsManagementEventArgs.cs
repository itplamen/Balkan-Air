namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    public class AirportsManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public int CountryId { get; set; }
    }
}
