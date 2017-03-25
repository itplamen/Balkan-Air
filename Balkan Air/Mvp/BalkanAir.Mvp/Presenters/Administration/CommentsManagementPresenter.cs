namespace BalkanAir.Mvp.Presenters.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using Common;
    using EventArgs.Administration;
    using Services.Data.Contracts;
    using ViewContracts.Administration;

    public class CommentsManagementPresenter : Presenter<ICommentsManagementView>
    {
        private readonly ICommentsServices commentsServices;

        public CommentsManagementPresenter(ICommentsManagementView view, ICommentsServices commentsServices) 
            : base(view)
        {
            if (commentsServices == null)
            {
                throw new ArgumentNullException(nameof(ICommentsServices));
            }

            this.commentsServices = commentsServices;

            this.View.OnCommentsGetData += this.View_OnCommentsGetData;
            this.View.OnCommentsUpdateItem += this.View_OnCommentsUpdateItem;
            this.View.OnCommentsDeleteItem += this.View_OnCommentsDeleteItem;
        }

        private void View_OnCommentsGetData(object sender, EventArgs e)
        {
            this.View.Model.Comments = this.commentsServices.GetAll()
                .OrderByDescending(c => c.DateOfComment);
        }

        private void View_OnCommentsUpdateItem(object sender, CommentsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CommentsManagementEventArgs));
            }

            var comment = this.commentsServices.GetComment(e.Id);

            if (comment == null)
            {
                this.View.ModelState.AddModelError(ErrorMessages.MODEL_ERROR_KEY, 
                    string.Format(ErrorMessages.MODEL_ERROR_MESSAGE, e.Id));
                return;
            }

            this.View.TryUpdateModel(comment);

            if (this.View.ModelState.IsValid)
            {
                this.commentsServices.UpdateComment(e.Id, comment);
            }
        }

        private void View_OnCommentsDeleteItem(object sender, CommentsManagementEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(CommentsManagementEventArgs));
            }

            this.commentsServices.DeleteComment(e.Id);
        }
    }
}
