namespace BalkanAir.Data.Services
{
    using System.Linq;

    using BalkanAir.Data.Services.Contracts;
    using Models;
    using Repositories.Contracts;

    public class CategoriesServices : ICategoriesServices
    {
        private IRepository<Category> categories;

        public CategoriesServices(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All();
        }
    }
}
