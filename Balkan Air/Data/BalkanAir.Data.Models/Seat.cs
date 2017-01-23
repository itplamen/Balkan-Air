namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1)]
        public string Number { get; set; }

        [Required]
        [Range(1, 30)]
        public int Row { get; set; }

        public bool IsReserved { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("TravelClass")]
        public int TravelClassId { get; set; }

        public virtual TravelClass TravelClass { get; set; }
    }
}
