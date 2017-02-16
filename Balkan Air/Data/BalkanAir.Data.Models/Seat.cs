namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(2)]
        public string Number { get; set; }

        public bool IsReserved { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int LegInstanceId { get; set; }

        [ForeignKey("LegInstanceId")]
        public virtual LegInstance LegInstance { get; set; }
    }
}
