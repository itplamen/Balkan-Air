namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class FlightStatusesManagementViewModel
    {
        public IQueryable<FlightStatus> FlightStatuses { get; set; }
    }
}
