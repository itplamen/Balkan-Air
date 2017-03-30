namespace BalkanAir.Web.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using System.Web;
    using System.Web.ModelBinding;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class NewsManagement : Page
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

        [Inject]
        public INotificationsServices NotificationsServices { get; set; }

        [Inject]
        public IUserNotificationsServices UserNotificationsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        public IQueryable<News> NewsListView_GetData([QueryString]string orderBy)
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

        public void NewsListView_UpdateItem(int id)
        {
            var news = new News();
            this.TryUpdateModel(news);

            FileUpload editImage = (FileUpload)this.NewsListView.EditItem.FindControl("UploadImageEdit");

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

        public void NewsListView_InsertItem()
        {
            var news = new News();
            this.TryUpdateModel(news);

            FileUpload editImage = (FileUpload)this.NewsListView.InsertItem.FindControl("UploadImage");

            if (editImage != null)
            {
                news.HeaderImage = this.GetUploadedImage(editImage);
            }

            news.DateCreated = DateTime.Now;

            this.NewsServices.AddNews(news);

            this.SendNotificationToSubscribedUsers(news);
            this.SendMailToSubscribedUsers(news);
        }

        public IQueryable<Category> CategoriesDropDownList_GetData()
        {
            return this.CategoriesServices.GetAll()
                .OrderBy(c => c.Name);
        }

        protected void AddNewsButton_Click(object sender, EventArgs e)
        {
            this.NewsListView.InsertItemPosition = InsertItemPosition.LastItem;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            this.NewsListView.InsertItemPosition = InsertItemPosition.None;
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

        private void SendNotificationToSubscribedUsers(News news)
        {
            var notificationId = this.AddNotification(news);
            var usersIdToSendNotification = this.GetSubscribedUsersToReceiveNotification();

            this.UserNotificationsServices.SendNotification(notificationId, usersIdToSendNotification);
        }

        private void SendMailToSubscribedUsers(News news)
        {
            var userEmails = this.GetSubscribedUsersToReceiveEmail();

            StringBuilder messageBody = new StringBuilder();
            messageBody.Append("Hello, Passenger,");
            messageBody.Append("<br/><br/>" + this.GetContent(news));
            messageBody.Append("<br/><br/><small>If you don't want to receive emails for new news, go to your account settings and " +
                "uncheck <strong>'Receive email when there is a new news'</strong> option!</small>");

            var mailSender = MailSender.Instance;
            mailSender.SendMail(userEmails[0], "Added a new news!", messageBody.ToString(), userEmails);
        }

        private int AddNotification(News news)
        {
            StringBuilder content = new StringBuilder();
            content.Append(this.GetContent(news));
            content.Append("<br/><small>If you don't want to receive notifications for new news, go to your account settings and " +
                "uncheck <strong>'Receive notification when there is a new news'</strong> option!</small>");

            var addedNewNewsNotification = new Notification()
            {
                Content = content.ToString(),
                DateCreated = DateTime.Now,
                Type = NotificationType.AddedNewNews
            };

            this.NotificationsServices.AddNotification(addedNewNewsNotification);

            return addedNewNewsNotification.Id;
        }

        private IEnumerable<string> GetSubscribedUsersToReceiveNotification()
        {
            return this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.ReceiveNotificationWhenNewNews)
                .Select(u => u.Id)
                .ToList();
        }

        private IList<string> GetSubscribedUsersToReceiveEmail()
        {
            return this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.ReceiveEmailWhenNewNews)
                .Select(u => u.Email)
                .ToList();
        }

        private string GetContent(News news)
        {
            string date = news.DateCreated.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            string time = news.DateCreated.ToString("HH:mm", CultureInfo.InvariantCulture);
            string addedAt = date + " at " + time;

            return string.Format(@"Added a new news ""{0}"" on {1}!", news.Title, addedAt);
        }
    }
}