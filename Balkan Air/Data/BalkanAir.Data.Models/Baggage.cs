namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }
    }
}
