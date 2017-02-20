namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class CategoriesManagement : Page
    {
        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

        public IQueryable<Category> CategoriesGridView_GetData()
        {
            return this.CategoriesServices.GetAll()
                .OrderBy(c => c.Name);
        }

        public void CategoriesGridView_UpdateItem(int id)
        {
            var category = this.CategoriesServices.GetCategory(id);
            
            if (category == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(category);

            if (ModelState.IsValid)
            {
                this.CategoriesServices.UpdateCategory(id, category);
            }
        }

        public void CategoriesGridView_DeleteItem(int id)
        {
            this.CategoriesServices.DeleteCategory(id);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateCategoryBtn_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                var category = new Category() { Name = this.CategoryNameTextBox.Text };
                this.CategoriesServices.AddCategory(category);

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