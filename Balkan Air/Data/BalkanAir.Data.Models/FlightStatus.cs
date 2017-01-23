namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FlightStatus
    {
        public FlightStatus()
        {
            this.Flights = new HashSet<Flight>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
