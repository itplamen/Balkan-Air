namespace BalkanAir.Api.Models.FlightNumbers
{
    using System.Collections.Generic;

    using Data.Models;
    using FlightLegs;
    using Infrastructure.Mapping;

    public class FlightNumberResponseModel : IMapFrom<Flight>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<FlightLegSimpleResponseModel> FlightLegs { get; set; }
    }
}