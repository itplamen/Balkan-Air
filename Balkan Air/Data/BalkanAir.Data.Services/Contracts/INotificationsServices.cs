namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface INotificationsServices
    {
        int AddNotification(Notification notification);

        Notification GetNotification(int id);

        IQueryable<Notification> GetAll();

        Notification UpdateNotification(int id, Notification notification);

        Notification DeleteNotification(int id);
    }
}
