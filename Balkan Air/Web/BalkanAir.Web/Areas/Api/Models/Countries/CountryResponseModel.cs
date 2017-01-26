namespace BalkanAir.Web.Areas.Api.Models.Countries
{
    using System.Collections.Generic;

    using AutoMapper;

    using Airports;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CountryResponseModel : IMapFrom<Country>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public bool IsDeleted { get; set; }

        public int NumberOfAirports { get; set; }

        public IEnumerable<AirportSimpleResponseModel> Airports { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Country, CountryResponseModel>()
                .ForMember(c => c.NumberOfAirports, opt => opt.MapFrom(c => c.Airports.Count));
        }
    }
}