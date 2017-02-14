namespace BalkanAir.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Common;

    public class TravelClass
    {
        public TravelClass()
        {
            this.Seats = new HashSet<Seat>();
            this.ReservedSeat = true;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public TravelClassType Type { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Meal { get; set; }
 
        public bool PriorityBoarding { get; set; }

        public bool ReservedSeat { get; set; }

        public bool EarnMiles { get; set; }

        [Required]
        [Range(ValidationConstants.TRAVEL_CLASS_MIN_PRICE, 
            ValidationConstants.TRAVEL_CLASS_MAX_PRICE, 
            ErrorMessage = "Invalid travel class price!")]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

        [NotMapped]
        public int AvailableSeats
        {
            get
            {
                return this.Seats
                    .Where(s => !s.IsReserved)
                    .Count();
            }
        }
    }
}
