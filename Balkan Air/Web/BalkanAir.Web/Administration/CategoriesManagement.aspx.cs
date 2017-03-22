namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(CategoriesManagementPresenter))]
    public partial class CategoriesManagement : MvpPage<CategoriesManagementViewModel>, ICategoriesManagementView
    {
        public event EventHandler OnCategoriesGetData;
        public event EventHandler<CategoriesManagementEventArgs> OnCategoriesUpdateItem;
        public event EventHandler<CategoriesManagementEventArgs> OnCategoriesDeleteItem;
        public event EventHandler<CategoriesManagementEventArgs> OnCategoriesAddItem;

        public IQueryable<Category> CategoriesGridView_GetData()
        {
            this.OnCategoriesGetData?.Invoke(null, null);

            return this.Model.Categories;
        }

        public void CategoriesGridView_UpdateItem(int id)
        {
            this.OnCategoriesUpdateItem?.Invoke(null, new CategoriesManagementEventArgs() { Id = id });
        }

        public void CategoriesGridView_DeleteItem(int id)
        {
            this.OnCategoriesDeleteItem?.Invoke(null, new CategoriesManagementEventArgs() { Id = id });
        }

        protected void CreateCategoryBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var categoryEventArgs = new CategoriesManagementEventArgs() { Name = this.CategoryNameTextBox.Text };

                this.OnCategoriesAddItem?.Invoke(sender, categoryEventArgs);

                this.SuccessPanel.Visible = true;
                this.AddedCategoryIdLiteral.Text = categoryEventArgs.Id.ToString();

                this.ClearFields();
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            this.ClearFields();
        }

        private void ClearFields()
        {
            this.CategoryNameTextBox.Text = string.Empty;
        }
    }
}