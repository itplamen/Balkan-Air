namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
        [Range(1, 30)]
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

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        public virtual ICollection<Baggage> Baggages { get; set; }
    }
}
