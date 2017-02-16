namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IFaresServices
    {
        int AddFare(Fare fare);

        IQueryable<Fare> GetAll();

        Fare GetFare(int id);

        Fare UpdateFare(int id, Fare fare);

        Fare DeleteFare(int id);
    }
}
