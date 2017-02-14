namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageCategories : Page
    {
        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

        public IQueryable<Category> ManageCategoriesGridView_GetData()
        {
            return this.CategoriesServices.GetAll()
                .OrderBy(c => c.Name);
        }

        public void ManageCategoriesGridView_UpdateItem(int id)
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

        public void ManageCategoriesGridView_DeleteItem(int id)
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
            }
        }
    }
}