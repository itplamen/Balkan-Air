namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IAircraftManufacturersServices
    {
        int AddManufacturer(AircraftManufacturer manufacturer);

        AircraftManufacturer GetManufacturer(int id);

        IQueryable<AircraftManufacturer> GetAll();

        AircraftManufacturer UpdateManufacturer(int id, AircraftManufacturer manufacturer);

        AircraftManufacturer DeleteManufacturer(int id);
    }
}
