namespace BalkanAir.Web.Account
{
    using System.Linq;
    using System.Web;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;

    using Auth;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class PreviousTrips : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        public IQueryable<Booking> PreviousTripsListView_GetData()
        {
            var user = this.GetCurrentUser();

            return this.BookingsServices.GetAll()
                .Where(b => b.UserId == user.Id && !b.IsDeleted);
        }

        private User GetCurrentUser()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return manager.FindById(this.User.Identity.GetUserId());
        }
    }
}