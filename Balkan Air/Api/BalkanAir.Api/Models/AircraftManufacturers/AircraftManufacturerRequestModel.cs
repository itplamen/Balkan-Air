namespace BalkanAir.Api.Models.AircraftManufacturers
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class AircraftManufacturerRequestModel : IMapFrom<AircraftManufacturer>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
