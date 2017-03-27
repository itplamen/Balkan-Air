namespace BalkanAir.Mvp.EventArgs.Account
{
    using System;

    using Data.Models;

    public class ItineraryEventArgs : EventArgs
    {
        public string UserId { get; set; }

        public string Number { get; set; }

        public string Passenger { get; set; }

        public int BookingId { get; set; }

        public BaggageType BaggageType { get; set; }
    }
}
