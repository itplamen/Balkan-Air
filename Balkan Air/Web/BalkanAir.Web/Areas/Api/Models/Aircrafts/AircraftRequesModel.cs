namespace BalkanAir.Web.Areas.Api.Models.Aircrafts
{
    using System.ComponentModel.DataAnnotations;

    using Data.Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AircraftRequesModel : IMapFrom<Aircraft>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        [Range(ValidationConstants.AIRCRAFT_MIN_SEATS, ValidationConstants.AIRCRAFT_MAX_SEATS, 
            ErrorMessage = "Invalid number of seats!")]
        public int TotalSeats { get; set; }

        [Required]
        public int AircraftManufacturerId { get; set; }
    }
}