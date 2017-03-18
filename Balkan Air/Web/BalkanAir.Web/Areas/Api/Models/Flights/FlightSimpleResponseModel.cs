namespace BalkanAir.Web.Areas.Api.Models.Flights
{
    using System;

    using AutoMapper;

    using Airports;
    using Data.Models;
    using Infrastructure.Mapping;

    public class FlightSimpleResponseModel : IMapFrom<LegInstance>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public AirportSimpleResponseModel Origin { get; set; }

        public AirportSimpleResponseModel Destination { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<LegInstance, FlightSimpleResponseModel>()
                .ForMember(l => l.Departure, opt => opt.MapFrom(l => l.DepartureDateTime))
                .ForMember(l => l.Arrival, opt => opt.MapFrom(l => l.ArrivalDateTime))
                .ForMember(l => l.Origin, opt => opt.MapFrom(l => l.FlightLeg.Route.Origin))
                .ForMember(l => l.Destination, opt => opt.MapFrom(l => l.FlightLeg.Route.Destination));
        }
    }
}