namespace BalkanAir.Api.Models.TravelClasses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateTravelClassRequestModel : IMapFrom<TravelClass>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Meal { get; set; }

        public bool PriorityBoarding { get; set; }

        public bool ReservedSeat { get; set; }

        public bool EarnMiles { get; set; }

        [Required]
        [Range(
            ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_ROWS_FOR_EACH,
            ValidationConstants.ECONOMY_CLASS_NUMBER_OF_ROWS,
            ErrorMessage = "Invalid number of rows for travel class!")]
        public int NumberOfRows { get; set; }

        [Required]
        [Range(
            ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_SEATS_FOR_EACH,
            ValidationConstants.ECONOMY_CLASS_NUMBER_OF_SEATS,
            ErrorMessage = "Invalid number of seats for travel class!")]
        public int NumberOfSeats { get; set; }

        [Required]
        [Range(
            ValidationConstants.TRAVEL_CLASS_MIN_PRICE,
            ValidationConstants.TRAVEL_CLASS_MAX_PRICE,
            ErrorMessage = "Invalid travel class price!")]
        public decimal Price { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<UpdateTravelClassRequestModel, TravelClass>()
                .ForMember(t => t.Type, opt => opt.MapFrom(t => Enum.Parse(typeof(TravelClassType), t.Type)));
        }
    }
}
