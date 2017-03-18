namespace BalkanAir.Web.Areas.Api.Models.FlightLegs
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateFlightLegRequestModel : IMapFrom<FlightLeg>, IHaveCustomMappings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DepartureAirportId { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public int RouteId { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<UpdateFlightLegRequestModel, FlightLeg>()
                .ForMember(f => f.ScheduledDepartureDateTime, opt => opt.MapFrom(f => f.Departure))
                .ForMember(f => f.ScheduledArrivalDateTime, opt => opt.MapFrom(f => f.Arrival));
        }
    }
}