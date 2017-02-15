namespace BalkanAir.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class UserNotificationsServices : IUserNotificationsServices
    {
        private readonly IRepository<UserNotification> userNotifications;

        public UserNotificationsServices(IRepository<UserNotification> userNotifications)
        {
            this.userNotifications = userNotifications;
        }

        public void SendNotification(int notificationId, string userId)
        {
            if (notificationId <= 0)
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_ID);
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_USER_ID);
            }

            this.AddUserNotification(notificationId, userId);
            this.userNotifications.SaveChanges();
        }

        public void SendNotification(int notificationId, IEnumerable<string> usersId)
        {
            if (notificationId <= 0)
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_ID);
            }

            if (usersId == null || !usersId.Any())
            {
                throw new ArgumentNullException(ErrorMessages.NULL_OR_EMPTY_LIST_OF_USERS);
            }

            foreach (var userId in usersId)
            {
                this.AddUserNotification(notificationId, userId);
            }

            this.userNotifications.SaveChanges();
        }

        public void SetNotificationAsRead(int notificationId, string userId)
        {
            if (notificationId <= 0)
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_ID);
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_USER_ID);
            }

            // If notification was sent more than once.
            this.userNotifications.All()
                .Where(un => un.NotificationId == notificationId && un.UserId.Equals(userId))
                .ToList()
                .ForEach(un =>
                {
                    un.IsRead = true;
                    un.DateRead = DateTime.Now;
                });

            this.userNotifications.SaveChanges();
        }

        public void SetAllNotificationsAsRead(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(ErrorMessages.INVALID_USER_ID);
            }

            this.userNotifications.All()
                .Where(un => un.UserId.Equals(userId) && !un.IsRead)
                .ToList()
                .ForEach(un =>
                {
                    un.IsRead = true;
                    un.DateRead = DateTime.Now;
                });

            this.userNotifications.SaveChanges();
        }

        public UserNotification GetUserNotification(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.userNotifications.GetById(id);
        }

        public IQueryable<UserNotification> GetAll()
        {
            return this.userNotifications.All();
        }

        public UserNotification UpdateUserNotification(int id, UserNotification userNotification)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (userNotification == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var userNotificationToUpdate = this.userNotifications.GetById(id);

            if (userNotificationToUpdate != null)
            {
                userNotificationToUpdate.DateReceived = userNotification.DateReceived;
                userNotificationToUpdate.IsRead = userNotification.IsRead;
                userNotificationToUpdate.DateRead = userNotification.DateRead;
                userNotificationToUpdate.UserId = userNotification.UserId;
                userNotificationToUpdate.NotificationId = userNotification.NotificationId;

                this.userNotifications.SaveChanges();
            }

            return userNotificationToUpdate;
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
