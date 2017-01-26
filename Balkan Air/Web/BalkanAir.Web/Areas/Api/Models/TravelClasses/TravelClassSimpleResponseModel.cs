namespace BalkanAir.Web.Areas.Api.Models.TravelClasses
{
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class TravelClassSimpleResponseModel : IMapFrom<TravelClass>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<TravelClass, TravelClassSimpleResponseModel>()
                .ForMember(tc => tc.Type, opt => opt.MapFrom(tc => tc.Type.ToString()));
        }
    }
}