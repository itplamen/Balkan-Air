namespace BalkanAir.Api.Models.AircraftManufacturers
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class AircraftManufacturerSimpleResponseModel : IMapFrom<AircraftManufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}