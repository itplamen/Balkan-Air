namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class RoutesManagementViewModel
    {
        public IQueryable<Route> Routes { get; set; }

        public IQueryable<object> Airports { get; set; }
    }
}
