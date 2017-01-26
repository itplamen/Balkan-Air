namespace BalkanAir.Web.Areas.Api.Models.AircraftManufacturers
{
    using System.Collections.Generic;

    using AutoMapper;

    using Aircrafts;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AircraftManufacturerResponseModel : IMapFrom<AircraftManufacturer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public int NumberOfAircrafts { get; set; }

        public IEnumerable<AircraftSimpleResponseModel> Aircrafts { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<AircraftManufacturer, AircraftManufacturerResponseModel>()
                .ForMember(am => am.NumberOfAircrafts, opt => opt.MapFrom(am => am.Aircrafts.Count));
        }
    }
}