namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageNews : Page
    {
        private IDictionary<string, string> orderNewsBy = new Dictionary<string, string>()
        {
            { "title", "Title" },
            { "date", "DateCreated" },
            { "category", "Category.Name" }
        };

        protected string SuccessMessage { get; private set; }

        [Inject]
        public INewsServices NewsServices { get; set; }

        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

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

        public IQueryable<Category> CategoriesDropDownList_GetData()
        {
            return this.CategoriesServices.GetAll()
                .OrderBy(c => c.Name);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddNewsButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                News news = new News()
                {
                    Title = this.TitleInsertTextBox.Text,
                    HeaderImage = this.GetUploadedImage(),
                    Content = this.ContentInsertEditor.Content,
                    DateCreated = DateTime.Now,
                    CategoryId = int.Parse(this.CategoriesInsertDropDownList.SelectedItem.Value)
                };

                this.NewsServices.AddNews(news);
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.UploadImage.Dispose();
            this.TitleInsertTextBox.Text = string.Empty;
            this.CategoriesInsertDropDownList.SelectedIndex = 0;
            this.ContentInsertEditor.Content = string.Empty;
        }

        private byte[] GetUploadedImage()
        {
            byte[] imageData = null;

            if (this.IsImageValid())
            {
                var imageLength = this.UploadImage.PostedFile.ContentLength;
                imageData = new byte[imageLength + 1];

                Stream fileStream = this.UploadImage.PostedFile.InputStream;

                using (fileStream)
                {
                    fileStream.Read(imageData, 0, imageLength);
                }
            }

            return imageData;
        }

        private bool IsImageValid()
        {
            if (this.UploadImage.HasFile)
            {
                if (this.UploadImage.PostedFile.ContentLength <= 1000000)
                {
                    if (this.UploadImage.PostedFile.ContentType == "image/jpg" ||
                        this.UploadImage.PostedFile.ContentType == "image/jpeg" ||
                        this.UploadImage.PostedFile.ContentType == "image/png" ||
                        this.UploadImage.PostedFile.ContentType == "image/gif")
                    {
                        return true;
                    }
                    else
                    {
                        this.ImageValidator.ErrorMessage = "Invalid image format - jpg/jpeg/png/gif are supported!";
                        this.ImageValidator.IsValid = false;
                    }
                }
                else
                {
                    this.ImageValidator.ErrorMessage = "File is too big!";
                    this.ImageValidator.IsValid = false;
                }
            }
            else
            {
                this.ImageValidator.ErrorMessage = "Select image first!";
                this.ImageValidator.IsValid = false;
            }

            return false;
        }
    }
}