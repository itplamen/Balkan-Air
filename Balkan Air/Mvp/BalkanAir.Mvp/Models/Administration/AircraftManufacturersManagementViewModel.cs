namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class AircraftManufacturersManagementViewModel
    {
        public IQueryable<AircraftManufacturer> AircraftManufacturers { get; set; }

        public IQueryable<object> Aircrafts { get; set; }
    }
}
