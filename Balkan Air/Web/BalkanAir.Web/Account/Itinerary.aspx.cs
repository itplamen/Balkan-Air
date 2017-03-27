namespace BalkanAir.Web.Account
{
    using System;
    using System.Web;
    using System.Web.ModelBinding;
    
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Auth;
    using Common;
    using Data.Models;
    using Mvp.EventArgs.Account;
    using Mvp.Models.Account;
    using Mvp.Presenters.Account;
    using Mvp.ViewContracts.Account;

    [PresenterBinding(typeof(ItineraryPresenter))]
    public partial class Itinerary : MvpPage<ItineraryViewModel>, IItineraryView
    {
        public event EventHandler<ItineraryEventArgs> OnItinerariesGetItem;
        public event EventHandler<ItineraryEventArgs> OnCabinBagsInfoShow;
        public event EventHandler<ItineraryEventArgs> OnCheckedInBagsInfoShow;
        public event EventHandler<ItineraryEventArgs> OnEquipmentBagsInfoShow;
        public event EventHandler<ItineraryEventArgs> OnTravelClassInfoShow;

        private ApplicationUserManager Manager
        {
            get{ return Context.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private User CurrentUser
        {
            get { return this.Manager.FindById(this.Context.User.Identity.GetUserId()); }
        }

        public Data.Models.Booking ViewItineraryFormView_GetItem([QueryString] string number, [QueryString]string passenger)
        {
            this.OnItinerariesGetItem?.Invoke(null, new ItineraryEventArgs()
            {
                UserId = this.CurrentUser.Id,
                Number = number,
                Passenger = passenger
            });

            if (this.Model.Booking == null)
            {
                this.Response.Redirect(Pages.ACCOUNT);
            }

            return this.Model.Booking;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var itineraryNumber = this.Request.QueryString["number"];
            var passenger = this.Request.QueryString["passenger"];

            if (itineraryNumber == null || passenger == null)
            {
                this.Response.Redirect(Pages.ACCOUNT);
            }
        }

        protected string ShowCabinBags(int bookingId)
        {
            this.OnCabinBagsInfoShow?.Invoke(null, new ItineraryEventArgs()
            {
                BookingId = bookingId
            });

            return this.Model.CabinBagsInfo;
        }

        protected string ShowCheckedInBags(int bookingId)
        {
            this.OnCheckedInBagsInfoShow?.Invoke(null, new ItineraryEventArgs()
            {
                BookingId = bookingId
            });

            return this.Model.CheckedInBagsInfo;
        }

        protected string ShowEquipmentBags(int bookingId, BaggageType type)
        {
            this.OnEquipmentBagsInfoShow?.Invoke(null, new ItineraryEventArgs()
            {
                BookingId = bookingId,
                BaggageType = type
            });

            return this.Model.EquipmentBagsInfo;
        }

        protected string ShowTravelClass(int bookingId)
        {
            this.OnTravelClassInfoShow?.Invoke(null, new ItineraryEventArgs()
            {
                BookingId = bookingId
            });

            return this.Model.TravelClassInfo;
        }
    }
}