namespace BalkanAir.Api.Models.Categories
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Data.Models;
    using Infrastructure.Mapping;

    public class UpdateCategoryRequestModel : IMapFrom<Category>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}