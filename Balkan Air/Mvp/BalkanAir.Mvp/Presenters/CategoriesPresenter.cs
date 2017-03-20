namespace BalkanAir.Mvp.Presenters
{
    using System.Linq;

    using WebFormsMvp;

    using Services.Data.Contracts;
    using ViewContracts;
    
    public class CategoriesPresenter : Presenter<ICategoriesView>
    {
        private readonly ICategoriesServices categoriesServices;

        public CategoriesPresenter(ICategoriesView view, ICategoriesServices categoriesServices) 
            : base(view)
        {
            this.categoriesServices = categoriesServices;

            this.View.OnSortedCategoriesGetData += this.View_OnSortedCategoriesGetData;
        }

        private void View_OnSortedCategoriesGetData(object sender, System.EventArgs e)
        {
            this.View.Model.SortedCategories = this.categoriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
