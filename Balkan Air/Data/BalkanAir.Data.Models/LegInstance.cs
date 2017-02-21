namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class LegInstance
    {
        public LegInstance()
        {
            this.Seats = new HashSet<Seat>();
            this.Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int FlightLegId { get; set; }

        [ForeignKey("FlightLegId")]
        public virtual FlightLeg FlightLeg { get; set; }

        [ForeignKey("FlightStatus")]
        public int FlightStatusId { get; set; }

        public virtual FlightStatus FlightStatus { get; set; }

        [ForeignKey("Aircraft")]
        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return this.ArrivalDateTime - this.DepartureDateTime;
            }
        }
    }
}
