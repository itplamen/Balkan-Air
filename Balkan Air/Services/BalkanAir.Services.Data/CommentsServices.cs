namespace BalkanAir.Services.Data
{
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class CommentsServices : ICommentsServices
    {
        private readonly IRepository<Comment> comments;

        public CommentsServices(IRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public int AddComment(Comment comment)
        {
            this.comments.Add(comment);
            this.comments.SaveChanges();

            return comment.Id;
        }

        public Comment GetComment(int id)
        {
            return this.comments.GetById(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return this.comments.All();
        }

        public Comment UpdateComment(int id, Comment comment)
        {
            var commentToUpdate = this.comments.GetById(id);

            if (commentToUpdate != null)
            {
                commentToUpdate.Content = comment.Content;
                commentToUpdate.IsDeleted = commentToUpdate.IsDeleted;
                this.comments.SaveChanges();
            }

            return commentToUpdate;
        }

        public Comment DeleteComment(int id)
        {
            var commentToDelete = this.comments.GetById(id);

            if (commentToDelete != null)
            {
                commentToDelete.IsDeleted = true;
                this.comments.SaveChanges();
            }

            return commentToDelete;
        }
    }
}
