namespace BalkanAir.Services.Data.Tests.TestObjects
{
    using BalkanAir.Data.Models;

    public static class TestObjectFactory
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
    }
}
