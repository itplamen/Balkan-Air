namespace BalkanAir.Data.Services
{
    using System.Linq;

    using Contracts;
    using Models;
    using Repositories.Contracts;

    public class FlightStatusesServices : IFlightStatusesServices
    {
        private readonly IRepository<FlightStatus> flightStatuses;

        public FlightStatusesServices(IRepository<FlightStatus> flightStatuses)
        {
            this.flightStatuses = flightStatuses;
        }

        public int AddFlightStatus(FlightStatus flightStatus)
        {
            this.flightStatuses.Add(flightStatus);
            this.flightStatuses.SaveChanges();

            return flightStatus.Id;
        }

        public FlightStatus GetFlightStatus(int id)
        {
            return this.flightStatuses.GetById(id);
        }

        public IQueryable<FlightStatus> GetAll()
        {
            return this.flightStatuses.All();
        }

        public FlightStatus UpdateFlightStatus(int id, FlightStatus flightStatus)
        {
            var flightStatusToUpdate = this.flightStatuses.GetById(id);

            if (flightStatusToUpdate != null)
            {
                flightStatusToUpdate.Name = flightStatus.Name;
                flightStatusToUpdate.IsDeleted = flightStatus.IsDeleted;
                this.flightStatuses.SaveChanges();
            }

            return flightStatusToUpdate;
        }

        public FlightStatus DeleteFlightStatus(int id)
        {
            var flightStatusToDelete = this.flightStatuses.GetById(id);

            if (flightStatusToDelete != null)
            {
                flightStatusToDelete.IsDeleted = true;
                this.flightStatuses.SaveChanges();
            }

            return flightStatusToDelete;
        }
    }
}
