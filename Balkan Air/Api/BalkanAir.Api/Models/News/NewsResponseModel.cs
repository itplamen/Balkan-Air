namespace BalkanAir.Api.Models.News
{
    using System;

    using Categories;
    using Data.Models;
    using Infrastructure.Mapping;

    public class NewsResponseModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public CategorySimpleResponseModel Category { get; set; }
    }
}