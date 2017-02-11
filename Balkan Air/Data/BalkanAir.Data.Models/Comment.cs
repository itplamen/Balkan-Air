namespace BalkanAir.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.COMMENT_CONTENT_LENGHT)]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfComment { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public int NewsId { get; set; }

        [ForeignKey("NewsId")]
        public virtual News News { get; set; }
    }
}
