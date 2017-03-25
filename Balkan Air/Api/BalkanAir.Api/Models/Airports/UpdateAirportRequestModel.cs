namespace BalkanAir.Api.Models.Airports
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateAirportRequestModel : IMapFrom<Airport>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(3)]
        public string Abbreviation { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}