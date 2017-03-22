namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    using Data.Models;
    
    public class NotificationsManagementEventArgs
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public NotificationType Type { get; set; }
    }
}
