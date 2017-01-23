namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string NameOnCard { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Invalid expiration month!")]
        public int ExpirationMonth { get; set; }

        [Required]
        public int ExpirationYear { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string CvvNumber { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
