namespace BalkanAir.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Helper;
    using Models;

    public class SeedData
    {
        private const int FIRST_CLASS_ROWS = 2;
        private const int BUSINESS_CLASS_ROWS = 4;

        private IBalkanAirDbContext context;

        public SeedData(IBalkanAirDbContext context)
        {
            this.context = context;

            this.AircraftManufacturers = new List<AircraftManufacturer>();
            this.SeedAircraftManufacturers();

            this.Aircrafts = new List<Aircraft>();
            this.SeedAircrafts();

            this.Countries = new List<Country>();
            this.SeedCountries();

            this.Airports = new List<Airport>();
            this.SeedAirports();

            this.FlightStatuses = new List<FlightStatus>();
            this.SeedFlightStatuses();

            this.Flights = new List<Flight>();
            this.SeedFlights();

            this.TravelClasses = new List<TravelClass>();
            this.SeedTravelClasses();

            this.Seats = new List<Seat>();
            //this.SeedSeats();

            this.Notifications = new List<Notification>();
            this.SeedNotifications();

            this.Categories = new List<Category>();
            this.SeedCategories();

            this.News = new List<News>();
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

        public List<AircraftManufacturer> AircraftManufacturers { get; set; }

        public List<Aircraft> Aircrafts { get; set; }

        public List<TravelClass> TravelClasses { get; set; }

        public List<Country> Countries { get; set; }

        public List<Airport> Airports { get; set; }
        
        public List<FlightStatus> FlightStatuses { get; set; }

        public List<Flight> Flights { get; set; }

        public List<Seat> Seats { get; set; }

        public List<Category> Categories { get; set; }

        public List<News> News { get; set; }

        public List<User> Users { get; set; }

        public List<Notification> Notifications { get; set; }


        /* ******************* SEED DATAS ******************* */
        private void SeedAircraftManufacturers()
        {
            this.AircraftManufacturers.Add(new AircraftManufacturer() { Name = "Airbus" });
            this.AircraftManufacturers.Add(new AircraftManufacturer() { Name = "Bombardier" });
        }

        private void SeedAircrafts()
        {            
            // Aircraft from Sofia to Madrid and back
            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A310",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            // Aircraft from Sofia to Lisbon and back
            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A320",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            // Aircraft from Varna to Berlin and back
            this.Aircrafts.Add(new Aircraft()
            {
                Model = "A330",
                AircraftManufacturer = this.AircraftManufacturers[0]
            });

            // Aircraft from Varna to Berlin and back
            this.Aircrafts.Add(new Aircraft()
            {
                Model = "ERJ140",
                AircraftManufacturer = this.AircraftManufacturers[1]
            });
        }

        private void SeedTravelClasses()
        {
            // TravelClasses from Sofia to Madrid
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 50.99m,
                Flight = this.Flights[0]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 90m,
                PriorityBoarding = true,
                Flight = this.Flights[0]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 105.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[0]
            });



            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 50.99m,
                Flight = this.Flights[1]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 90m,
                PriorityBoarding = true,
                Flight = this.Flights[1]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 105.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[1]
            });


            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 50.99m,
                Flight = this.Flights[2]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 90m,
                PriorityBoarding = true,
                Flight = this.Flights[2]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 105.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[2]
            });

            // TravelClass from Madrid to Sofia
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 50.99m,
                Flight = this.Flights[3]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 90m,
                PriorityBoarding = true,
                Flight = this.Flights[3]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 105.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[3]
            });

            // TravelClass from Sofia to Lisbon and back
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 42.99m,
                Flight = this.Flights[4]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 102.99m,
                PriorityBoarding = true,
                Flight = this.Flights[4]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 112.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[4]
            });

            // TravelClasses from Varna to Berlin
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Coffee or Tea",
                Price = 60m,
                Flight = this.Flights[5]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 180m,
                PriorityBoarding = true,
                Flight = this.Flights[5]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 190m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[5]
            });

            // TravelClasses from London to Paris
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Economy,
                Meal = "Snack",
                Price = 20.99m,
                Flight = this.Flights[6]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.Business,
                Meal = "Menu",
                Price = 70m,
                PriorityBoarding = true,
                Flight = this.Flights[6]
            });
            this.TravelClasses.Add(new TravelClass()
            {
                Type = TravelClassType.First,
                Meal = "Menu",
                Price = 75.99m,
                PriorityBoarding = true,
                EarnMiles = true,
                Flight = this.Flights[6]
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

        private void SeedFlights()
        {
            // Sofia - Madrid
            this.AddFlight(this.Airports[0], this.Airports[5], new DateTime(2017, 2, 18, 11, 30, 0),
                new DateTime(2017, 2, 18, 16, 30, 0), this.FlightStatuses[0], this.Aircrafts[0]);

            // Sofia - Madrid
            this.AddFlight(this.Airports[0], this.Airports[5], new DateTime(2017, 1, 18, 20, 30, 0),
               new DateTime(2017, 1, 18, 23, 30, 0), this.FlightStatuses[0], this.Aircrafts[0]);

            // Sofia - Madrid
            this.AddFlight(this.Airports[0], this.Airports[5], new DateTime(2017, 2, 18, 20, 30, 0),
                 new DateTime(2017, 2, 18, 23, 30, 0), this.FlightStatuses[1], this.Aircrafts[0]);

            // Madrid - Sofia
            this.AddFlight(this.Airports[5], this.Airports[0], new DateTime(2017, 2, 2, 20, 30, 0),
                 new DateTime(2017, 2, 2, 23, 30, 0), this.FlightStatuses[1], this.Aircrafts[0]);

            // Sofia - Lisbon
            this.AddFlight(this.Airports[0], this.Airports[2], new DateTime(2017, 2, 18, 10, 30, 0),
                new DateTime(2017, 2, 14, 20, 30, 0), this.FlightStatuses[0], this.Aircrafts[1]);

            // Varna - Berlin
            this.AddFlight(this.Airports[1], this.Airports[12], new DateTime(2016, 12, 25, 18, 24, 0),
                new DateTime(2016, 12, 25, 21, 55, 0), this.FlightStatuses[3], this.Aircrafts[2]);

            // London - Paris
            this.AddFlight(this.Airports[14], this.Airports[16], new DateTime(2017, 3, 3, 10, 0, 0),
                new DateTime(2017, 3, 3, 12, 0, 0), this.FlightStatuses[7], this.Aircrafts[3]);
        }

        public void SeedSeats()
        {
            this.AddSeatsToFlight(this.Flights[0]);
            this.AddSeatsToFlight(this.Flights[1]);
            this.AddSeatsToFlight(this.Flights[2]);
            this.AddSeatsToFlight(this.Flights[3]);
            this.AddSeatsToFlight(this.Flights[4]);
            this.AddSeatsToFlight(this.Flights[5]);
            this.AddSeatsToFlight(this.Flights[6]);
        }

        private void AddFlight(Airport fromAirport, Airport toAirport, DateTime departure, DateTime arrival,
            FlightStatus flightStatus, Aircraft aircraft)
        {
            this.Flights.Add(new Flight()
            {
                Number = new FlightNumber(this.context).GetUniqueFlightNumber(),
                DepartureAirport = fromAirport,
                ArrivalAirport = toAirport,
                Departure = departure,
                Arrival = arrival,
                FlightStatus = flightStatus,
                Aircraft = aircraft
            });
        }

        private void AddSeatsToFlight(Flight flight)
        {
            if (flight == null)
            {
                return;
            }

            TravelClass firstClass = this.GetTravelClassFromFlight(flight, TravelClassType.First);
            TravelClass businessClass = this.GetTravelClassFromFlight(flight, TravelClassType.Business);
            TravelClass economyClass = this.GetTravelClassFromFlight(flight, TravelClassType.Economy);

            int travelClassId = 0;
            int numberOfRows = 30;

            // 2 rows for First Class and Business Class and 26 rows for Economy Class
            for (int row = 1; row <= numberOfRows; row++)
            {
                if (row <= FIRST_CLASS_ROWS && firstClass != null)
                {
                    travelClassId = firstClass.Id;
                }
                else if (row > FIRST_CLASS_ROWS && row <= BUSINESS_CLASS_ROWS && businessClass != null)
                {
                    travelClassId = businessClass.Id;
                }
                else if (row > BUSINESS_CLASS_ROWS && economyClass != null)
                {
                    travelClassId = economyClass.Id;
                }

                this.Seats.Add(new Seat() { Number = "A", Row = row, TravelClassId = travelClassId });
                this.Seats.Add(new Seat() { Number = "B", Row = row, TravelClassId = travelClassId });
                this.Seats.Add(new Seat() { Number = "C", Row = row, TravelClassId = travelClassId });
                this.Seats.Add(new Seat() { Number = "D", Row = row, TravelClassId = travelClassId });
                this.Seats.Add(new Seat() { Number = "E", Row = row, TravelClassId = travelClassId });
                this.Seats.Add(new Seat() { Number = "F", Row = row, TravelClassId = travelClassId });
            }
        }

        private TravelClass GetTravelClassFromFlight(Flight flight, TravelClassType type)
        {
            if (flight == null)
            {
                return null;
            }

            return flight.TravelClasses
                .Where(t => t.Type == type)
                .FirstOrDefault();
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
    }
}
