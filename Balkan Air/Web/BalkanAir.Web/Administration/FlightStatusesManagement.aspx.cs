namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class FlightStatusesManagement : Page
    {
        [Inject]
        public IFlightStatusesServices FlightStatusesServices { get; set; }

        public IQueryable<FlightStatus> FlightStatusesGridView_GetData()
        {
            return this.FlightStatusesServices.GetAll()
                .OrderBy(fs => fs.Name);
        }

        public void FlightStatusesGridView_UpdateItem(int id)
        {
            var flightStatus = this.FlightStatusesServices.GetFlightStatus(id);

            if (flightStatus == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(flightStatus);
            if (ModelState.IsValid)
            {
                this.FlightStatusesServices.UpdateFlightStatus(id, flightStatus);
            }
        }

        public void FlightStatusesGridView_DeleteItem(int id)
        {
            this.FlightStatusesServices.DeleteFlightStatus(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateFlightStatustBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var doesFlightStatusExist = this.FlightStatusesServices.GetAll()
                   .Any(fs => fs.Name.ToLower() == this.FlightStatusNameTextBox.Text.ToLower());

                if (doesFlightStatusExist)
                {
                    this.FlightStatusNameTextBox.BorderColor = Color.Red;
                    return;
                }

                var flightStatus = new FlightStatus() { Name = this.FlightStatusNameTextBox.Text };

                this.FlightStatusesServices.AddFlightStatus(flightStatus);
                this.FlightStatusNameTextBox.BorderColor = Color.Empty;
            }
        }
    }
}