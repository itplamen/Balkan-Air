namespace BalkanAir.Web.Areas.Api.Models.Flights
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateFlightRequestModel : IMapFrom<LegInstance>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int FlightLegId { get; set; }

        [Required]
        public int FlightStatusId { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<UpdateFlightRequestModel, LegInstance>()
                .ForMember(f => f.DepartureDateTime, opt => opt.MapFrom(f => f.Departure))
                .ForMember(f => f.ArrivalDateTime, opt => opt.MapFrom(f => f.Arrival));
        }
    }
}