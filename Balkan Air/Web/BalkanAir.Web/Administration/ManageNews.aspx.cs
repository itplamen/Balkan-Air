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

            FileUpload editImage = (FileUpload)this.ManageNewsListView.EditItem.FindControl("UploadImageEdit");

            if (editImage != null)
            {
                news.HeaderImage = this.GetUploadedImage(editImage);
            }

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

            FileUpload editImage = (FileUpload)this.ManageNewsListView.InsertItem.FindControl("UploadImage");

            if (editImage != null)
            {
                news.HeaderImage = this.GetUploadedImage(editImage);
            }

            news.DateCreated = DateTime.Now;

            this.NewsServices.AddNews(news);
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
            this.ManageNewsListView.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.ManageNewsListView.InsertItemPosition = InsertItemPosition.None;
        }

        private byte[] GetUploadedImage(FileUpload fileUpload)
        {
            byte[] imageData = null;

            if (this.IsImageValid(fileUpload))
            {
                var imageLength = fileUpload.PostedFile.ContentLength;
                imageData = new byte[imageLength + 1];

                Stream fileStream = fileUpload.PostedFile.InputStream;

                using (fileStream)
                {
                    fileStream.Read(imageData, 0, imageLength);
                }
            }

            return imageData;
        }

        private bool IsImageValid(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                if (fileUpload.PostedFile.ContentLength <= 1000000)
                {
                    if (fileUpload.PostedFile.ContentType == "image/jpg" || fileUpload.PostedFile.ContentType == "image/jpeg" ||
                        fileUpload.PostedFile.ContentType == "image/png" || fileUpload.PostedFile.ContentType == "image/gif")
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