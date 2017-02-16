namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
    using Contracts;

    public class FlightLegsServices : IFlightLegsServices
    {
        private readonly IRepository<FlightLeg> flightLegs;

        public FlightLegsServices(IRepository<FlightLeg> flightLegs)
        {
            this.flightLegs = flightLegs;
        }

        public int AddFlightLeg(FlightLeg flightLeg)
        {
            if (flightLeg == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            this.flightLegs.Add(flightLeg);
            this.flightLegs.SaveChanges();

            return flightLeg.Id;
        }

        public IQueryable<FlightLeg> GetAll()
        {
            return this.flightLegs.All();
        }

        public FlightLeg GetFlightLeg(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.flightLegs.GetById(id);
        }

        public FlightLeg UpdateFlightLeg(int id, FlightLeg flightLeg)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (flightLeg == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

            var flightLegToUpdate = this.flightLegs.GetById(id);

            if (flightLegToUpdate != null)
            {
                flightLegToUpdate.DepartureAirportId = flightLeg.DepartureAirportId;
                flightLegToUpdate.ScheduledDepartureDateTime = flightLeg.ScheduledDepartureDateTime;
                flightLegToUpdate.ArrivalAirportId = flightLeg.ArrivalAirportId;
                flightLegToUpdate.ScheduledArrivalDateTime = flightLeg.ScheduledArrivalDateTime;
                flightLegToUpdate.IsDeleted = flightLeg.IsDeleted;
                flightLegToUpdate.FlightId = flightLeg.FlightId;
                flightLegToUpdate.RouteId = flightLeg.RouteId;

                this.flightLegs.SaveChanges();
            }

            return flightLegToUpdate;
        }

        public FlightLeg DeleteFlightLeg(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            var flightLegToDelete = this.flightLegs.GetById(id);

            if (flightLegToDelete != null)
            {
                flightLegToDelete.IsDeleted = true;

                this.flightLegs.SaveChanges();
            }

            return flightLegToDelete;
        }
    }
}
