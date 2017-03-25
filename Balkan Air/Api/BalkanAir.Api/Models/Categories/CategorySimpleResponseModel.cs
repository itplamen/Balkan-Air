namespace BalkanAir.Api.Models.Categories
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class CategorySimpleResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}