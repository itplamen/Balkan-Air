namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class RoutesManagement : Page
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [Inject]
        public IRoutesServices RoutesServices { get; set; }

        public IQueryable<Route> RoutesGridView_GetData()
        {
            return this.RoutesServices.GetAll()
                .OrderBy(r => r.Origin.Name)
                .ThenBy(r => r.Destination.Name);
        }

        public void RoutesGridView_UpdateItem(int id)
        {
            var route = this.RoutesServices.GetRoute(id);
            
            if (route == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(route);

            if (ModelState.IsValid)
            {
                this.RoutesServices.UpdateRoute(id, route);
            }
        }

        public void RoutesGridView_DeleteItem(int id)
        {
            this.RoutesServices.DeleteRoute(id);
        }

        public IQueryable<object> AirportsDropDownList_GetData()
        {
            return this.AirportsServices.GetAll()
                .Where(a => !a.IsDeleted)
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation)
                .Select(a => new
                {
                    Id = a.Id,
                    AirportInfo = "Id:" + a.Id + ", " + a.Name + " (" + a.Abbreviation + ")"
                });
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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

                var route = new Route()
                {
                    OriginId = int.Parse(this.AddOriginDropDownList.SelectedItem.Value),
                    DestinationId = int.Parse(this.AddDestinationDropDownList.SelectedItem.Value),
                    DistanceInKm = distance
                };

                int id = this.RoutesServices.AddRoute(route);

                this.SuccessPanel.Visible = true;
                this.AddedRouteIdLiteral.Text = id.ToString();

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
                    return - 1;
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