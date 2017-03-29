namespace BalkanAir.Tests.Common.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data.Models;
    
    public static class TestObjectFactoryDataModels
    {
        public static IQueryable<AircraftManufacturer> AircraftManufacturers = new List<AircraftManufacturer>()
        {
            new AircraftManufacturer()
            {
                Id = 1,
                Name = "Manufacturer Test"
            }
        }.AsQueryable();

        public static IQueryable<Aircraft> Aircrafts = new List<Aircraft>()
        {
            new Aircraft()
            {
                Id = 1,
                Model = "Aircraft Model Test",
                TotalSeats = 1,
                AircraftManufacturer = new AircraftManufacturer() { Name = "Manufacturer Test" }
            }
        }.AsQueryable();

        public static IQueryable<Airport> Airports = new List<Airport>()
        {
            new Airport()
            {
                Id = 1,
                Name = "Test Name",
                Abbreviation = Constants.AIRPORT_VALID_ABBREVIATION,
                CountryId = 1
            }
        }.AsQueryable();

        public static IQueryable<Baggage> Baggage = new List<Baggage>()
        {
            new Baggage()
            {
                Id = 1,
                Type = BaggageType.Cabin,
                Price = 1,
                BookingId = 1
            }
        }.AsQueryable();

        public static IQueryable<Booking> Bookings = new List<Booking>()
        {
            new Booking()
            {
                Id = 1,
                User = new User()
                {
                    UserSettings = new UserSettings()
                    {
                        FirstName = "Test First Name",
                        LastName = "Test Last Name"
                    }
                },
                LegInstanceId = 1
            }
        }.AsQueryable();

        public static IQueryable<Category> Categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = Constants.CATEGORY_VALID_NAME
            }
        }.AsQueryable();

        public static IQueryable<Country> Countries = new List<Country>()
        {
            new Country()
            {
                Id = 1,
                Name = "Country Test",
                Abbreviation = Constants.COUNTRY_VALID_ABBREVIATION
            }
        }.AsQueryable();

        public static IQueryable<Fare> Fares = new List<Fare>()
        {
            new Fare()
            {
                Id = 1,
                Price = 1m,
                RouteId = 1,
                Route = new Route()
                {
                    Origin = new Airport()
                    {
                        Name = "Test Origin",
                        Abbreviation = Constants.ROUTE_VALID_ORIGIN_ABBREVIATION
                    },
                    Destination = new Airport()
                    {
                        Name = "Test Destination",
                        Abbreviation = Constants.ROUTE_VALID_DESTINATION_ABBREVIATION
                    }
                }
            }
        }.AsQueryable();

        public static IQueryable<FlightLeg> FlightLegs = new List<FlightLeg>()
        {
            new FlightLeg()
            {
                Id = 1,
                DepartureAirportId = 1,
                ScheduledDepartureDateTime = DateTime.Now,
                ArrivalAirportId = 1,
                ScheduledArrivalDateTime = DateTime.Now,
                FlightId = 1,
                RouteId = 1
            }
        }.AsQueryable();

        public static IQueryable<Flight> Flights = new List<Flight>()
        {
            new Flight()
            {
                Id = 1,
                Number = "ABCDEF"
            }
        }.AsQueryable();

        public static IQueryable<LegInstance> LegInstances = new List<LegInstance>()
        {
            new LegInstance()
            {
                Id = 1,
                DepartureDateTime = new DateTime(2017, 1, 1, 1, 1, 1),
                ArrivalDateTime = new DateTime(2017, 1, 1, 1, 1, 1),
                Price = 1m,
                FlightLegId = 1,
                FlightStatusId = 1,
                AircraftId = 1,
                Aircraft = new Aircraft(),
                FlightStatus = new FlightStatus()
                {
                    Name = Constants.FLIGHT_VALID_STATUS
                },
                FlightLeg = new FlightLeg()
                {
                    Flight = new Flight()
                    {
                        Number = Constants.FLIGHT_VALID_NUMBER
                    },
                    Route = new Route()
                    {
                        Origin = new Airport() { Abbreviation = Constants.ROUTE_VALID_ORIGIN_ABBREVIATION },
                        Destination = new Airport() { Abbreviation = Constants.ROUTE_VALID_DESTINATION_ABBREVIATION }
                    }
                }
            }
        }.AsQueryable();

        public static IQueryable<News> News = new List<News>()
        {
            new News()
            {
                Id = 1,
                Title = "Test News Title",
                Content = "Test News Content",
                DateCreated = new DateTime(2017, 1, 1, 1, 1, 1),
                Category = new Category()
                {
                    Id = 1,
                    Name = Constants.CATEGORY_VALID_NAME
                }
            }
        }.AsQueryable();

        public static IQueryable<Route> Routes = new List<Route>()
        {
            new Route()
            {
                Id = 1,
                Origin = new Airport()
                {
                    Name = "Test Origin",
                    Abbreviation = "ABC"
                },
                Destination = new Airport()
                {
                    Name = "Test Destination",
                    Abbreviation = "DEF"
                }
            }
        }.AsQueryable();

        public static IQueryable<TravelClass> TravelClasses = new List<TravelClass>()
        {
            new TravelClass()
            {
                Id = 1,
                Type = TravelClassType.Economy,
                Meal = "Test Meal",
                NumberOfRows = 5,
                NumberOfSeats = 20,
                Price = 1,
                Aircraft = new Aircraft()
                {
                    Id = 1
                }
            }
        }.AsQueryable();

        public static IQueryable<User> Users = new List<User>()
        {
            new User()
            {
                UserSettings = new UserSettings()
                {
                    FirstName = "Test First Name",
                    LastName = "Test Last Name",
                    Gender = Constants.USER_VALID_GENDER,
                    Nationality = Constants.USER_VALID_NATIONALITY
                }       
            }
        }.AsQueryable();
    }
}
