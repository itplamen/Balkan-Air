namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class BaggageManagementPresenter : Presenter<IBaggageManagementView>
    {
        private readonly IBaggageServices baggageServices;
        private readonly IBookingsServices bookingsServices;

        public BaggageManagementPresenter(IBaggageManagementView view, IBaggageServices baggageServices, 
            IBookingsServices bookingsServices) 
            : base(view)
        {
            if (baggageServices == null)
            {
                throw new ArgumentNullException(nameof(IBaggageServices));
            }

            if (bookingsServices == null)
            {
                throw new ArgumentNullException(nameof(IBookingsServices));
            }

            this.baggageServices = baggageServices;
            this.bookingsServices = bookingsServices;

            this.View.OnBaggageGetData += this.View_OnBaggageGetData;
            this.View.OnBaggageUpdateItem += this.View_OnBaggageUpdateItem;
            this.View.OnBaggageDeleteItem += this.View_OnBaggageDeleteItem;
            this.View.OnBaggageAddItem += this.View_OnBaggageAddItem;
            this.View.OnBookingsGetData += this.View_OnBookingsGetData;
        }

        private void View_OnBaggageGetData(object sender, EventArgs e)
        {
            this.View.Model.Baggage = this.baggageServices.GetAll();
        }

        private void View_OnBaggageUpdateItem(object sender, BaggageManagementEventArgs e)
        {
            var baggage = this.baggageServices.GetBaggage(e.Id);

            if (baggage == null)
            {
                this.View.ModelState.AddModelError("", String.Format("Item with id {0} was not found", e.Id));
                return;
            }

            this.View.TryUpdateModel(baggage);

            if (this.View.ModelState.IsValid)
            {
                this.baggageServices.UpdateBaggage(e.Id, baggage);
            }
        }

        private void View_OnBaggageDeleteItem(object sender, BaggageManagementEventArgs e)
        {
            this.baggageServices.DeleteBaggage(e.Id);
        }

        private void View_OnBaggageAddItem(object sender, BaggageManagementEventArgs e)
        {
            var bag = new Baggage()
            {
                Type = e.Type,
                MaxKilograms = e.MaxKilograms,
                Size = e.Size,
                Price = e.Price,
                BookingId = e.BookingId
            };

            e.Id = this.baggageServices.AddBaggage(bag);
        }

        private void View_OnBookingsGetData(object sender, EventArgs e)
        {
            this.View.Model.Bookings = this.bookingsServices.GetAll()
                .Where(b => !b.IsDeleted)
                .Select(b => new
                {
                    Id = b.Id,
                    BookingInfo = "Id: " + b.Id + ", " + b.User.UserSettings.FirstName + " " + b.User.UserSettings.LastName
                });
        }
    }
}
