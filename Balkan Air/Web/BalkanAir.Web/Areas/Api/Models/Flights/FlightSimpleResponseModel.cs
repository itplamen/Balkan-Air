namespace BalkanAir.Web.Areas.Api.Models.Flights
{
    using System;

    using Data.Models;
    using Infrastructure.Mapping;
    using Airports;

    public class FlightSimpleResponseModel : IMapFrom<Flight>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public AirportSimpleResponseModel DepartureAirport { get; set; }

        public AirportSimpleResponseModel ArrivalAirport { get; set; }
    }
}