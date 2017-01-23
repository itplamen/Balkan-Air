namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AircraftManufacturer
    {
        public AircraftManufacturer()
        {
            this.Aircrafts = new HashSet<Aircraft>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Aircraft> Aircrafts { get; set; }
    }
}
