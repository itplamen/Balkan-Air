namespace BalkanAir.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class News
    {
        public News()
        {
            this.Comments = new HashSet<Comment>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public byte[] HeaderImage { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
