namespace BalkanAir.Data.Migrations
{
    using System;
    using System.Collections.Generic;

    using Common;
    using Helper;
    using Models;

    public class SeedData
    {
        private const string SNACK = "Snack";
        private const string MENU = "Menu";

        private IBalkanAirDbContext context;

        public SeedData(IBalkanAirDbContext context)
        {
            this.context = context;

            this.AircraftManufacturers = new List<AircraftManufacturer>();
            this.SeedAircraftManufacturers();

            this.Aircrafts = new List<Aircraft>();
            this.SeedAircrafts();

            this.TravelClasses = new List<TravelClass>();
            this.SeedTravelClasses();

            this.Countries = new List<Country>();
            this.SeedCountries();

            this.Airports = new List<Airport>();
            this.SeedAirports();

            this.FlightStatuses = new List<FlightStatus>();
            this.SeedFlightStatuses();

            this.Routes = new List<Route>();
            this.SeedRoutes();

            this.Fares = new List<Fare>();
            this.SeedFares();

            this.Flights = new List<Flight>();
            this.SeedFlights();

            this.FlightLegs = new List<FlightLeg>();
            this.SeedFlightLegs();

            this.LegInstances = new List<LegInstance>();
            this.SeedLegInstances();

            this.Notifications = new List<Notification>();
            this.SeedNotifications();

            this.Categories = new List<Category>();
            this.SeedCategories();

            this.News = new List<News>();
            this.SeedNews();
        }

        public List<AircraftManufacturer> AircraftManufacturers { get; set; }

        public List<Aircraft> Aircrafts { get; set; }

        public List<TravelClass> TravelClasses { get; set; }

        public List<Country> Countries { get; set; }

        public List<Airport> Airports { get; set; }
        
        public List<FlightStatus> FlightStatuses { get; set; }

        public List<Flight> Flights { get; set; }

        public List<Route> Routes { get; set; }

        public List<Fare> Fares { get; set; }

        public List<FlightLeg> FlightLegs { get; set; }

        public List<LegInstance> LegInstances { get; set; }

        public List<Seat> Seats { get; set; }

        public List<Category> Categories { get; set; }

        public List<News> News { get; set; }

        public List<User> Users { get; set; }

        public List<Notification> Notifications { get; set; }

        private void SeedAircraftManufacturers()
        {
            this.AircraftManufacturers.Add(new AircraftManufacturer() { Name = "Airbus" });
            this.AircraftManufacturers.Add(new AircraftManufacturer() { Name = "Bombardier" });
        }

        private void SeedAircrafts()
        {            
            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A310",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A320",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A330",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            this.Aircrafts.Add(new Aircraft()
            {
                Model = "ERJ140",
                AircraftManufacturer = this.AircraftManufacturers[1]
            });
        }

        private void SeedTravelClasses()
        {
            for (int aircraftId = 1; aircraftId <= this.Aircrafts.Count; aircraftId++)
            {
                this.AddTravelClass(TravelClassType.Economy, SNACK, 4.99m, aircraftId, ValidationConstants.ECONOMY_CLASS_NUMBER_OF_ROWS,
                    ValidationConstants.ECONOMY_CLASS_NUMBER_OF_SEATS);
                this.AddTravelClass(TravelClassType.Business, MENU, 17m, aircraftId, ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_ROWS_FOR_EACH,
                    ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_SEATS_FOR_EACH, true);
                this.AddTravelClass(TravelClassType.First, MENU, 22m, aircraftId, ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_ROWS_FOR_EACH,
                    ValidationConstants.FIRST_AND_BUSINESS_CLASS_NUMBER_OF_SEATS_FOR_EACH, true, true);
            }
        }

        private void AddTravelClass(TravelClassType type, string meal, decimal price, int aircraftId, int numberOfRows,
            int numberOfSeats, bool priorityBoarding = false, bool earnMiles = false)
        {
            this.TravelClasses.Add(new TravelClass()
            {
                Type = type,
                Meal = meal,
                Price = price,
                PriorityBoarding = priorityBoarding,
                EarnMiles = earnMiles,
                NumberOfRows = numberOfRows,
                NumberOfSeats = numberOfSeats,
                AircraftId = aircraftId
            });
        }

