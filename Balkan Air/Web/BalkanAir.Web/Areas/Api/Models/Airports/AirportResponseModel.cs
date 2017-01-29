namespace BalkanAir.Web.Areas.Api.Models.Airports
{
    using System.Collections.Generic;

    using AutoMapper;

    using Countries;
    using Data.Models;
    using Flights;
    using Infrastructure.Mapping;

    public class AirportResponseModel : IMapFrom<Airport>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public bool IsDeleted { get; set; }

        public CountrySimpleResponseModel Country { get; set; }

        public int NumberOfDeparturesFlights { get; set; }

        public IEnumerable<FlightSimpleResponseModel> DeparturesFlights { get; set; }

        public int NumberOfArrivalsFlights { get; set; }

        public IEnumerable<FlightSimpleResponseModel> ArrivalsFlights { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Airport, AirportResponseModel>()
                .ForMember(a => a.NumberOfDeparturesFlights, opt => opt.MapFrom(a => a.DeparturesFlights.Count))
                .ForMember(a => a.NumberOfArrivalsFlights, opt => opt.MapFrom(a => a.ArrivalsFlights.Count));
        }
    }
}