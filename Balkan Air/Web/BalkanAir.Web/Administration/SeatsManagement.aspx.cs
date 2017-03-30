namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.ViewContracts.Administration;
    using Mvp.Presenters.Administration;

    [PresenterBinding(typeof(SeatsManagementPresenter))]
    public partial class SeatsManagement : MvpPage<SeatsManagementViewModel>, ISeatsManagementView
    {
        public event EventHandler OnSeatsGetData;

        public event EventHandler<SeatsManagementEventArgs> OnSeatsUpdateItem;

        public event EventHandler<SeatsManagementEventArgs> OnSeatsDeleteItem;

        public event EventHandler<SeatsManagementEventArgs> OnSeatsAddItem;

        public event EventHandler OnTravelClassesGetData;

        public event EventHandler OnLegInstancesGetData;

        public event EventHandler<SeatsManagementEventArgs> OnTravelClassInfoGetItem;

        public IQueryable<Seat> SeatsGridView_GetData()
        {
            this.OnSeatsGetData?.Invoke(null, null);

            return this.Model.Seats;
        }

        public void SeatsGridView_UpdateItem(int id)
        {
            this.OnSeatsUpdateItem?.Invoke(
                null, 
                new SeatsManagementEventArgs()
                {
                    Id = id,
                    TravelClassId = int.Parse(this.TravelClassIdHiddenField.Value),
                    LegInstanceId = int.Parse(this.LegInstanceIdHiddenField.Value)
                });
        }

        public void SeatsGridView_DeleteItem(int id)
        {
            this.OnSeatsDeleteItem?.Invoke(null, new SeatsManagementEventArgs() { Id = id });
        }

        public IQueryable<object> TravelClassDropDownList_GetData()
        {
            this.OnTravelClassesGetData?.Invoke(null, null);

            return this.Model.TravelClasses;
        }

        public IQueryable<object> LegInstanceDropDown_GetData()
        {
            this.OnLegInstancesGetData?.Invoke(null, null);

            return this.Model.LegInstances;
        }

        protected string GetTravelClass(int travelClassId)
        {
            this.OnTravelClassInfoGetItem?.Invoke(null, new SeatsManagementEventArgs() { TravelClassId = travelClassId });

            return this.Model.TravelClassInfo;
        }

        protected void CreateSeatBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var seatEventArgs = new SeatsManagementEventArgs()
                {
                    Row = int.Parse(this.RowTextBox.Text),
                    Number = this.SeatNumberTextBox.Text.ToUpper(),
                    IsReserved = this.IsSeatReservedCheckBox.Checked,
                    TravelClassId = int.Parse(this.AddTravelClassDropDownList.SelectedItem.Value),
                    LegInstanceId = int.Parse(this.AddLegInstanceDropDown.SelectedItem.Value)
                };

                this.OnSeatsAddItem?.Invoke(sender, seatEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedSeatIdLiteral.Text = seatEventArgs.Id.ToString();

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