namespace BalkanAir.Common
{
    public static class ErrorMessages
    {
        // Common
        public const string INVALID_ID = "Id cannot be less or equal to zero!";
        public const string ENTITY_CANNOT_BE_NULL = "Entity cannot be null!";
        public const string MODEL_ERROR_KEY = "key";
        public const string MODEL_ERROR_MESSAGE = "Item with id {0} was not found";
        public const string NULL_OR_EMPTY_ENTITY_NAME = "Name cannot be null or empty!";

        // Airports and countries
        public const string ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY = "Abbreviation cannot be null or empty!";

        // Categories
        public const string INVALID_CATEGORY_NAME = "Invalid category name!";

        // Flight
        public const string NULL_OR_EMPTY_FLIGHT_NUMBER = "Flight number cannot be null or empty!";
        public const string NULL_OR_EMPTY_FLIGHT_STATUS = "Flight status cannot be null or empty!";

        // News
        public const string INVALID_COUNT_VALUE = "Count cannot be zero or negative!";

        // UserNotifications
        public const string NULL_OR_EMPTY_LIST_OF_USERS = "Cannot send notification to null or empty list of users!";

        // Users
        public const string NULL_OR_EMPTY_EMAIL = "Email cannot be null or empty!";
        public const string INVALID_IMAGE_TO_UPLOAD = "Invalid image to upload!";

        // Users, Roles
        public const string NULL_OR_EMPTY_ID = "ID cannot be null or emtpy!";
    }
}
