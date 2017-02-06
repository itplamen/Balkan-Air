namespace BalkanAir.Data.Common
{
    public static class ValidationConstants
    {
        // Baggage
        public const int CABIN_BAG_MAX_KILOGRAMS = 10;
        public const int CHECKED_IN_BAG_MAX_KOLOGRAMS = 32;
        public const int CABIN_BAG_FREE_PRICE = 0;
        public const int CHECKED_IN_BAG_PRICE_EUROS = 30;

        // TravelClass
        public const int ECONOMY_CLASS_CABIN_BAGS = 1;
        public const int ECONOMY_CLASS_CHECKED_IN_BAGS = 1;
        public const int FIRST_CLASS_CABIN_BAGS = 1;
        public const int FIRST_CLASS_CHECKED_IN_BAGS = 2;
        public const int BUSINESS_CLASS_CABIN_BAGS = 2;
        public const int BUSSINESS_CLASS_CHECKED_IN_BAGS = 2;

        // Aircraft
        public const int AIRCRAFT_NAME_MIN_LENGTH = 2;
        public const int AIRCRAFT_NAME_MAX_LENGTH = 30;
        public const int AIRCRAFT_MIN_SEATS = 2;
        public const int AIRCRAFT_MAX_SEATS = 180;

        // Flight
        public const int FLIGHT_NUMBER_LENGTH = 6;
        public const int FLIGHT_DESTINATION_MIN_LENGTH = 2;
        public const int FLIGHT_DESTINATION_MAX_LENGTH = 50;
        public const int FLIGHT_TERMINAL_MIN_LENGHT = 1;
        public const int FLIGHT_TERMINAL_MAX_LENGHT = 20;
        public const int FLIGHT_MIN_BAGGAGE_KG = 0;
        public const int FLIGHT_MAX_CHECK_IN_BAGGAGE_KG = 50;
        public const int FLIGHT_MAX_CABIN_BAGGAGE_KG = 10;

        // Passenger
        public const int PASSENGER_NAME_MIN_LENGTH = 2;
        public const int PASSENGER_NAME_MAX_LENGTH = 50;
        public const string PASSENGER_MIN_DATE_OF_BIRTH = "1-Jan-1916";
        public const int PASSENGER_IDENTITY_DOCUMENT_ID_MIN_LENGTH = 5;
        public const int PASSENGER_IDENTITY_DOCUMENT_ID_MAX_LENGTH = 20;

        // Comments
        public const int COMMENT_CONTENT_LENGHT = 250;

        // Other
        public const string ADMINISTRATOR_ROLE = "Administrator";

    }
}
