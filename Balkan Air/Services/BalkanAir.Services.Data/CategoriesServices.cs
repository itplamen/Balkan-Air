namespace BalkanAir.Services.Data
{
    using System;
    using System.Linq;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Repositories.Contracts;
    using Common;
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
            if (category == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            return this.categories.GetById(id);
        }

        public Category UpdateCategory(int id, Category category)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

            if (category == null)
            {
                throw new ArgumentNullException(ErrorMessages.ENTITY_CANNOT_BE_NULL);
            }

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
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(ErrorMessages.INVALID_ID);
            }

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
