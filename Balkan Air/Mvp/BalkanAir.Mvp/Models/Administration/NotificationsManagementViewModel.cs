namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class NotificationsManagementViewModel
    {
        public IQueryable<Notification> Notifications { get; set; }
    }
}
