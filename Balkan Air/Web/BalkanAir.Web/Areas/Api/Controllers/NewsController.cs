namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper.QueryableExtensions;

    using Ninject;

    using Data.Services.Contracts;
    using Models;

    public class NewsController : ApiController
    {
        [Inject]
        public INewsServices NewsServices { get; set; }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(NewsResponceModel news)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }


            return this.Ok(1);
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult All()
        {
            var articles = this.NewsServices.GetAll()
                .ProjectTo<NewsResponceModel>();

            return this.Ok(articles);
        }
    }
}