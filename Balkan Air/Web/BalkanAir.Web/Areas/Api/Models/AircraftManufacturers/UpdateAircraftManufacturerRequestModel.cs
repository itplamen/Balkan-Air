namespace BalkanAir.Web.Areas.Api.Models.AircraftManufacturers
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;
   
    public class UpdateAircraftManufacturerRequestModel : IMapFrom<AircraftManufacturer>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}