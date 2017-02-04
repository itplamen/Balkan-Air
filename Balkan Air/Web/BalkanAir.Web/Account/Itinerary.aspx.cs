namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Common;
    using Services.Data.Contracts;
    using Data.Models;

    public partial class Itinerary : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        public Data.Models.Booking ViewItineraryFormView_GetItem([QueryString] int id, string flight)
        {
            var booking = this.BookingsServices.GetBooking(id);

            if (booking == null)
            {
                this.Response.Redirect(Pages.ACCOUNT);
            }

            return booking;
        }

        protected void Page_Load(object sender, EventArgs e)
        {  
        }

        protected string ShowCabinBags(int bookingId)
        {
            var cabinBags = this.GetBaggages(bookingId, BaggageType.Cabin);

            if (cabinBags == null || cabinBags.Count == 0)
            {
                throw new ArgumentNullException("Cabing bags cannot be null! Passenger is allowed to take at least 1 cabin bag!");
            }
            else if (cabinBags.Count == 1)
            {
                return cabinBags.Count + " x cabin bag " + cabinBags[0].Size;
            }
            else
            {
                return cabinBags.Count + " x cabin bags " + cabinBags[0].Size;
            }
        }

        protected string ShowCheckedInBags(int bookingId)
        {
            var checkedInBags = this.GetBaggages(bookingId, BaggageType.CheckedIn);

            if (checkedInBags == null || checkedInBags.Count == 0)
            {
                return "NO CHECKED-IN BAGS!";
            }

            if (checkedInBags.Count == 1)
            {
                return checkedInBags.Count + " x checked-in bag " + checkedInBags[0].MaxKilograms + " KG";
            }
            else
            {
                return checkedInBags.Count + " x checked-in bags " + checkedInBags[0].MaxKilograms + " KG";
            }
        }

        protected string ShowEquipmentBags(int bookingId, BaggageType type)
        {
            var equiptment = GetBaggages(bookingId, type);
            var typeAsString = this.GetEqyipmentTypeAsString(type);

            if (equiptment == null || equiptment.Count == 0)
            {
                return "NO " + typeAsString.ToUpper() + "!";
            }
            else
            {
                return equiptment.Count + " x " + typeAsString + " allowed";
            }
        }

        protected string ShowTravelClass(int bookingId)
        {
            int travelClassId = this.BookingsServices.GetBooking(bookingId).TravelClassId;
            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            return travelClass.Type.ToString() + " Class ";
        }

        private IList<Baggage> GetBaggages(int bookingId, BaggageType type)
        {
            return this.BookingsServices.GetBooking(bookingId)
                .Baggages
                .Where(b => b.Type == type)
                .ToList();
        }

        /// <summary>
        /// Converts baggage equipment type to string.
        /// </summary>
        /// <param name="type">Baggage type.</param>
        /// <returns>Baggage type as string.</returns>
        /// <example>
        /// BabyEquipment => baby equipment
        /// SportsEquipment => sports equipment
        /// MusicEquipment => music equipment
        /// </example>
        private string GetEqyipmentTypeAsString(BaggageType type)
        {
            if (type != BaggageType.BabyEquipment && type != BaggageType.SportsEquipment 
                && type != BaggageType.MusicEquipment)
            {
                throw new ArgumentException("Invalid baggage type!");
            }

            string typeAsString = type.ToString().ToLower();

            int startIndex = typeAsString.IndexOf("equipment");
            string intervalToInsert = " ";

            return typeAsString.Insert(startIndex, intervalToInsert);
        }
    }
}