namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Models.News;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class NewsController : ApiController
    {
        private readonly INewsServices newsServices;
        private readonly ICategoriesServices categoriesServices;

        public NewsController(INewsServices newsServices, ICategoriesServices categoriesServices)
        {
            this.newsServices = newsServices;
            this.categoriesServices = categoriesServices;
        }

        [HttpPost]
        public IHttpActionResult Create(NewsRequestModel news)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newsToAdd = Mapper.Map<Data.Models.News>(news);
            var addedNewsId = this.newsServices.AddNews(newsToAdd);

            return this.Ok(addedNewsId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var news = this.newsServices.GetAll()
                .ProjectTo<NewsResponseModel>()
                .ToList();

            return this.Ok(news);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var news = this.newsServices.GetAll()
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
            if (count <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_COUNT_VALUE);
            }

            var latestNews = this.newsServices.GetAll()
                .Where(n => !n.IsDeleted)
                .OrderByDescending(n => n.DateCreated)
                .ProjectTo<NewsResponseModel>()
                .Take(count);

            return this.Ok(latestNews);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetLatesByCategory(int count, string category)
        {
            if (count <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_COUNT_VALUE);
            }

            if (string.IsNullOrEmpty(category))
            {
                return this.BadRequest(ErrorMessages.NULL_OR_EMPTY_ENTITY_NAME);
            }

            if (!this.categoriesServices.GetAll().Any(c => c.Name.ToLower() == category.ToLower()))
            {
                return this.BadRequest(ErrorMessages.INVALID_CATEGORY_NAME);
            }

            var latestNews = this.newsServices.GetAll()
                .Where(n => !n.IsDeleted && n.Category.Name.ToLower() == category.ToLower())
                .OrderByDescending(n => n.DateCreated)
                .ProjectTo<NewsResponseModel>()
                .Take(count);

            return this.Ok(latestNews);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateNewsRequestModel news)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newsToUpdate = Mapper.Map<Data.Models.News>(news);
            var updatedNews = this.newsServices.UpdateNews(id, newsToUpdate);

            if (updatedNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedNews.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedNews = this.newsServices.DeleteNews(id);

            if (deletedNews == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedNews.Id);
        }
    }
}