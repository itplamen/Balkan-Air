namespace BalkanAir.Web.Areas.Api.Models.FlightLegs
{
    using System;

    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;
    using Routes;

    public class FlightLegSimpleResponseModel : IMapFrom<FlightLeg>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public RouteSimpleResponseModel Route { get; set; }

        public int LegInstances { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<FlightLeg, FlightLegSimpleResponseModel>()
                .ForMember(f => f.Departure, opt => opt.MapFrom(f => f.ScheduledDepartureDateTime))
                .ForMember(f => f.Arrival, opt => opt.MapFrom(f => f.ScheduledArrivalDateTime))
                .ForMember(f => f.LegInstances, opt => opt.MapFrom(f => f.LegInstances.Count));
        }
    }
}