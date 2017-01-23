namespace BalkanAir.Data.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    public interface IUserNotificationsServices
    {
        void SendNotification(int notificationId, string userId);

        void SendNotification(int notificationId, IEnumerable<string> usersId);

        void SetNotificationAsRead(int notificationId, string userId);

        void SetAllNotificationsAsRead(string userId);

        UserNotification GetUserNotification(int id);

        IQueryable<UserNotification> GetAll();
    }
}
