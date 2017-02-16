namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IRoutesServices
    {
        int AddRoute(Route route);

        IQueryable<Route> GetAll();

        Route GetRoute(int id);

        Route UpdateRoute(int id, Route route);

        Route DeleteRoute(int id);
    }
}
