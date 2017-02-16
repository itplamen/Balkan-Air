namespace BalkanAir.Web
{
    using System;
    using System.Linq;
    using System.Web.ModelBinding;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Microsoft.AspNet.Identity;

    using Ninject;

    using Common;
    using Data.Common;
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

        public Data.Models.News ViewNewsFormView_GetItem([QueryString]string id)
        {
            int validId;
            bool isValid = int.TryParse(id, out validId);

            if (isValid && validId > 0 && this.IsIdLessOrEqualToLastNewsId(validId))
            {
                var news = this.NewsServices.GetNews(validId);

                if (!news.IsDeleted)
                {
                    this.NewsIdHiddenField.Value = validId.ToString();
                    return news;
                }
            }

            this.Response.Redirect(Pages.NEWS);
            return null;
        }

        public IQueryable<Comment> CommentsListView_GetData([QueryString]string id)
        {
            int validId;
            bool isValid = int.TryParse(id, out validId);

            if (isValid && validId > 0 && this.IsIdLessOrEqualToLastNewsId(validId))
            {
                var news = this.NewsServices.GetNews(validId);

                if (!news.IsDeleted)
                {
                    var comments = news.Comments
                             .Where(c => !c.IsDeleted)
                             .ToList();


                    this.NumberOfComments = comments.Count;

                    return comments
                        .AsQueryable();
                }
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

        public void CommentsListView_UpdateItem(int id)
        {
            var comment = new Comment();
            TryUpdateModel(comment);

            this.CommentsServices.UpdateComment(id, comment);
        }

        public void CommentsListView_DeleteItem(int id)
        {
            this.CommentsServices.DeleteComment(id);
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

        protected bool IsIconVisible(string userId)
        {
            if (this.Context.User.Identity.IsAuthenticated &&
                this.Context.User.IsInRole(ValidationConstants.ADMINISTRATOR_ROLE))
            {
                return true;
            }

            if (string.IsNullOrEmpty(userId) || !this.Context.User.Identity.IsAuthenticated)
            {
                return false;
            }
            else if (this.Context.User.Identity.IsAuthenticated && !this.Context.User.Identity.GetUserId().Equals(userId))
            {
                return false;
            }

            return true;
        }

        protected void CommentsListView_DataBound(object sender, EventArgs e)
        {
            DataPager pager = (DataPager)this.CommentsListView.FindControl("CommentsDataPager");

            if (pager != null)
            {
                pager.Visible = (pager.PageSize < pager.TotalRowCount);
            }
        }

        private bool IsIdLessOrEqualToLastNewsId(int id)
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