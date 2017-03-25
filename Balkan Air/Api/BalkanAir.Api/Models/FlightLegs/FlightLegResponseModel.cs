namespace BalkanAir.Api.Models.FlightLegs
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Data.Models;
    using FlightNumbers;
    using Flights;
    using Infrastructure.Mapping;
    using Routes;

    public class FlightLegResponseModel : IMapFrom<FlightLeg>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public RouteSimpleResponseModel Route { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public bool IsDeleted { get; set; }

        public FlightNumberSimpleResponseModel Flight { get; set; }

        public IEnumerable<FlightSimpleResponseModel> LegInstances { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<FlightLeg, FlightLegResponseModel>()
                .ForMember(f => f.Departure, opt => opt.MapFrom(f => f.ScheduledDepartureDateTime))
                .ForMember(f => f.Arrival, opt => opt.MapFrom(f => f.ScheduledArrivalDateTime));
        }
    }
}