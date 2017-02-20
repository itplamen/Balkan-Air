namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using Common;

    public class TravelClass
    {
        public TravelClass()
        {
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
        [Range(
            ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_ROWS_FOR_EACH, 
            ValidationConstants.ECONOMY_CLASS_NUMBER_OF_ROWS, 
            ErrorMessage = "Invalid number of rows for travel class!")]
        public int NumberOfRows { get; set; }

        [Required]
        [Range(
            ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_SEATS_FOR_EACH,
            ValidationConstants.ECONOMY_CLASS_NUMBER_OF_SEATS,
            ErrorMessage = "Invalid number of seats for travel class!")]
        public int NumberOfSeats { get; set; }

        [Required]
        [Range(
            ValidationConstants.TRAVEL_CLASS_MIN_PRICE, 
            ValidationConstants.TRAVEL_CLASS_MAX_PRICE, 
            ErrorMessage = "Invalid travel class price!")]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [ForeignKey("AircraftId")]
        public virtual Aircraft Aircraft { get; set; }

        [NotMapped]
        public int NumberOfAvailableSeats
        {
            get
            {
                if (this.Aircraft == null)
                {
                    return -1;
                }

                var legInstance = this.Aircraft.LegInstances
                    .Where(l => l.AircraftId == this.AircraftId)
                    .FirstOrDefault();

                if (legInstance != null)
                {
                    return this.NumberOfSeats - legInstance.Seats
                        .Count(s => !s.IsDeleted && s.IsReserved && s.TravelClassId == this.Id);
                }

                return -1;
            }
        }
    }
}
