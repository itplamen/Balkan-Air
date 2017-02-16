namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Route
    {
        public Route()
        {
            this.Fares = new HashSet<Fare>();
            this.FlightLegs = new HashSet<FlightLeg>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Origin")]
        public int OriginId { get; set; }

        public virtual Airport Origin { get; set; }

        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        public virtual Airport Destination { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Invalid distance!")]
        public double DistanceInKm { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Fare> Fares { get; set; }

        public virtual ICollection<FlightLeg> FlightLegs { get; set; }
    }
}
