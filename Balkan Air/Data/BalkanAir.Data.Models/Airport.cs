namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Airport
    {
        public Airport()
        {
            this.FromFlights = new HashSet<Flight>();
            this.ToFlights = new HashSet<Flight>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(3)]
        public string Abbreviation { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [InverseProperty("FromAirport")]
        public virtual ICollection<Flight> FromFlights { get; set; }

        [InverseProperty("ToAirport")]
        public virtual ICollection<Flight> ToFlights { get; set; }
    }
}