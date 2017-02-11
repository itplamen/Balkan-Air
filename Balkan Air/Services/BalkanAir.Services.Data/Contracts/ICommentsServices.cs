namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ICommentsServices
    {
        int AddComment(Comment comment);

        Comment GetComment(int id);

        IQueryable<Comment> GetAll();

        Comment UpdateComment(int id, Comment comment);

        Comment DeleteComment(int id);
    }
}
