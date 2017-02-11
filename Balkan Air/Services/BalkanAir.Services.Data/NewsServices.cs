namespace BalkanAir.Services.Data
{
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class NewsServices : INewsServices
    {
        private IRepository<News> news;

        public NewsServices(IRepository<News> news)
        {
            this.news = news;
        }

        public int AddNews(News news)
        {
            this.news.Add(news);
            this.news.SaveChanges();

            return news.Id;
        }

        public News GetNews(int id)
        {
            return this.news.GetById(id);
        }

        public IQueryable<News> GetAll()
        {
            return this.news.All();
        }

        public News UpdateNews(int id, News news)
        {
            var newsToUpdate = this.news.GetById(id);

            if (newsToUpdate != null)
            {
                newsToUpdate.Title = news.Title;
                newsToUpdate.HeaderImage = news.HeaderImage;
                newsToUpdate.Content = news.Content;
                newsToUpdate.IsDeleted = news.IsDeleted;
                newsToUpdate.CategoryId = news.CategoryId;

                this.news.SaveChanges();
            }

            return newsToUpdate;
        }

        public News DeleteNews(int id)
        {
            var newsToDelete = this.news.GetById(id);

            if (newsToDelete != null)
            {
                newsToDelete.IsDeleted = true;
                this.news.SaveChanges();
            }

            return newsToDelete;
        }

         
    }
}
