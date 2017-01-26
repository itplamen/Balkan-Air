namespace BalkanAir.Data.Services.Contracts
{
    using System.Linq;

    using Models;

    public interface ICategoriesServices
    {
        int AddCategory(Category category);

        Category GetCategory(int id);

        IQueryable<Category> GetAll();

        Category UpdateCategory(int id, Category category);

        Category DeleteCategory(int id);
    }
}
