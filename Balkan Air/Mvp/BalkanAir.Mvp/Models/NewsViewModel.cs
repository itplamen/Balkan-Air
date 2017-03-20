namespace BalkanAir.Mvp.Models
{
    using System.Collections.Generic;

    using Data.Models;

    public class NewsViewModel
    {
        public ICollection<News> News { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
