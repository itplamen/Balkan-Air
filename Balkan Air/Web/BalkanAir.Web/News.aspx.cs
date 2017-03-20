namespace BalkanAir.Web
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Services.Data.Contracts;
    using WebFormsMvp.Web;
    using Mvp.News;
    using WebFormsMvp;

    [PresenterBinding(typeof(NewsPresenter))]
    public partial class News : MvpPage<NewsViewModel>, INewsView
    {
        //[Inject]
        //public ICategoriesServices CategoriesServices { get; set; }

        //[Inject]
        //public INewsServices NewsServices { get; set; }

        public event EventHandler OnNewsGetData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //this.ShowNewsByCategoryDropDownList.DataSource = this.CategoriesServices.GetAll()
                //    .Where(c => !c.IsDeleted)
                //    .OrderBy(c => c.Name)
                //    .ToList();
                //this.ShowNewsByCategoryDropDownList.DataBind();
                //this.ShowNewsByCategoryDropDownList.Items.Insert(0, new ListItem("-- All --", "0"));

                
                this.BindAllNewsToRepeater();
            }
        }

        protected void ShowNewsByCategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCategoryId = int.Parse(this.ShowNewsByCategoryDropDownList.SelectedItem.Value);

            // Default value selected --All--
            if (selectedCategoryId == 0)
            {
                this.BindAllNewsToRepeater();
            }
            else
            {
                //this.NewsRepeater.DataSource = this.NewsServices.GetAll()
                //    .Where(a => !a.IsDeleted && a.CategoryId == selectedCategoryId)
                //    .OrderBy(a => a.DateCreated)
                //    .ToList();
                //this.NewsRepeater.DataBind();
            }
        }

        private void BindAllNewsToRepeater()
        {
            this.OnNewsGetData?.Invoke(null, null);

            this.NewsRepeater.DataSource = this.Model.News
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.DateCreated)
                .ToList();
            this.NewsRepeater.DataBind();
        }
    }
}