        private void SeedCountries()
        {
            this.Countries.Add(new Country() { Name = "Bulgaria", Abbreviation = "BG" });
            this.Countries.Add(new Country() { Name = "Portugal", Abbreviation = "PT" });
            this.Countries.Add(new Country() { Name = "Spain", Abbreviation = "ES" });
            this.Countries.Add(new Country() { Name = "Italy", Abbreviation = "IT" });
            this.Countries.Add(new Country() { Name = "Germany", Abbreviation = "DE" });
            this.Countries.Add(new Country() { Name = "United Kingdom", Abbreviation = "UK" });
            this.Countries.Add(new Country() { Name = "France", Abbreviation = "FR" });
            this.Countries.Add(new Country() { Name = "Czech Republic", Abbreviation = "CZ" });
        }

        private void SeedFlightStatuses()
        {
            this.FlightStatuses.Add(new FlightStatus() { Name = "Landed" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Expected" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Delayed" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Cancelled" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Departed" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Taxiing" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Boarding" });
            this.FlightStatuses.Add(new FlightStatus() { Name = "Check-in" });
        }

        private void SeedAirports()
        {
            this.Airports.Add(new Airport()
            {
                Name = "Sofia",
                Abbreviation = "SOF",
                Country = this.Countries[0]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Varna",
                Abbreviation = "VAR",
                Country = this.Countries[0]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Lisbon",
                Abbreviation = "LIS",
                Country = this.Countries[1]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Porto",
                Abbreviation = "OPO",
                Country = this.Countries[1]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Faro",
                Abbreviation = "FAO",
                Country = this.Countries[1]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Madrid",
                Abbreviation = "MAD",
                Country = this.Countries[2]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Barcelona",
                Abbreviation = "GRO",
                Country = this.Countries[2]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Barcelona",
                Abbreviation = "REU",
                Country = this.Countries[2]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Ibiza",
                Abbreviation = "IBZ",
                Country = this.Countries[2]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Rome",
                Abbreviation = "CIA",
                Country = this.Countries[3]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Rome",
                Abbreviation = "FCO",
                Country = this.Countries[3]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Naples",
                Abbreviation = "NAP",
                Country = this.Countries[3]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Berlin",
                Abbreviation = "SXF",
                Country = this.Countries[4]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Dortmund",
                Abbreviation = "DTM",
                Country = this.Countries[4]
            });
            this.Airports.Add(new Airport()
            {
                Name = "London",
                Abbreviation = "LGW",
                Country = this.Countries[5]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Liverpool",
                Abbreviation = "LPL",
                Country = this.Countries[5]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Paris",
                Abbreviation = "BVA",
                Country = this.Countries[6]
            });
            this.Airports.Add(new Airport()
            {
                Name = "Prague",
                Abbreviation = "PRG",
                Country = this.Countries[7]
            });
        }

        private void SeedRoutes()
        {
            // Sofia - Madrid
            this.Routes.Add(new Route()
            {
                OriginId = 1,
                DestinationId = 6
            });

            // Madrid - Sofia
            this.Routes.Add(new Route()
            {
                OriginId = 6,
                DestinationId = 1
            });

            // Sofia - Lisbon
            this.Routes.Add(new Route()
            {
                OriginId = 1,
                DestinationId = 3
            });

            // Varna - Berlin
            this.Routes.Add(new Route()
            {
                OriginId = 2,
                DestinationId = 13
            });

            // London - Paris
            this.Routes.Add(new Route()
            {
                OriginId = 15,
                DestinationId = 17
            });

            // Liverpool - Berlin, Berlin - Ibiza
            //this.Routes.Add(new Route()
            //{
            //    OriginId = 1,
            //    DestinationId = 3
            //});
        }

