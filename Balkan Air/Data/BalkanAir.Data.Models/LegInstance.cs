namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LegInstance
    {
        public LegInstance()
        {
            this.Seats = new HashSet<Seat>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfTravel { get; set; }

        [Required]
        public TimeSpan DepartureTime { get; set; }

        [Required]
        public TimeSpan ArrivalTime { get; set; }

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

        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return this.ArrivalTime - this.DepartureTime;
            }
        }
    }
}
