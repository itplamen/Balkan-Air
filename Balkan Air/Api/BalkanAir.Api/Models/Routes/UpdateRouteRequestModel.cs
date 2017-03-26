namespace BalkanAir.Api.Models.Routes
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateRouteRequestModel : IMapFrom<Route>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OriginId { get; set; }

        [Required]
        public int DestinationId { get; set; }

        [Required]
        public double DistanceInKm { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
