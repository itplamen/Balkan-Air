namespace BalkanAir.Web
{
    using System;
    using System.Web.UI.WebControls;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Mvp.EventArgs;
    using Mvp.Models;
    using Mvp.Presenters;
    using Mvp.ViewContracts;

    [PresenterBinding(typeof(NewsPresenter))]
    public partial class News : MvpPage<NewsViewModel>, INewsView
    {
        public event EventHandler OnCategoriesGetData;
        public event EventHandler OnNewsGetData;
        public event EventHandler<NewsEventArgs> OnNewsByCategoryGetData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.OnCategoriesGetData?.Invoke(sender, e);

                this.ShowNewsByCategoryDropDownList.DataSource = this.Model.Categories;
                this.ShowNewsByCategoryDropDownList.DataBind();
                this.ShowNewsByCategoryDropDownList.Items.Insert(0, new ListItem("-- All --", "0"));

                this.BindAllNewsToRepeater();
            }
        }

        protected void ShowNewsByCategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCategoryId = int.Parse(this.ShowNewsByCategoryDropDownList.SelectedItem.Value);

            // Default value selected -- All --
            if (selectedCategoryId == 0)
            {
                this.BindAllNewsToRepeater();
            }
            else
            {
                this.OnNewsByCategoryGetData?.Invoke(sender, new NewsEventArgs() { Id = selectedCategoryId });

                this.NewsRepeater.DataSource = this.Model.News;
                this.NewsRepeater.DataBind();
            }
        }

        private void BindAllNewsToRepeater()
        {
            this.OnNewsGetData?.Invoke(null, null);

            this.NewsRepeater.DataSource = this.Model.News;
            this.NewsRepeater.DataBind();
        }
    }
}