namespace BalkanAir.Web.Areas.Api.Models
{
    using System;
    using System.Linq.Expressions;
    using System.ComponentModel.DataAnnotations;

    using BalkanAir.Data.Models;

    public class AirportResponseModel
    {
        public static Expression<Func<Airport, AirportResponseModel>> FromAirport
        {
            get
            {
                return airport => new AirportResponseModel
                {
                    Id = airport.Id,
                    Name = airport.Name,
                    Abbreviation = airport.Abbreviation,
                    IsDeleted = airport.IsDeleted,
                    CountryId = airport.CountryId
                };
            }
        }

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

        public bool IsDeleted { get; set; }

        public int CountryId { get; set; }
    }
}