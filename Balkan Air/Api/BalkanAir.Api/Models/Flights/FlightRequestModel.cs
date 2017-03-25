namespace BalkanAir.Api.Models.Flights
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class FlightRequestModel : IMapFrom<LegInstance>, IHaveCustomMappings
    {
        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        public int FlightLegId { get; set; }

        [Required]
        public int FlightStatusId { get; set; }

        [Required]
        public int AircraftId { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<FlightRequestModel, LegInstance>()
                .ForMember(f => f.DepartureDateTime, opt => opt.MapFrom(f => f.Departure))
                .ForMember(f => f.ArrivalDateTime, opt => opt.MapFrom(f => f.Arrival));
        }
    }
}