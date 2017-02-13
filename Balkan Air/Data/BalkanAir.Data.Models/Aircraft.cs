namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;
    
    public class Aircraft
    {
        public Aircraft()
        {
            this.Flights = new HashSet<Flight>();
            this.TotalSeats = ValidationConstants.AIRCRAFT_MAX_SEATS;  // By Default
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        [Range(
            ValidationConstants.AIRCRAFT_MIN_SEATS,
            ValidationConstants.AIRCRAFT_MAX_SEATS, 
            ErrorMessage = "Invalid number of seats!")]
        public int TotalSeats { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int AircraftManufacturerId { get; set; }

        [ForeignKey("AircraftManufacturerId")]
        public virtual AircraftManufacturer AircraftManufacturer { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
