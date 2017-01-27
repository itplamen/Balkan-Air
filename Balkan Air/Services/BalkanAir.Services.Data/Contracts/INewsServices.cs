namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface INewsServices
    {
        int AddNews(News news);

        News GetNews(int id);

        IQueryable<News> GetAll();

        News UpdateNews(int id, News news);

        News DeleteNews(int id);
    }
}
