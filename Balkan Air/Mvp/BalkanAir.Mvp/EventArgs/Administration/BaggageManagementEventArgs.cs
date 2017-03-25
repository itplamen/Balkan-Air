namespace BalkanAir.Mvp.EventArgs.Administration
{
    using System;

    using Data.Models;

    public class BaggageManagementEventArgs : EventArgs
    {
        public int Id { get; set; }

        public BaggageType Type { get; set; }

        public int MaxKilograms { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int BookingId { get; set; }
    }
}
