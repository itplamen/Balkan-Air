namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Flight
    {
        public Flight()
        {
            this.FlightLegs = new HashSet<FlightLeg>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(ValidationConstants.FLIGHT_NUMBER_LENGTH)]
        public string Number { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<FlightLeg> FlightLegs { get; set; }
    }
}
