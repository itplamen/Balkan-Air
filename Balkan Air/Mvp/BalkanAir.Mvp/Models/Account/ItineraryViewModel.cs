namespace BalkanAir.Mvp.Models.Account
{
    using Data.Models;

    public class ItineraryViewModel
    {
        public Booking Booking { get; set; }

        public string CabinBagsInfo { get; set; }

        public string CheckedInBagsInfo { get; set; }

        public string EquipmentBagsInfo { get; set; }

        public string TravelClassInfo { get; set; }
    }
}
