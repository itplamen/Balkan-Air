namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class SeatsManagementPresenter : Presenter<ISeatsManagementView>
    {
        private readonly ILegInstancesServices legInstancesServices;
        private readonly ISeatsServices seatsServices;
        private readonly ITravelClassesServices travelClassesServices;

        public SeatsManagementPresenter(
            ISeatsManagementView view, 
            ILegInstancesServices legInstancesServices,
            ISeatsServices seatsServices, 
            ITravelClassesServices travelClassesServices) 
            : base(view)
        {
            if (legInstancesServices == null)
            {
                throw new ArgumentNullException(nameof(ILegInstancesServices));
            }

            if (seatsServices == null)
            {
                throw new ArgumentNullException(nameof(ISeatsServices));
            }

            if (travelClassesServices == null)
            {
                throw new ArgumentNullException(nameof(ITravelClassesServices));
            }

            this.legInstancesServices = legInstancesServices;
            this.seatsServices = seatsServices;
            this.travelClassesServices = travelClassesServices;

            this.View.OnSeatsGetData += this.View_OnSeatsGetData;
            this.View.OnSeatsUpdateItem += this.View_OnSeatsUpdateItem;
            this.View.OnSeatsDeleteItem += this.View_OnSeatsDeleteItem;
            this.View.OnSeatsAddItem += this.View_OnSeatsAddItem;
            this.View.OnTravelClassesGetData += this.View_OnTravelClassesGetData;
            this.View.OnLegInstancesGetData += this.View_OnLegInstancesGetData;
            this.View.OnTravelClassInfoGetItem += this.View_OnTravelClassInfoGetItem;
        }

        private void View_OnSeatsGetData(object sender, EventArgs e)
        {
            this.View.Model.Seats = this.seatsServices.GetAll();
        }

        private void View_OnSeatsUpdateItem(object sender, SeatsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(SeatsManagementEventArgs));
            }

            var seat = this.seatsServices.GetSeat(e.Id);

            if (seat == null)
            {
                this.View.ModelState.AddModelError(
                    ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));

                return;
            }

            this.View.TryUpdateModel(seat);

            if (this.View.ModelState.IsValid)
            {
                seat.TravelClassId = e.TravelClassId;
                seat.LegInstanceId = e.LegInstanceId;

                this.seatsServices.UpdateSeat(e.Id, seat);
            }
        }

        private void View_OnSeatsDeleteItem(object sender, SeatsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(SeatsManagementEventArgs));
            }

            this.seatsServices.DeleteSeat(e.Id);
        }

        private void View_OnSeatsAddItem(object sender, SeatsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(SeatsManagementEventArgs));
            }

            var seat = new Seat()
            {
                Row = e.Row,
                Number = e.Number,
                IsReserved = e.IsReserved,
                TravelClassId = e.TravelClassId,
                LegInstanceId = e.LegInstanceId
            };

            e.Id = this.seatsServices.AddSeat(seat);
        }

        private void View_OnTravelClassesGetData(object sender, EventArgs e)
        {
            this.View.Model.TravelClasses = this.travelClassesServices.GetAll()
                .Where(t => !t.IsDeleted)
                .OrderBy(t => t.AircraftId)
                .Select(t => new
                {
                    Id = t.Id,
                    TravelClassInfo = "Id:" + t.Id + " " + t.Type.ToString() + " class " + t.NumberOfRows + " rows"
                });
        }

        private void View_OnLegInstancesGetData(object sender, EventArgs e)
        {
            this.View.Model.LegInstances = this.legInstancesServices.GetAll()
                .Where(l => !l.IsDeleted)
                .OrderBy(l => l.DepartureDateTime)
                .ThenBy(l => l.ArrivalDateTime)
                .Select(l => new
                {
                    Id = l.Id,
                    LegInstanceInfo = "Id:" + l.Id + ", " + l.DepartureDateTime + " -> " + l.ArrivalDateTime
                });
        }

        private void View_OnTravelClassInfoGetItem(object sender, SeatsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(SeatsManagementEventArgs));
            }

            var travelClass = this.travelClassesServices.GetTravelClass(e.TravelClassId);

            if (travelClass == null)
            {
                this.View.Model.TravelClassInfo = ErrorMessages.TRAVEL_CLASS_NOT_FOUND;
            }

            this.View.Model.TravelClassInfo = "Id:" + travelClass.Id + " " + travelClass.Type.ToString() + " class " + travelClass.NumberOfRows + " rows";
        }
    }
}
