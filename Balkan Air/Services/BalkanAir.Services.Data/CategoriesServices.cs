namespace BalkanAir.Services.Data
{
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Contracts;

    public class CategoriesServices : ICategoriesServices
    {
        private IRepository<Category> categories;

        public CategoriesServices(IRepository<Category> categories)
        {
            this.categories = categories;
        }


        public int AddCategory(Category category)
        {
            this.categories.Add(category);
            this.categories.SaveChanges();

            return category.Id;
        }


        public IQueryable<Category> GetAll()
        {
            return this.categories.All();
        }

        public Category GetCategory(int id)
        {
            return this.categories.GetById(id);
        }

        public Category UpdateCategory(int id, Category category)
        {
            var categoryToUpdate = this.categories.GetById(id);

            if (category != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.IsDeleted = category.IsDeleted;

                this.categories.SaveChanges();
            }

            return category;
        }

        public Category DeleteCategory(int id)
        {
            var categoryToDelete = this.categories.GetById(id);

            if (categoryToDelete != null)
            {
                categoryToDelete.IsDeleted = true;
                this.categories.SaveChanges();
            }

            return categoryToDelete;
        }
    }
}
