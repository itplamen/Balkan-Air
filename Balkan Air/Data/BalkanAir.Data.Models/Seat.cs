namespace BalkanAir.Data.Models
{
    using Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(
            ValidationConstants.MIN_ROW_NUMBER, 
            ValidationConstants.MAX_ROW_NUMBER, 
            ErrorMessage = "Invalid row number!")]
        public int Row { get; set; }

        [Required]
        [MaxLength(1)]
        public string Number { get; set; }

        [Required]
        public int TravelClassId { get; set; }

        public bool IsReserved { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int LegInstanceId { get; set; }

        [ForeignKey("LegInstanceId")]
        public virtual LegInstance LegInstance { get; set; }
    }
}
