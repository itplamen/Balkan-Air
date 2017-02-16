namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageSeats : Page
    { 
        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        public IQueryable<Seat> ManageSeatsGridView_GetData()
        {
            return this.SeatsServices.GetAll();
        }

        public void ManageSeatsGridView_UpdateItem(int id)
        {
            var seat = this.SeatsServices.GetSeat(id);
            
            if (seat == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(seat);
            if (ModelState.IsValid)
            {
                this.SeatsServices.UpdateSeat(id, seat);
            }
        }

        public void ManageSeatsGridView_DeleteItem(int id)
        {
            this.SeatsServices.DeleteSeat(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}