namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Airport
    {
        public Airport()
        {
            this.Origins = new HashSet<Route>();
            this.Destinations = new HashSet<Route>();
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
        [Index(IsUnique = true)]
        public string Abbreviation { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [InverseProperty("Origin")]
        public virtual ICollection<Route> Origins { get; set; }

        [InverseProperty("Destination")]
        public virtual ICollection<Route> Destinations { get; set; }
    }
}