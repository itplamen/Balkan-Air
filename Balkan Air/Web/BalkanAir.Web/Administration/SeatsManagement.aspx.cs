namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class SeatsManagement : Page
    {
        [Inject]
        public ILegInstancesServices LegInstancesServices { get; set; }

        [Inject]
        public ISeatsServices SeatsServices { get; set; }

        [Inject]
        public ITravelClassesServices TravelClassesServices { get; set; }

        public IQueryable<Seat> SeatsGridView_GetData()
        {
            return this.SeatsServices.GetAll();
        }

        public void SeatsGridView_UpdateItem(int id)
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

        public void SeatsGridView_DeleteItem(int id)
        {
            this.SeatsServices.DeleteSeat(id);
        }

        public IQueryable<object> TravelClassDropDownList_GetData()
        {
            return this.TravelClassesServices.GetAll()
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.AircraftId)
                .Select(t => new
                {
                    Id = t.Id,
                    TravelClassInfo = "Id:" + t.Id + " " + t.Type.ToString() + " class " + t.NumberOfRows + " rows"
                });
        }

        public IQueryable<object> LegInstanceDropDown_GetData()
        {
            return this.LegInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.DepartureDateTime)
                .ThenBy(l => l.ArrivalDateTime)
                .Select(l => new
                {
                    Id = l.Id,
                    LegInstanceInfo = "Id:" + l.Id + ", " + l.DepartureDateTime + " -> " + l.ArrivalDateTime
                });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }  

        protected string GetTravelClass(int travelClassId)
        {
            var travelClass = this.TravelClassesServices.GetTravelClass(travelClassId);

            if (travelClass == null)
            {
                return "No travel class found!";
            }

            return "Id:" + travelClass.Id + " " + travelClass.Type.ToString() + " class " + travelClass.NumberOfRows + " rows";
        }

        protected void CreateAirportBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var seat = new Seat()
                {
                    Row = int.Parse(this.RowTextBox.Text),
                    Number = this.SeatNumberTextBox.Text.ToUpper(),
                    IsReserved = this.IsSeatReservedCheckBox.Checked,
                    TravelClassId = int.Parse(this.AddTravelClassDropDownList.SelectedItem.Value),
                    LegInstanceId = int.Parse(this.AddLegInstanceDropDown.SelectedItem.Value)
                };

                int id = this.SeatsServices.AddSeat(seat);

                this.SuccessPanel.Visible = true;
                this.AddedSeatIdLiteral.Text = id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.RowTextBox.Text = string.Empty;
            this.SeatNumberTextBox.Text = string.Empty;
            this.IsSeatReservedCheckBox.Checked = false;
            this.AddTravelClassDropDownList.SelectedIndex = 0;
            this.AddLegInstanceDropDown.SelectedIndex = 0;
        }
    }
}