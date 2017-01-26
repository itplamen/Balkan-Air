namespace BalkanAir.Web.Areas.Api.Models.Users
{
    using AutoMapper;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UsersResponseModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Nationality { get; set; }

        public int NumberOfBookings { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<User, UsersResponseModel>()
                .ForMember(u => u.Gender, opt => opt.MapFrom(u => u.UserSettings.Gender.ToString()))
                .ForMember(u => u.NumberOfBookings, opt => opt.MapFrom(u => u.UserSettings.Bookings.Count));
        }
    }
}