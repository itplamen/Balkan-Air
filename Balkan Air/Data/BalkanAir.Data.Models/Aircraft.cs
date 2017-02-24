namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Common;
    
    public class Aircraft
    {
        public Aircraft()
        {
            this.TravelClasses = new HashSet<TravelClass>();
            this.LegInstances = new HashSet<LegInstance>();
            this.TotalSeats = ValidationConstants.AIRCRAFT_MAX_SEATS;  // By Default
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int AircraftManufacturerId { get; set; }

        [ForeignKey("AircraftManufacturerId")]
        public virtual AircraftManufacturer AircraftManufacturer { get; set; }

        public virtual ICollection<TravelClass> TravelClasses { get; set; }

        public virtual ICollection<LegInstance> LegInstances { get; set; }

        [NotMapped]
        public decimal GetCheapestTravelClassPrice
        {
            get
            {
                if (this.TravelClasses.Count == 0)
                {
                    return 0;
                }

                return this.TravelClasses
                    .Min(t => t.Price);
            }
        }
    }
}
