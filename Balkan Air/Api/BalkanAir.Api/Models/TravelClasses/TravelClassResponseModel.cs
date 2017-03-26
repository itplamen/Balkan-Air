namespace BalkanAir.Api.Models.TravelClasses
{
    using AutoMapper;

    using Aircrafts;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TravelClassResponseModel : IMapFrom<TravelClass>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Meal { get; set; }

        public bool PriorityBoarding { get; set; }

        public bool ReservedSeat { get; set; }

        public bool EarnMiles { get; set; }

        public int NumberOfRows { get; set; }

        public int NumberOfSeats { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public AircraftSimpleResponseModel Aircraft { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<TravelClass, TravelClassResponseModel>()
                .ForMember(t => t.Type, opt => opt.MapFrom(t => t.Type.ToString()));
        }
    }
}
