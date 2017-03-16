namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Booking
    {
        public Booking()
        {
            this.Baggage = new HashSet<Baggage>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(ValidationConstants.CONFIRMATION_CODE_LENGTH)]
        public string ConfirmationCode { get; set; }

        [Required]
        public DateTime DateOfBooking { get; set; }

        [Required]
        [Range(ValidationConstants.MIN_ROW_NUMBER, ValidationConstants.MAX_ROW_NUMBER)]
        public int Row { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string SeatNumber { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public int TravelClassId { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int LegInstanceId { get; set; }

        [ForeignKey("LegInstanceId")]
        public virtual LegInstance LegInstance { get; set; }

        public virtual ICollection<Baggage> Baggage { get; set; }
    }
}
