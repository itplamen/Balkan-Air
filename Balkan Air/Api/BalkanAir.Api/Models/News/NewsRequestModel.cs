namespace BalkanAir.Api.Models.News
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Infrastructure.Mapping;

    public class NewsRequestModel : IMapFrom<Data.Models.News>, IHaveCustomMappings
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<NewsRequestModel, BalkanAir.Data.Models.News> ()
                .ForMember(n => n.DateCreated, opt => opt.MapFrom(n => DateTime.Now));
        }
    }
}