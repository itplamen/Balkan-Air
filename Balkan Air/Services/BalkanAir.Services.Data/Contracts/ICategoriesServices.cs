namespace BalkanAir.Services.Data.Contracts
{
    using System.Linq;

    using BalkanAir.Data.Models;

    public interface ICategoriesServices
    {
        int AddCategory(Category category);

        Category GetCategory(int id);

        IQueryable<Category> GetAll();

        Category UpdateCategory(int id, Category category);

        Category DeleteCategory(int id);
    }
}
