namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface IBaggageServices
    {
        int AddBaggage(Baggage baggage);

        Baggage GetBaggage(int id);

        IQueryable<Baggage> GetAll();

        Baggage UpdateBaggage(int id, Baggage baggage);

        Baggage DeleteBaggage(int id);
    }
}
