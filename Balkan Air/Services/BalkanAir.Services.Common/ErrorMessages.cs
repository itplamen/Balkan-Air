namespace BalkanAir.Services.Common
{
    public static class ErrorMessages
    {
        // Common
        public const string INVALID_ID = "Id cannot be less or equal to zero!";
        public const string ENTITY_CANNOT_BE_NULL = "Entity cannot be null!";

        // Airports and countries
        public const string ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY = "Abbreviation cannot be null or empty!";

        // Categories
        public const string CATEGORY_NAME_CANNOT_BE_NULL_OR_EMPTY = "Category name cannot be null or empty!";
        public const string INVALID_CATEGORY_NAME = "Invalid category name!";

        // Flight
        public const string INVALID_FLIGHT_NUMBER = "Flight number cannot be null or empty!";
        public const string INVALID_FLIGHT_STATUS = "Flight status cannot be null or empty!";

        // UserNotifications
        public const string NULL_OR_EMPTY_LIST_OF_USERS = "Cannot send notification to null or empty list of users!";

        // Users
        public const string NULL_OR_EMPTY_EMAIL = "Email cannot be null or empty!";
        public const string INVALID_IMAGE_TO_UPLOAD = "Invalid image to upload!";

        // Users, Roles
        public const string NULL_OR_EMPTY_ID = "ID cannot be null or emtpy!";
    }
}
