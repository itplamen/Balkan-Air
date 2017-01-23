namespace BalkanAir.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class UserNotificationsServices : IUserNotificationsServices
    {
        private readonly IRepository<UserNotification> userNotifications;

        public UserNotificationsServices(IRepository<UserNotification> userNotifications)
        {
            this.userNotifications = userNotifications;
        }

        public void SendNotification(int notificationId, string userId)
        {
            this.AddUserNotification(notificationId, userId);
            this.userNotifications.SaveChanges();
        }

        public void SendNotification(int notificationId, IEnumerable<string> usersId)
        {
            foreach (var userId in usersId)
            {
                this.AddUserNotification(notificationId, userId);
            }

            this.userNotifications.SaveChanges();
        }

        public void SetNotificationAsRead(int notificationId, string userId)
        {
            // If notification was sent more than once.
            this.userNotifications.All()
                .Where(un => un.NotificationId == notificationId && un.UserId.Equals(userId))
                .ToList()
                .ForEach(un => { un.IsRead = true; un.DateRead = DateTime.Now; });

            this.userNotifications.SaveChanges();
        }

        public void SetAllNotificationsAsRead(string userId)
        {
            this.userNotifications.All()
                .Where(un => un.UserId.Equals(userId) && !un.IsRead)
                .ToList()
                .ForEach(un => { un.IsRead = true; un.DateRead = DateTime.Now; });

            this.userNotifications.SaveChanges();
        }

        public UserNotification GetUserNotification(int id)
        {
            return this.userNotifications.GetById(id);
        }

        public IQueryable<UserNotification> GetAll()
        {
            return this.userNotifications.All();
        }

        private void AddUserNotification(int notificationId, string userId)
        {
            this.userNotifications.Add(new UserNotification()
            {
                UserId = userId,
                NotificationId = notificationId,
                DateReceived = DateTime.Now
            });
        }
    }
}
