namespace BalkanAir.Mvp.Models.Administration
{
    using System.Linq;

    using Data.Models;

    public class CategoriesManagementViewModel
    {
        public IQueryable<Category> Categories { get; set; }
    }
}
