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
            this.Baggages = new HashSet<Baggage>();
        }

        [Key]
        public int Id { get; set; }

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
        public int FlightId { get; set; }

        public virtual ICollection<Baggage> Baggages { get; set; }
    }
}
