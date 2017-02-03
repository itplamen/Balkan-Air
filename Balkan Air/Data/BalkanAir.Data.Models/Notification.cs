namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public Notification()
        {
            this.UserNotification = new HashSet<UserNotification>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<UserNotification> UserNotification { get; set; }
    }
}
