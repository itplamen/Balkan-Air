namespace BalkanAir.Web.Areas.Api.Models.FlightNumbers
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using BalkanAir.Common;
    using Data.Models;
    using Infrastructure.Mapping;
    
    public class UpdateFlightNumberRequestModel : IMapFrom<Flight>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(ValidationConstants.FLIGHT_NUMBER_LENGTH)]
        public string Number { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}