namespace BalkanAir.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserNotification
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateReceived { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        [Required]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Required]
        [Column(Order = 1)]
        public int NotificationId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}
