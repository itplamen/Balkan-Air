namespace BalkanAir.Data.Helper
{
    using System;
    using System.Linq;

    public class Generator
    {
        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int NUMBER_LENGHT = 6;

        private static Random random = new Random();

        private IBalkanAirDbContext context;

        public Generator(IBalkanAirDbContext context)
        {
            this.context = context;
        }

        public string GetUniqueFlightNumber()
        {
            string flightNumber = string.Empty;

            while (true)
            {
                flightNumber = GetRandomStringNumber(NUMBER_LENGHT);

                if (!string.IsNullOrEmpty(flightNumber) && !this.DoesFlightNumberExist(flightNumber))
                {
                    break;
                }
            }

            return flightNumber;
        }

        public string GetUniqueBookingConfirmationCode()
        {
            string code = string.Empty;

            while (true)
            {
                code = GetRandomStringNumber(NUMBER_LENGHT);

                if (!string.IsNullOrEmpty(code) && !this.DoesBookingConfirmationCodeExist(code))
                {
                    break;
                }
            }

            return code;
        }

        private static string GetRandomStringNumber(int length)
        {
            return new string(Enumerable.Repeat(CHARACTERS, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        private bool DoesFlightNumberExist(string flightNumber)
        {
            return this.context.Flights.Any(f => f.Number.ToLower().Equals(flightNumber.ToLower()));
        }

        private bool DoesBookingConfirmationCodeExist(string confirmationCode)
        {
            return this.context.Booking.Any(b => b.ConfirmationCode.ToLower().Equals(confirmationCode.ToLower()));
        }
    }
}
