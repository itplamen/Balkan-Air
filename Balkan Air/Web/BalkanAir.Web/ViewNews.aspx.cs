namespace BalkanAir.Web
{
    using System;
    using System.Linq;
    using System.Web.ModelBinding;
    using System.Web.UI;

    using Microsoft.AspNet.Identity;

    using Ninject;

    using Common;
    using Data.Models;
    using Services.Data.Contracts;

    public partial class ViewNews : Page
    {
        [Inject]
        public ICommentsServices CommentsServices { get; set; }

        [Inject]
        public INewsServices NewsServices { get; set; }

        [Inject]
        public IUsersServices UsersServices { get; set; }

        protected int NumberOfComments { get; private set; }

        public BalkanAir.Data.Models.News ViewNewsFormView_GetItem([QueryString]string id)
        {
            int validId;
            bool isValid = int.TryParse(id, out validId);

            if (isValid && validId > 0 && this.IsNewsIdValid(validId))
            {
                this.NewsIdHiddenField.Value = validId.ToString();
                return this.NewsServices.GetNews(validId);
            }

            this.Response.Redirect(Pages.NEWS);
            return null;
        }

        public IQueryable<Comment> CommentsListView_GetData([QueryString]string id)
        {
            int validId;
            bool isValid = int.TryParse(id, out validId);

            if (isValid && validId > 0 && this.IsNewsIdValid(validId))
            {
                var comments = this.NewsServices.GetNews(validId).Comments;
                this.NumberOfComments = comments.Count;

                return comments
                    .AsQueryable();
            }

            return null;
        }

        public void CommentsListView_InsertItem()
        {
            var comment = new Comment();
            TryUpdateModel(comment);

            comment.DateOfComment = DateTime.Now;
            comment.NewsId = int.Parse(this.NewsIdHiddenField.Value);

            if (this.Context.User.Identity.IsAuthenticated)
            {
                comment.UserId = this.Context.User.Identity.GetUserId();
            }

            this.CommentsServices.AddComment(comment);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected string GetProfileIconSrc(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return string.Empty;
            }

            var user = this.UsersServices.GetUser(userId);

            if (user != null && user.UserSettings.ProfilePicture != null)
            {
                return "data:image/jpeg;base64," + Convert.ToBase64String(user.UserSettings.ProfilePicture);
            }

            return string.Empty;
        }

        protected string GetAuthorOfTheComment(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return "Anonymous";
            }

            var user = this.UsersServices.GetUser(userId);

            if (!string.IsNullOrEmpty(user.UserSettings.FirstName) && !string.IsNullOrEmpty(user.UserSettings.LastName))
            {
                return user.UserSettings.FirstName + " " + user.UserSettings.LastName;
            }
            else
            {
                return user.Email;
            }
        }

        private bool IsNewsIdValid(int id)
        {
            int lastNewsId = this.NewsServices.GetAll()
               .OrderByDescending(a => a.Id)
               .First()
               .Id;

            if (id <= lastNewsId)
            {
                return true;
            }

            return false;
        }
    }
}