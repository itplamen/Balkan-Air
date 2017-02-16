namespace BalkanAir.Web.Areas.Api.Models.Flights
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Aircrafts;
    using Airports;
    using Data.Models;
    using FlightStatuses;
    using Infrastructure.Mapping;
    using TravelClasses;

    public class FlightResponseModel : IMapFrom<Flight>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public bool IsDeleted { get; set; }

        public FlightStatusSimpleResponseModel FlightStatus { get; set; }

        public AircraftSimpleResponseModel  Aircraft { get; set; }
    
        public AirportSimpleResponseModel DepartureAirport { get; set; }

        public AirportSimpleResponseModel ArrivalAirport { get; set; }

        public IEnumerable<TravelClassSimpleResponseModel> TravelClasses { get; set; }

        public int NumberOfBookings { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return this.Arrival - this.Departure;
            }
        }

        public decimal CheapestPriceFromAllTravelClasses
        {
            get
            {
                if (!this.TravelClasses.Any(tr => tr.Type == TravelClassType.Economy.ToString()))
                {
                    return 0;
                }

                return this.TravelClasses
                    .FirstOrDefault(tr => tr.Type == TravelClassType.Economy.ToString())
                    .Price;
            }
        }

        public void CreateMappings(IConfiguration config)
        {
            //config.CreateMap<Flight, FlightResponseModel>()
            //    .ForMember(f => f.NumberOfBookings, opt => opt.MapFrom(f => f.Bookings.Count));
        }
    }
}