namespace BalkanAir.Api.Models.Aircrafts
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class AircraftSimpleResponseModel : IMapFrom<Aircraft>
    {
        public int Id { get; set; }

        public string Model { get; set; }
        
        public int TotalSeats { get; set; }
    }
}