namespace BalkanAir.Web.Areas.Api.Models.News
{
    using System;

    using AutoMapper;

    using Infrastructure.Mapping;

    public class NewsSimpleResponseModel : IMapFrom<BalkanAir.Data.Models.News>, IHaveCustomMappings
    {
        private const int START_INDEX = 0;
        private const int MAX_LENGTH = 100;

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<BalkanAir.Data.Models.News, NewsSimpleResponseModel>()
                .ForMember(n => n.Title, opt => opt.MapFrom(n => n.Title.Substring(START_INDEX, MAX_LENGTH) + "..."))
                .ForMember(n => n.Content, opt => opt.MapFrom(n => n.Content.Substring(START_INDEX, MAX_LENGTH) + "..."));
        }
    }
}