namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class CommentsManagementViewModel
    {
        public IQueryable<Comment> Comments { get; set; }
    }
}
