namespace BalkanAir.Web.Areas.Api.Models.Flights
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    using AutoMapper;

    using Data.Common;
    using Data.Models;
    using Infrastructure.Mapping;
    using System.Collections.Generic;
    using TravelClasses;

    public class FlightRequestModel : IMapFrom<Flight>
    {
        [Required]
        [Index(IsUnique = true)]
        [StringLength(ValidationConstants.FLIGHT_NUMBER_LENGTH)]
        public string Number { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public int FlightStatusId { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public int DepartureAirportId { get; set; }

        [Required]
        public int ArrivalAirportId { get; set; }

        [Required]
        public IEnumerable<TravelClassRequestModel> TravelClasses { get; set; }
    }
}