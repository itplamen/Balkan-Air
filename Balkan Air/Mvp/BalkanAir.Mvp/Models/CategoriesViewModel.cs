namespace BalkanAir.Mvp.Models
{
    using System.Collections.Generic;

    using Data.Models;

    public class CategoriesViewModel
    {
        public ICollection<Category> SortedCategories { get; set; }
    }
}
