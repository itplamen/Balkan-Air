namespace BalkanAir.Tests.Common
{
    using Data.Models;

    public static class Constants
    {
        // All tests
        public const int ENTITY_VALID_ID = 1;

        // Aircraft Manufacturers
        public const string MANUFACTURER_VALID_NAME = "Manufacturer Name Test";

        // Aircrafts
        public const string AIRCRAFT_VALID_MODEL = "Aircraft Model Test";
        public const int AIRCRAFT_TOTAL_SEATS = 1;

        // Airprots
        public const string AIRPROT_VALID_NAME = "Airport Name Test";
        public const string AIRPORT_VALID_ABBREVIATION = "ABC";

        // Categories
        public const string CATEGORY_VALID_NAME = "Category Test";

        // Countries
        public const string COUNTRY_VALID_NAME = "Country Test";
        public const string COUNTRY_VALID_ABBREVIATION = "CT";

        // Fares
        public const int FARE_VALID_PRICE = 1;
    
        // Flights
        public const string FLIGHT_VALID_STATUS = "Test";
        public const string FLIGHT_VALID_NUMBER = "TEST12";

        // Routes
        public const string ROUTE_VALID_ORIGIN_NAME = "Test Origin";
        public const string ROUTE_VALID_ORIGIN_ABBREVIATION = "ABC";
        public const string ROUTE_VALID_DESTINATION_NAME = "Test Destination";
        public const string ROUTE_VALID_DESTINATION_ABBREVIATION = "DEF";

        // Users
        public const Gender USER_VALID_GENDER = Gender.Male;
        public const string USER_VALID_NATIONALITY = "Test Nationality";
    }
}
