namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ILegInstancesServices
    {
        int AddLegInstance(LegInstance legInstance);

        IQueryable<LegInstance> GetAll();

        LegInstance GetLegInstance(int id);

        LegInstance UpdateLegInstance(int id, LegInstance legInstance);

        LegInstance DeleteLegInstance(int id);
    }
}
