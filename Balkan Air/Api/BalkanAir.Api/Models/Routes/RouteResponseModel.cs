namespace BalkanAir.Api.Models.Routes
{
    using Airports;
    using Data.Models;
    using Infrastructure.Mapping;

    public class RouteResponseModel : IMapFrom<Route>
    {
        public int Id { get; set; }

        public AirportSimpleResponseModel Origin { get; set; }

        public AirportSimpleResponseModel Destination { get; set; }

        public string DistanceInKm { get; set; }

        public bool IsDeleted { get; set; }
    }
}
