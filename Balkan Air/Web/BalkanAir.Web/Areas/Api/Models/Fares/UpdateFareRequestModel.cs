namespace BalkanAir.Web.Areas.Api.Models.Fares
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateFareRequestModel : IMapFrom<Fare>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int RouteId { get; set; }
    }
}