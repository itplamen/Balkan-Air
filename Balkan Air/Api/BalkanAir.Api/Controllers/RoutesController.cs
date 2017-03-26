namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Models;
    using Models.Routes;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class RoutesController : ApiController
    {
        private readonly IRoutesServices routesServices;

        public RoutesController(IRoutesServices routesServices)
        {
            this.routesServices = routesServices;
        }

        [HttpPost]
        public IHttpActionResult Create(RouteRequestModel route)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var routeToAdd = Mapper.Map<Route>(route);
            var routeId = this.routesServices.AddRoute(routeToAdd);

            return this.Ok(routeId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var routes = this.routesServices.GetAll()
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name)
                .ProjectTo<RouteResponseModel>()
                .ToList();

            return this.Ok(routes);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var route = this.routesServices.GetAll()
                .ProjectTo<RouteResponseModel>()
                .FirstOrDefault(r => r.Id == id);

            if (route == null)
            {
                return this.NotFound();
            }

            return this.Ok(route);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateRouteRequestModel route)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var routeToUpdate = Mapper.Map<Route>(route);
            var updatedRoute = this.routesServices.UpdateRoute(id, routeToUpdate);

            if (updatedRoute == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedRoute.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedRoute = this.routesServices.DeleteRoute(id);

            if (deletedRoute == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedRoute.Id);
        }
    }
}
