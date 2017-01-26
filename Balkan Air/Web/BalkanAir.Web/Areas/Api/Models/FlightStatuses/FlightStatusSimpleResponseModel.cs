namespace BalkanAir.Web.Areas.Api.Models.FlightStatuses
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class FlightStatusSimpleResponseModel : IMapFrom<FlightStatus>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}