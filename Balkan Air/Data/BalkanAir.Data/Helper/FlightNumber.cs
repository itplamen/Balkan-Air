namespace BalkanAir.Data.Helper
{
    using System;
    using System.Linq;

    public class FlightNumber
    {
        private const string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();

        private IBalkanAirDbContext context;

        public FlightNumber(IBalkanAirDbContext context)
        {
            this.context = context;
        }

        public string GetUniqueFlightNumber(int flightNumberLength = 6)
        {
            string flightNumber = string.Empty;

            while (true)
            {
                flightNumber = GetRandomStringNumber(flightNumberLength);

                if (!string.IsNullOrEmpty(flightNumber) && !this.DoesFlightNumberExist(flightNumber))
                {
                    break;
                }
            }

            return flightNumber;
        }

        private static string GetRandomStringNumber(int length)
        {
            return new string(Enumerable.Repeat(CHARACTERS, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        private bool DoesFlightNumberExist(string flightNumber)
        {
            return this.context.Flights.Any(flight => flight.Number.Equals(flightNumber));
        }
    }
}
