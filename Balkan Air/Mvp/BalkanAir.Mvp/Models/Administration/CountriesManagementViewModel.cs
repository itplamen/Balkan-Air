namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;
    
    public class CountriesManagementViewModel
    {
        public IQueryable<Country> Countries { get; set; }
    }
}
