namespace BalkanAir.Mvp.News
{
    using System.Linq;

    using Data.Models;

    public class NewsViewModel
    {
        public IQueryable<News> News { get; set; }
    }
}
