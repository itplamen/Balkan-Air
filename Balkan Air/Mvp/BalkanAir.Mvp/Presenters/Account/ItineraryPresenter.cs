namespace BalkanAir.Mvp.Presenters.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using Data.Models;
    using EventArgs.Account;
    using Services.Data.Contracts;
    using ViewContracts.Account;

    public class ItineraryPresenter : Presenter<IItineraryView>
    {
        private readonly IBookingsServices bookingsServices;
        private readonly ITravelClassesServices travelClassesServices;

        public ItineraryPresenter(
            IItineraryView view, 
            IBookingsServices bookingsServices, 
            ITravelClassesServices travelClassesServices) 
            : base(view)
        {
            if (bookingsServices == null)
            {
                throw new ArgumentNullException(nameof(IBookingsServices));
            }

            if (travelClassesServices == null)
            {
                throw new ArgumentNullException(nameof(ITravelClassesServices));
            }

            this.bookingsServices = bookingsServices;
            this.travelClassesServices = travelClassesServices;

            this.View.OnItinerariesGetItem += this.View_OnItinerariesGetItem;
            this.View.OnCabinBagsInfoShow += this.View_OnCabinBagsInfoShow;
            this.View.OnCheckedInBagsInfoShow += this.View_OnCheckedInBagsInfoShow;
            this.View.OnEquipmentBagsInfoShow += this.View_OnEquipmentBagsInfoShow;
            this.View.OnTravelClassInfoShow += this.View_OnTravelClassInfoShow;
        }

        private void View_OnItinerariesGetItem(object sender, ItineraryEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(ItineraryEventArgs));
            }

            this.View.Model.Booking = this.bookingsServices.GetAll()
                .FirstOrDefault(b => b.UserId == e.UserId && b.ConfirmationCode.Trim().ToLower() == e.Number.ToLower() &&
                                     b.User.UserSettings.LastName.Trim().ToLower() == e.Passenger.ToLower());
        }

        private void View_OnCabinBagsInfoShow(object sender, ItineraryEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(ItineraryEventArgs));
            }

            var cabinBags = this.GetBaggages(e.BookingId, BaggageType.Cabin);

            if (cabinBags.Count == 1)
            {
                this.View.Model.CabinBagsInfo = cabinBags.Count + " x cabin bag " + cabinBags[0].Size;
            }
            else
            {
                this.View.Model.CabinBagsInfo = cabinBags.Count + " x cabin bags " + cabinBags[0].Size;
            }
        }

        private void View_OnCheckedInBagsInfoShow(object sender, ItineraryEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(ItineraryEventArgs));
            }

            var checkedInBags = this.GetBaggages(e.BookingId, BaggageType.CheckedIn);

            if (checkedInBags == null || checkedInBags.Count == 0)
            {
                this.View.Model.CheckedInBagsInfo = "NO CHECKED-IN BAGS!";
            }
            else if (checkedInBags.Count == 1)
            {
                this.View.Model.CheckedInBagsInfo = checkedInBags.Count + " x checked-in bag " + checkedInBags[0].MaxKilograms + " KG";
            }
            else
            {
                this.View.Model.CheckedInBagsInfo = checkedInBags.Count + " x checked-in bags " + checkedInBags[0].MaxKilograms + " KG";
            }
        }

        private void View_OnEquipmentBagsInfoShow(object sender, ItineraryEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(ItineraryEventArgs));
            }

            var equiptment = this.GetBaggages(e.BookingId, e.BaggageType);
            var typeAsString = this.GetEqyipmentTypeAsString(e.BaggageType);

            if (equiptment == null || equiptment.Count == 0)
            {
                this.View.Model.EquipmentBagsInfo = "NO " + typeAsString.ToUpper() + "!";
            }
            else
            {
                this.View.Model.EquipmentBagsInfo = equiptment.Count + " x " + typeAsString + " allowed";
            }
        }

        private void View_OnTravelClassInfoShow(object sender, ItineraryEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(ItineraryEventArgs));
            }

            var booking = this.bookingsServices.GetBooking(e.BookingId);

            if (booking == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var travelClass = this.travelClassesServices.GetTravelClass(booking.TravelClassId);

            this.View.Model.TravelClassInfo = travelClass.Type.ToString() + " Class ";
        }

        private IList<Baggage> GetBaggages(int bookingId, BaggageType type)
        {
            if (bookingId <= 0)
            {
                throw new IndexOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var booking = this.bookingsServices.GetBooking(bookingId);

            if (booking == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var bags = booking.Baggage
                .Where(b => b.Type == type)
                .ToList();

            if (type == BaggageType.Cabin && bags.Count == 0)
            {
                throw new ArgumentNullException(ErrorMessages.NULL_CABIN_BAGS);
            }

            return bags;
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
                throw new ArgumentException(ErrorMessages.INVALID_BAGGAGE_EQUIPMENT_TYPE);
            }

            string typeAsString = type.ToString().ToLower();

            int startIndex = typeAsString.IndexOf("equipment");
            string intervalToInsert = " ";

            return typeAsString.Insert(startIndex, intervalToInsert);
        }
    }
}
