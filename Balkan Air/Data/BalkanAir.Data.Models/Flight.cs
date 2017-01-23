namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Common;

    public class Flight
    {
        public Flight()
        {
            this.TravelClasses = new HashSet<TravelClass>();
            this.Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(GlobalConstants.FLIGHT_NUMBER_LENGTH)]
        public string Number { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("FlightStatus")]
        public int FlightStatusId { get; set; }

        public virtual FlightStatus FlightStatus { get; set; }

        [ForeignKey("Aircraft")]
        public int AircraftId { get; set; }

        public virtual Aircraft Aircraft { get; set; }

        public virtual Airport FromAirport { get; set; }

        public virtual Airport ToAirport { get; set; }

        public virtual ICollection<TravelClass> TravelClasses { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return this.Arrival - this.Departure;
            }
        }

        [NotMapped]
        public bool AreFromAndToAirportsValid
        {
            get
            {
                return this.FromAirport.Name != this.ToAirport.Name &&
                    this.FromAirport.Abbreviation != this.ToAirport.Abbreviation;
            }
        }

        [NotMapped]
        public decimal GetCheapestPriceFromAllTravelClasses
        {
            get
            {
                return this.TravelClasses
                    .FirstOrDefault(tr => tr.Type == TravelClassType.Economy)
                    .Price;
            }
        }
    }
}
