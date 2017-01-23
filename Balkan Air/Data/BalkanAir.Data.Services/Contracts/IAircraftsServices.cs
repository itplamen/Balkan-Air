namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface IAircraftsServices
    {
        int AddAircraft(Aircraft aircraft);

        Aircraft GetAircraft(int id);

        IQueryable<Aircraft> GetAll();

        Aircraft UpdateAircraft(int id, Aircraft aircraft);

        Aircraft DeleteAircraft(int id);
    }
}
