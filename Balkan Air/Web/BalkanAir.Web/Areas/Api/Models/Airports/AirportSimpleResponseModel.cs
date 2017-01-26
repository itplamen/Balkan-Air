namespace BalkanAir.Web.Areas.Api.Models.Airports
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class AirportSimpleResponseModel : IMapFrom<Airport>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}