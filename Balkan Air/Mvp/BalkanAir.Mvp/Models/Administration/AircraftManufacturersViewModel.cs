namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class AircraftManufacturersViewModel
    {
        public IQueryable<AircraftManufacturer> AircraftManufacturers { get; set; }

        public IQueryable<object> Aircrafts { get; set; }
    }
}
