namespace BalkanAir.Web.Areas.Api.Models.Categories
{
    using System.Collections.Generic;

    using Data.Models;
    using Infrastructure.Mapping;
    using News;

    public class CategoryResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual IEnumerable<NewsSimpleResponseModel> News { get; set; }
    }
}