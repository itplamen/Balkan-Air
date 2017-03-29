namespace BalkanAir.Services.Data.Tests.TestObjects
{
    using System;

    using BalkanAir.Data.Models;

    public static class TestObjectFactoryRepositories
    {
        public static InMemoryRepository<AircraftManufacturer> GetAircraftManufacturersRepository(int numberOfManufacturers = 25)
        {
            var manufacturers = new InMemoryRepository<AircraftManufacturer>();

            for (int i = 0; i < numberOfManufacturers; i++)
            {
                manufacturers.Add(new AircraftManufacturer()
                {
                    Id = i,
                    Name = "Test Manufacturer " + i
                });
            }

            return manufacturers;
        }

        public static InMemoryRepository<Aircraft> GetAircraftsRepository(int numberOfAircrafts = 25)
        {
            var aircrafts = new InMemoryRepository<Aircraft>();

            for (int i = 0; i < numberOfAircrafts; i++)
            {
                aircrafts.Add(new Aircraft()
                {
                    Id = i,
                    Model = "Test Aircraft " + i,
                    TotalSeats = 1,
                    AircraftManufacturerId = 1
                });
            }

            return aircrafts;
        }

        public static InMemoryRepository<Airport> GetAirportsRepository(int numberOfAirprots = 25)
        {
            var airports = new InMemoryRepository<Airport>();

            for (int i = 0; i < numberOfAirprots; i++)
            {
                airports.Add(new Airport()
                {
                    Id = i,
                    Name = "Test Airprot " + i,
                    Abbreviation = "T" + i,
                    CountryId = 1
                });
            }

            return airports;
        }

        public static InMemoryRepository<Baggage> GetBaggageRepository(int numberOfBags = 25)
        {
            var bags = new InMemoryRepository<Baggage>();

            for (int i = 0; i < numberOfBags; i++)
            {
                bags.Add(new Baggage()
                {
                    Id = i,
                    Type = BaggageType.Cabin,
                    Price = 1m,
                    Size = "Test Size " + i,
                    BookingId = 1
                });
            }

            return bags;
        }

        public static InMemoryRepository<Category> GetCategoriesRepository(int numberOfCategories = 25)
        {
            var categories = new InMemoryRepository<Category>();

            for (int i = 0; i < numberOfCategories; i++)
            {
                categories.Add(new Category()
                {
                    Id = i,
                    Name = "Test Category Name"
                });
            }

            return categories;
        }

        public static InMemoryRepository<Country> GetCountriesRepository(int numberOfCountries = 25)
        {
            var countries = new InMemoryRepository<Country>();

            for (int i = 0; i < numberOfCountries; i++)
            {
                countries.Add(new Country()
                {
                    Id = i,
                    Name = "Test Country " + i,
                    Abbreviation = i.ToString()
                });
            }

            return countries;
        }

        public static InMemoryRepository<Fare> GetFaresRepository(int numberOfFares = 25)
        {
            var fares = new InMemoryRepository<Fare>();

            for (int i = 0; i < numberOfFares; i++)
            {
                fares.Add(new Fare()
                {
                    Id = i,
                    Price = 1,
                    RouteId = i
                });
            }

            return fares;
        }

        public static InMemoryRepository<LegInstance> GetLegInstancesRepository(int numberOfLegInstances = 25)
        {
            var legInstances = new InMemoryRepository<LegInstance>();

            for (int i = 0; i < numberOfLegInstances; i++)
            {
                legInstances.Add(new LegInstance()
                {
                    Id = i,
                    DepartureDateTime = new DateTime(2017, 1, 1),
                    ArrivalDateTime = new DateTime(2017, 1, 1),
                    Price = 1,
                    FlightLegId = 1
                });
            }

            return legInstances;
        }

        public static InMemoryRepository<News> GetNewsRepository(int numberOfNews = 25)
        {
            var news = new InMemoryRepository<News>();

            for (int i = 0; i < numberOfNews; i++)
            {
                news.Add(new News()
                {
                    Id = i,
                    Title = "Test News Title " + i,
                    Content = "Test News Content " + i,
                    DateCreated = new DateTime(2017, 1, 1, 1, 1, 1),
                    CategoryId = i
                });
            }

            return news;
        }

        public static InMemoryRepository<Route> GetRoutesRepository(int numberOfRoutes = 25)
        {
            var routes = new InMemoryRepository<Route>();

            for (int i = 0; i < numberOfRoutes; i++)
            {
                routes.Add(new Route()
                {
                    Id = i,
                    OriginId = i,
                    DestinationId = i
                });
            }

            return routes;
        }

        public static InMemoryRepository<UserNotification> GetUserNotificationsRepository(int numberOfNotifications = 25)
        {
            var userNotifications = new InMemoryRepository<UserNotification>();

            for (int i = 0; i < numberOfNotifications; i++)
            {
                userNotifications.Add(new UserNotification()
                {
                    Id = i,
                    DateReceived = DateTime.Now,
                    UserId = "User Id Test",
                    NotificationId = 1
                });
            }

            return userNotifications;
        }

        public static InMemoryRepository<User> GetUsersRepository(int numberOfUsers = 25)
        {
            var users = new InMemoryRepository<User>();

            for (int i = 0; i < numberOfUsers; i++)
            {
                users.Add(new User()
                {
                    Id = "user id " + i,
                    Email = i + "_user_email@test.bg",
                    UserName = i + "_user_email@test.bg"
                });
            }

            return users;
        }
    }
}
