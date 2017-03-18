namespace BalkanAir.Web.Areas.Api.Models.Fares
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class FareRequestModel : IMapFrom<Fare>
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int RouteId { get; set; }
    }
}