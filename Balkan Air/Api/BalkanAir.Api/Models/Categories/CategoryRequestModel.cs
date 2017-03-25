namespace BalkanAir.Api.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CategoryRequestModel : IMapFrom<Category>
    {
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}