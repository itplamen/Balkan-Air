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
            {   Id = 1,
                Name = "Test Name",
                Abbreviation = "ABC",
                CountryId = 1
            }
        }.AsQueryable();

        public static IQueryable<Category> Categories = new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Category Test"
            }
        }.AsQueryable();

        public static IQueryable<Country> Countries = new List<Country>()
        {
            new Country()
            {
                Id = 1,
                Name = "Country Test",
                Abbreviation = "CT"
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
                        Abbreviation = "ABC"
                    },
                    Destination = new Airport()
                    {
                        Name = "Test Destination",
                        Abbreviation = "DEF"
                    }
                }
            }
        }.AsQueryable();

        public static IQueryable<LegInstance> Flights = new List<LegInstance>()
        {
            new LegInstance()
            {
                Id = 1,
                DepartureDateTime = new DateTime(2017, 1, 1, 1, 1, 1),
                ArrivalDateTime = DateTime.Now,
                Price = 1m,
                FlightLegId = 1,
                FlightStatusId = 1,
                AircraftId = 1,
                Aircraft = new Aircraft(),
                FlightStatus = new FlightStatus()
                {
                    Name = "Test"
                },
                FlightLeg = new FlightLeg()
                {
                    Flight = new Flight()
                    {
                        Number = "Test12"
                    },
                    Route = new Route()
                    {
                        Origin = new Airport() { Abbreviation = "ABC" },
                        Destination = new Airport() { Abbreviation = "DEF" }
                    }
                }
            }
        }.AsQueryable();
    }
}
