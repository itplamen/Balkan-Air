namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FlightLeg
    {
        public FlightLeg()
        {
            this.LegInstances = new HashSet<LegInstance>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int DepartureAirportId  { get; set; }

        [Required]
        public TimeSpan ScheduledDepartureTime { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        [Required]
        public TimeSpan ScheduledArrivalTime { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        [Required]
        public int RouteId { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }

        public virtual ICollection<LegInstance> LegInstances { get; set; }
    }
}
