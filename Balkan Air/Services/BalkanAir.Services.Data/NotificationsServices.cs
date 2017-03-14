namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class NotificationsServices : INotificationsServices
    {
        private readonly IRepository<Notification> notifications;

        public NotificationsServices(IRepository<Notification> notifications)
        {
            this.notifications = notifications;
        }

        public int AddNotification(Notification notification)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.notifications.Add(notification);
            this.notifications.SaveChanges();

            return notification.Id;
        }

        public Notification GetNotification(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.notifications.GetById(id);
        }

        public IQueryable<Notification> GetAll()
        {
            return this.notifications.All();
        }

        public Notification UpdateNotification(int id, Notification notification)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (notification == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var notificationToUpdate = this.notifications.GetById(id);

            if (notificationToUpdate != null)
            {
                notificationToUpdate.Content = notification.Content;
                notificationToUpdate.DateCreated = notification.DateCreated;
                notificationToUpdate.Type = notification.Type;
                this.notifications.SaveChanges();
            }

            return notificationToUpdate;
        }

        public Notification DeleteNotification(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var notificationToDelete = this.notifications.GetById(id);

            if (notificationToDelete != null)
            {
                notificationToDelete.IsDeleted = true;
                this.notifications.SaveChanges();
            }

            return notificationToDelete;
        }
    }
}
