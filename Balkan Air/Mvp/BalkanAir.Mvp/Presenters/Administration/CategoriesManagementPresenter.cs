namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using Data.Models;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class CategoriesManagementPresenter : Presenter<ICategoriesManagementView>
    {
        private ICategoriesServices categoriesServices;

        public CategoriesManagementPresenter(ICategoriesManagementView view, ICategoriesServices categoriesServices) 
            : base(view)
        {
            if (categoriesServices == null)
            {
                throw new ArgumentNullException(nameof(ICategoriesServices));
            }

            this.categoriesServices = categoriesServices;

            this.View.OnCategoriesGetData += this.View_OnCategoriesGetData;
            this.View.OnCategoriesUpdateItem += this.View_OnCategoriesUpdateItem;
            this.View.OnCategoriesDeleteItem += this.View_OnCategoriesDeleteItem;
            this.View.OnCategoriesAddItem += this.View_OnCategoriesAddItem;
        }

        private void View_OnCategoriesGetData(object sender, EventArgs e)
        {
            this.View.Model.Categories = this.categoriesServices.GetAll()
                .OrderBy(c => c.Name);
        }

        private void View_OnCategoriesUpdateItem(object sender, CategoriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CategoriesManagementEventArgs));
            }

            var category = this.categoriesServices.GetCategory(e.Id);

            if (category == null)
            {
                this.View.ModelState.AddModelError(
                    ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));

                return;
            }

            this.View.TryUpdateModel(category);

            if (this.View.ModelState.IsValid)
            {
                this.categoriesServices.UpdateCategory(e.Id, category);
            }
        }

        private void View_OnCategoriesDeleteItem(object sender, CategoriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CategoriesManagementEventArgs));
            }

            this.categoriesServices.DeleteCategory(e.Id);
        }

        private void View_OnCategoriesAddItem(object sender, CategoriesManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CategoriesManagementEventArgs));
            }

            var category = new Category() { Name = e.Name };

            e.Id = this.categoriesServices.AddCategory(category);
        }
    }
}
