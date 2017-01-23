namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Services.Contracts;
    using Ninject;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
     
    public partial class PreviousTrips : Page
    {
        [Inject]
        public IBookingsServices BookingsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

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