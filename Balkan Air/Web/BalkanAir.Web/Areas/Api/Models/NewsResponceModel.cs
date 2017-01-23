namespace BalkanAir.Web.Areas.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BalkanAir.Data.Models;
    using Infrastructure.Mapping;

    public class NewsResponceModel : IMapFrom<News>
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}