namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Baggage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public BaggageType Type { get; set; }

        public int MaxKilograms { get; set; }

        public string Size { get; set; }

        [Required]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        //[ForeignKey("TravelClass")]
        //public int TravelClassId { get; set; }

        //public virtual TravelClass TravelClass { get; set; }

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}
