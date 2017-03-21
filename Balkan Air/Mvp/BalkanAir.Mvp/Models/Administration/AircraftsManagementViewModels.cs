namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class AircraftsManagementViewModels
    {
        public IQueryable<Aircraft> Aircrafts { get; set; }

        public IQueryable<AircraftManufacturer> AircraftManufacturer { get; set; }
    }
}
