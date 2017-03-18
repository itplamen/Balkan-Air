namespace BalkanAir.Web.Areas.Api.Models.Routes
{
    using Airports;
    using Data.Models;
    using Infrastructure.Mapping;

    public class RouteSimpleResponseModel : IMapFrom<Route>
    {
        public int Id { get; set; }

        public AirportSimpleResponseModel Origin { get; set; }

        public AirportSimpleResponseModel Destination { get; set; }
    }
}