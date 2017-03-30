namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(RoutesManagementPresenter))]
    public partial class RoutesManagement : MvpPage<RoutesManagementViewModel>, IRoutesManagementView
    {
        public event EventHandler OnRoutesGetData;
        public event EventHandler<RoutesManagementEventArgs> OnRoutesUpdateItem;
        public event EventHandler<RoutesManagementEventArgs> OnRoutesDeleteItem;
        public event EventHandler<RoutesManagementEventArgs> OnRoutesAddItem;
        public event EventHandler OnAirportsGetData;

        public IQueryable<Route> RoutesGridView_GetData()
        {
            this.OnRoutesGetData?.Invoke(null, null);

            return this.Model.Routes;
        }

        public void RoutesGridView_UpdateItem(int id)
        {
            this.OnRoutesUpdateItem?.Invoke(
                null, 
                new RoutesManagementEventArgs()
                {
                    Id = id,
                    OriginId = int.Parse(this.OriginIdHiddenField.Value),
                    DestinationId = int.Parse(this.DestinationIdHiddenField.Value)
                });
        }

        public void RoutesGridView_DeleteItem(int id)
        {
            this.OnRoutesDeleteItem?.Invoke(null, new RoutesManagementEventArgs() { Id = id });
        }

        public IQueryable<object> AirportsDropDownList_GetData()
        {
            this.OnAirportsGetData?.Invoke(null, null);

            return this.Model.Airports;
        }

        protected void CreateRoutetBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (!this.AreOriginAndDestinationValid())
                {
                    this.InvalidOriginAndDestinationCustomValidator.IsValid = false;
                    return;
                }

                double distance = this.GetValidDistance();

                if (distance == -1)
                {
                    this.InvalidDistanceCustonValidator.IsValid = false;
                    return;
                }

                var routeEventArgs = new RoutesManagementEventArgs()
                {
                    OriginId = int.Parse(this.AddOriginDropDownList.SelectedItem.Value),
                    DestinationId = int.Parse(this.AddDestinationDropDownList.SelectedItem.Value),
                    DistanceInKm = distance
                };

                this.OnRoutesAddItem?.Invoke(null, routeEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedRouteIdLiteral.Text = routeEventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private bool AreOriginAndDestinationValid()
        {
            return this.AddOriginDropDownList.SelectedItem.Value != this.AddDestinationDropDownList.SelectedItem.Value;
        }

        private double GetValidDistance()
        {
            double distance = 0;

            if (this.DistanceTextBox.Text != string.Empty)
            {
                bool isDistanceValid = double.TryParse(this.DistanceTextBox.Text, out distance);

                if (!isDistanceValid || (isDistanceValid && distance < 0))
                {
                    return -1;
                }
            }

            return distance;
        }

        private void ClearFields()
        {
            this.AddOriginDropDownList.SelectedIndex = 0;
            this.AddDestinationDropDownList.SelectedIndex = 0;
            this.DistanceTextBox.Text = string.Empty;
        }
    }
}