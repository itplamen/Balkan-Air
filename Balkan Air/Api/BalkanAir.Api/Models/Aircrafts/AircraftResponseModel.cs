namespace BalkanAir.Api.Models.Aircrafts
{
    using System.Collections.Generic;

    using AutoMapper;

    using AircraftManufacturers;
    using Data.Models;
    using Flights;
    using Infrastructure.Mapping;

    public class AircraftResponseModel : IMapFrom<Aircraft>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int TotalSeats { get; set; }

        public bool IsDeleted { get; set; }

        public AircraftManufacturerSimpleResponseModel AircraftManufacturer { get; set; }

        public int MadeFlights { get; set; }

        public IEnumerable<FlightSimpleResponseModel> Flights { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Aircraft, AircraftResponseModel>()
                .ForMember(a => a.MadeFlights, opt => opt.MapFrom(a => a.LegInstances.Count));
        }
    }
}