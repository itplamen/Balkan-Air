namespace BalkanAir.Data.Helper
{
    public interface INumberGenerator
    {
        string GetUniqueFlightNumber();

        string GetUniqueBookingConfirmationCode();
    }
}
