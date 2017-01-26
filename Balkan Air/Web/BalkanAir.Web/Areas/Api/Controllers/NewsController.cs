namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Ninject;

    using BalkanAir.Common;
    using BalkanAir.Services.Data.Contracts;
    using Models.News;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class NewsController : ApiController
    {
        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

        [Inject]
        public INewsServices NewsServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(NewsRequestModel news)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newsToAdd = Mapper.Map<BalkanAir.Data.Models.News>(news);
            var addedNewsId = this.NewsServices.AddNews(newsToAdd);

            return this.Ok(addedNewsId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var news = this.NewsServices.GetAll()
                .ProjectTo<NewsResponseModel>();

            return this.Ok(news);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var news = this.NewsServices.GetAll()
                .ProjectTo<NewsResponseModel>()
                .FirstOrDefault(n => n.Id == id);

            if (news == null)
            {
                return this.NotFound();
            }

            return this.Ok(news);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetLatestNews(int count = 5)
        {
            var latestNews = this.NewsServices.GetAll()
                .ProjectTo<NewsResponseModel>()
                .Where(n => !n.IsDeleted)
                .OrderByDescending(n => n.DateCreated)
                .Take(count);

            if (latestNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(latestNews);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetLatesByCategory(int count, string category)
        {
            var byCategory = this.CategoriesServices.GetAll()
                .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());

            if (byCategory == null)
            {
                return this.BadRequest("Invalid category!");
            }

            var latestNews = this.NewsServices.GetAll()
                .ProjectTo<NewsResponseModel>()
                .Where(n => !n.IsDeleted && n.Category.Name.ToLower() == byCategory.Name.ToLower())
                .OrderByDescending(n => n.DateCreated)
                .Take(count);

            if (latestNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(latestNews);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateNewsRequestModel news)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newsToUpdate = Mapper.Map<BalkanAir.Data.Models.News>(news);
            var updatedNews = this.NewsServices.UpdateNews(id, newsToUpdate);

            if (updatedNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedNews.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedNews = this.NewsServices.DeleteNews(id);

            if (deletedNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedNews.Id);
        }
    }
}