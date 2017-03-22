namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class FaresManagementViewModel
    {
        public IQueryable<Fare> Fares { get; set; }

        public IQueryable<object> Routes { get; set; }
    }
}
