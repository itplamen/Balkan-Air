namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using BalkanAir.Data.Models;
    using BalkanAir.Data.Services.Contracts;

    public partial class ManageNews : Page
    {
        private IDictionary<string, string> orderNewsBy = new Dictionary<string, string>()
        {
            { "title", "Title" },
            { "date", "DateCreated" },
            { "category", "Category.Name" }
        };

        [Inject]
        public INewsServices NewsServices { get; set; }

        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<News> ManageNewsListView_GetData([QueryString]string orderBy)
        {
            var news = this.NewsServices.GetAll();

            if (!string.IsNullOrEmpty(orderBy) && this.orderNewsBy.ContainsKey(orderBy))
            {
                return news.OrderBy(this.orderNewsBy[orderBy] + " Ascending");
            }
            else if (!string.IsNullOrEmpty(this.Request.Url.Query)) 
            {
                HttpUtility.ParseQueryString(this.Request.Url.Query).Remove(orderBy);
                this.Response.Redirect(this.Request.Url.AbsolutePath);
            }
            
            return news;
        }

        public void ManageNewsListView_UpdateItem(int id)
        {
            var news = new News();
            TryUpdateModel(news);

            this.NewsServices.UpdateNews(id, news);
        }

        public void ManageNewsListView_DeleteItem(int id)
        {
            this.NewsServices.DeleteNews(id);
        }

        public void ManageNewsListView_InsertItem()
        {
            var news = new News();
            TryUpdateModel(news);
            
            news.DateCreated = DateTime.Now;
            this.NewsServices.AddNews(news);
        }

        public IQueryable<Category> CategoriesDropDownList_GetData()
        {
            return this.CategoriesServices.GetAll();
        }

        protected void AddNewsButton_Click(object sender, EventArgs e)
        {
            this.ManageNewsListView.InsertItemPosition = InsertItemPosition.LastItem;

            //Button addNewsButton = sender as Button;

            //if (addNewsButton != null)
            //{
            //    addNewsButton.Visible = false;
            //}
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.ManageNewsListView.InsertItemPosition = InsertItemPosition.None;
        }
    }
}