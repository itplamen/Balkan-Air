namespace BalkanAir.Api.Models.Airports
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class AirportRequestModel : IMapFrom<Airport>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(3)]
        public string Abbreviation { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}