        private void SeedFares()
        {
            // Sofia - Madrid
            this.Fares.Add(new Fare()
            {
                Price = 55.90m,
                RouteId = 1
            });

            // Madrid - Sofia
            this.Fares.Add(new Fare()
            {
                Price = 29.99m,
                RouteId = 2
            });

            // Sofia - Lisbon
            this.Fares.Add(new Fare()
            {
                Price = 49.99m,
                RouteId = 3
            });

            // Varna - Berlin
            this.Fares.Add(new Fare()
            {
                Price = 62.20m,
                RouteId = 4
            });

            // London - Paris
            this.Fares.Add(new Fare()
            {
                Price = 19.99m,
                RouteId = 5
            });
        }

        private void SeedFlights()
        {
            // Three flights from Sofia to Madrid in different days

            // Sofia - Madrid
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Sofia - Madrid 
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Sofia - Madrid
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Madrid - Sofia
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Sofia - Lisbon
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Varna - Berlin
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // London - Paris
            this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });

            // Liverpool - Berlin, Berlin - Ibiza
            //this.Flights.Add(new Flight() { Number = new FlightNumber(this.context).GetUniqueFlightNumber() });
        }

        private void SeedFlightLegs ()
        {
            // Sofia - Madrid
            this.AddFlightLeg(1, new TimeSpan(17, 30, 00), 6, new TimeSpan(20, 30, 00), 1, 1);

            // Sofia - Madrid
            this.AddFlightLeg(1, new TimeSpan(17, 30, 00), 6, new TimeSpan(20, 30, 00), 2, 1);

            // Sofia - Madrid
            this.AddFlightLeg(1, new TimeSpan(17, 30, 00), 6, new TimeSpan(20, 30, 00), 3, 1);

            // Madrid - Sofia
            this.AddFlightLeg(6, new TimeSpan(20, 30, 00), 1, new TimeSpan(23, 30, 00), 4, 2);

            // Sofia - Lisbon
            this.AddFlightLeg(1, new TimeSpan(14, 45, 00), 3, new TimeSpan(19, 30, 00), 5, 3);

            // Varna - Berlin
            this.AddFlightLeg(2, new TimeSpan(2, 00, 00), 13, new TimeSpan(5, 35, 00), 6, 4);

            // London - Paris
            this.AddFlightLeg(15, new TimeSpan(18, 10, 00), 17, new TimeSpan(20, 00, 00), 7, 5);

            // Liverpool - Berlin, Berlin - Ibiza
            //this.AddFlightLeg(16, new TimeSpan(8, 15, 00), 13, new TimeSpan(10, 15, 00), 8, 3);
        }

        private void AddFlightLeg(int departureAirportId, TimeSpan cheduledDepartureTime, int arrivalAirportId, 
            TimeSpan scheduledArrivalTime, int flightId, int routeId)
        {
            this.FlightLegs.Add(new FlightLeg()
            {
                DepartureAirportId = departureAirportId,
                ScheduledDepartureTime = cheduledDepartureTime,
                ArrivalAirportId = arrivalAirportId,
                ScheduledArrivalTime = scheduledArrivalTime,
                FlightId = flightId,
                RouteId = routeId
            });
        }

        private void SeedLegInstances()
        {
            // Sofia - Madrid
            this.AddLegInstance(new DateTime(2017, 2, 18), this.FlightLegs[0].ScheduledDepartureTime, 
                this.FlightLegs[0].ScheduledArrivalTime, 1, 8, 1);

            // Sofia - Madrid
            this.AddLegInstance(new DateTime(2017, 2, 23), this.FlightLegs[1].ScheduledDepartureTime,
               this.FlightLegs[1].ScheduledArrivalTime, 2, 8, 1);

            // Sofia - Madrid
            this.AddLegInstance(new DateTime(2017, 2, 28), this.FlightLegs[2].ScheduledDepartureTime,
               this.FlightLegs[2].ScheduledArrivalTime, 3, 8, 1);

            // Madrid - Sofia
            this.AddLegInstance(new DateTime(2017, 2, 18), this.FlightLegs[3].ScheduledDepartureTime,
               this.FlightLegs[3].ScheduledArrivalTime, 4, 8, 1);

            // Sofia - Lisbon
            this.AddLegInstance(new DateTime(2017, 2, 18), this.FlightLegs[4].ScheduledDepartureTime,
               this.FlightLegs[4].ScheduledArrivalTime, 5, 8, 2);

            // Varna - Berlin
            this.AddLegInstance(new DateTime(2017, 2, 19), this.FlightLegs[5].ScheduledDepartureTime,
               this.FlightLegs[5].ScheduledArrivalTime, 6, 8, 3);

            // London - Paris
            this.AddLegInstance(new DateTime(2017, 2, 18), this.FlightLegs[6].ScheduledDepartureTime,
               this.FlightLegs[6].ScheduledArrivalTime, 7, 8, 4);
        }

        private void AddLegInstance(DateTime dateOfTravel, TimeSpan departureTime, TimeSpan arrivalTime, int flightLegId,
            int flightStatusId, int aircraftId)
        {
            this.LegInstances.Add(new LegInstance()
            {
                DateOfTravel = dateOfTravel,
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                FlightLegId = flightLegId,
                FlightStatusId = flightStatusId,
                AircraftId = aircraftId
            });
        }

        private void SeedCategories()
        {
            this.Categories.Add(new Category() { Name = "Routes" });
            this.Categories.Add(new Category() { Name = "Jobs" });
            this.Categories.Add(new Category() { Name = "Aircrafts" });
            this.Categories.Add(new Category() { Name = "Passengers" });
            this.Categories.Add(new Category() { Name = "Discounts" });
            this.Categories.Add(new Category() { Name = "Business" });
            this.Categories.Add(new Category() { Name = "Weather" });
        }

        private void SeedNotifications()
        {
            string welcomeNotificationContent = "Welcome to Balkan Air! Please, go to your email and confirm your account. " + 
                "If you lost your confirmation email go to 'Settings' and select 'Send me another confirmation email'!";

            this.Notifications.Add(new Notification()
            {
                Content = welcomeNotificationContent,
                DateCreated = DateTime.Now,
                Type = NotificationType.Welcome
            });

            string setAccountNotificationContent = "Thank you for confirming your account! Don't forget to fill all personal " + 
                "information abou you in order to book flights!";

            this.Notifications.Add(new Notification()
            {
                Content = setAccountNotificationContent,
                DateCreated = DateTime.Now,
                Type = NotificationType.AccountConfirmation
            });

            string newFlightBooked = "You have booked a new flight. Check out your profile and email to find information about " + 
                "your flight. Don't forget to confirm your booking!";

            this.Notifications.Add(new Notification()
            {
                Content = newFlightBooked,
                DateCreated = DateTime.Now,
                Type = NotificationType.FlightBooked
            });
        }

        private void SeedNews()
        {
            this.News.Add(new News()
            {
                Category = Categories[0],
                Title = "New Sofia - Budapest route launched",
                Content = "<p>Balkan Air launched a new route from Sofia (SOF) to Budapest (BUD), with a three times weekly " +
                "service beginning in October, as part of its winter 2017 schedule which will go on sale soon.</p>" +
                "<p>Balkan Air celebrated its new Sofia - Budapest route by releasing seats for sale at prices starting from " +
                "just &#8364;9.99 for travel in February and March. These low fare seats are available for booking until " +
                "midnight Monday, 30 January.</p>" +
                "<p><strong>Our Director of Air Service Development, Paul Winfield said:</strong></p>" +
                "<p><i>“It's great to be able to celebrate our first new route announcement of the year so early into 2017 and " +
                "for another airport in Italy to become linked with Liverpool later this year.</i></p>",
                DateCreated = DateTime.Now
            });
        }
    }
}
