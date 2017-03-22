namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(FlightStatusesManagementPresenter))]
    public partial class FlightStatusesManagement : MvpPage<FlightStatusesManagementViewModel>, IFlightStatusesManagementView
    {
        public event EventHandler OnFlightStatusesGetData;
        public event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesUpdateItem;
        public event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesDeleteItem;
        public event EventHandler<FlightStatusesManagementEventArgs> OnFlightStatusesAddItem;

        public IQueryable<FlightStatus> FlightStatusesGridView_GetData()
        {
            this.OnFlightStatusesGetData?.Invoke(null, null);

            return this.Model.FlightStatuses;
        }

        public void FlightStatusesGridView_UpdateItem(int id)
        {
            this.OnFlightStatusesUpdateItem?.Invoke(null, new FlightStatusesManagementEventArgs() { Id = id });
        }

        public void FlightStatusesGridView_DeleteItem(int id)
        {
            this.OnFlightStatusesDeleteItem?.Invoke(null, new FlightStatusesManagementEventArgs() { Id = id });
        }

        protected void CreateFlightStatustBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                this.OnFlightStatusesGetData?.Invoke(sender, e);

                var doesFlightStatusExist = this.Model.FlightStatuses
                   .Any(fs => fs.Name.ToLower() == this.FlightStatusNameTextBox.Text.ToLower());

                if (doesFlightStatusExist)
                {
                    this.FlightStatusNameTextBox.BorderColor = Color.Red;
                    return;
                }

                var flightStatusEventArgs = new FlightStatusesManagementEventArgs() { Name = this.FlightStatusNameTextBox.Text };
                this.OnFlightStatusesAddItem?.Invoke(sender, flightStatusEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedFLightStatusIdLiteral.Text = flightStatusEventArgs.Id.ToString();
                
                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.FlightStatusNameTextBox.Text = string.Empty;
            this.FlightStatusNameTextBox.BorderColor = Color.Empty;
        }
    }
}