namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class RoutesServices : IRoutesServices
    {
        private readonly IRepository<Route> routes;

        public RoutesServices(IRepository<Route> routes)
        {
            this.routes = routes;
        }

        public int AddRoute(Route route)
        {
            if (route == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.routes.Add(route);
            this.routes.SaveChanges();

            return route.Id;
        }

        public IQueryable<Route> GetAll()
        {
            return this.routes.All();
        }

        public Route GetRoute(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.routes.GetById(id);
        }

        public Route UpdateRoute(int id, Route route)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (route == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var routeToUpdate = this.routes.GetById(id);

            if (routeToUpdate != null)
            {
                routeToUpdate.OriginId = route.OriginId;
                routeToUpdate.DestinationId = route.DestinationId;
                routeToUpdate.DistanceInKm = route.DistanceInKm;
                routeToUpdate.IsDeleted = route.IsDeleted;

                this.routes.SaveChanges();
            }

            return routeToUpdate;
        }

        public Route DeleteRoute(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var routeToDelete = this.routes.GetById(id);

            if (routeToDelete != null)
            {
                routeToDelete.IsDeleted = true;

                this.routes.SaveChanges();
            }

            return routeToDelete;
        }
    }
}
