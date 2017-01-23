namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class NotificationsServices : INotificationsServices
    {
        private readonly IRepository<Notification> notifications;

        public NotificationsServices(IRepository<Notification> notifications)
        {
            this.notifications = notifications;
        }

        public int AddNotification(Notification notification)
        {
            this.notifications.Add(notification);
            this.notifications.SaveChanges();

            return notification.Id;
        }

        public Notification GetNotification(int id)
        {
            return this.notifications.GetById(id);
        }

        public IQueryable<Notification> GetAll()
        {
            return this.notifications.All();
        }

        public Notification UpdateNotification(int id, Notification notification)
        {
            var notificationToUpdate = this.notifications.GetById(id);

            if (notificationToUpdate != null)
            {
                notificationToUpdate.Content = notification.Content;
                notificationToUpdate.DateCreated = notification.DateCreated;
                notificationToUpdate.Url = notification.Url;
                this.notifications.SaveChanges();
            }

            return notificationToUpdate;
        }

        public Notification DeleteNotification(int id)
        {
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
