namespace BalkanAir.Web.Areas.Api.Models.News
{
    using System.ComponentModel.DataAnnotations;

    using Infrastructure.Mapping;

    public class UpdateNewsRequestModel : IMapFrom<BalkanAir.Data.Models.News>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}