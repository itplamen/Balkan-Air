namespace BalkanAir.Web.Areas.Api.Models.Fares
{
    using Data.Models;
    using Infrastructure.Mapping;
    using Routes;

    public class FareResponseModel : IMapFrom<Fare>
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public virtual RouteSimpleResponseModel Route { get; set; }
    }
}