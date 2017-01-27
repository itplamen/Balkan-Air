using BalkanAir.Data.Models;

namespace BalkanAir.Services.Data.Tests.TestObjects
{
    public static class TestObjectFactory
    {
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
