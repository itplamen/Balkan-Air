namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IAircraftManufacturersServices
    {
        int AddManufacturer(AircraftManufacturer manufacturer);

        AircraftManufacturer GetManufacturer(int id);

        IQueryable<AircraftManufacturer> GetAll();

        AircraftManufacturer UpdateManufacturer(int id, AircraftManufacturer manufacturer);

        AircraftManufacturer DeleteManufacturer(int id);
    }
}
