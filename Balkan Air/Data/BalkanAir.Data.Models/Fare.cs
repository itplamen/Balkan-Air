namespace BalkanAir.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Fare
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Invalid price!")]
        public decimal Price { get; set; }

        [Required]
        public int RouteId { get; set; }

        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
    }
}
