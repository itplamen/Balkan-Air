namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
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
            if (news == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.news.Add(news);
            this.news.SaveChanges();

            return news.Id;
        }

        public News GetNews(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.news.GetById(id);
        }

        public IQueryable<News> GetAll()
        {
            return this.news.All();
        }

        public News UpdateNews(int id, News news)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (news == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
