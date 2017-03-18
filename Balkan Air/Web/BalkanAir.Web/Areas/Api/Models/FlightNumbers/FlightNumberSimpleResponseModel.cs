namespace BalkanAir.Web.Areas.Api.Models.FlightNumbers
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class FlightNumberSimpleResponseModel : IMapFrom<Flight>
    {
        public int Id { get; set; }
        
        public string Number { get; set; }
    }
}