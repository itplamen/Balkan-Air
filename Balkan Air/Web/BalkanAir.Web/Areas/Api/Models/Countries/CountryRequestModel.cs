namespace BalkanAir.Web.Areas.Api.Models.Countries
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CountryRequestModel : IMapFrom<Country>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Abbreviation { get; set; }
    }
}