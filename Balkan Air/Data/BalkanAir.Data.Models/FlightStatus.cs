namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FlightStatus
    {
        public FlightStatus()
        {
            this.LegInstances = new HashSet<LegInstance>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<LegInstance> LegInstances { get; set; }
    }
}
