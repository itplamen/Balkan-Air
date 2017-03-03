namespace BalkanAir.Web.Common
{
    public static class NativeConstants
    {
        /******************** Session *******************/

        public const string DEPARTURE_AIRPORT_ID = "departureAirprotId";
        public const string DESTINATION_AIRPORT_ID = "destinationAirportId";
        public const string DEPARTURE_DATE = "departureDate";
        public const string ARRIVAL_DATE = "arrivalDate";
        public const string ONE_WAY_ROUTE_BOOKING = "oneWayRouteBooking";
        public const string ONE_WAY_ROUTE_SELECTED_CHECKED_IN_BAG = "oneWayRouteSelectedCheckedInBag";
        public const string ONE_WAY_ROUTE_SELECTED_CABIN_BAG = "oneWayRouteSelectedCabinBag";
        public const string ONE_WAY_ROUTE_SELECT_SEAT = "oneWayRouteSelectSeat";
        public const string RETURN_ROUTE_BOOKING = "returnRouteBooking";
        public const string RETURN_ROUTE_SELECT_SEAT = "returnRouteSelectSeat";
        public const string RETURN_ROUTE_SELECTED_CHECKED_IN_BAG = "returnRouteSelectedCheckedInBag";
        public const string RETURN_ROUTE_SELECTED_CABIN_BAG = "returnRouteSelectedCabinBag";
        public const string SELECTED_FLIGHT_ID = "selectedFlightId";
        public const string SELECTED_TRAVEL_CLASS_ID = "selectedTravelClassId";
        public const string SELECTED_ROW = "selectedRow";
        public const string SELECTED_SEAT_NUMBER = "selectedSeatNumber";


        /******************** Account/Profile *******************/

        public const int NATIONALITY_NOT_SELECTED_INDEX = 0;
        public const string NATIONALITY_NOT_SELECTED_TEXT = "--- Select nationality ---";
        public const int GENDER_NOT_SELECTED_INDEX = 0;
        public const string GENDER_NOT_SELECTED_TEXT = "--- Select gender ---";
    }
}
