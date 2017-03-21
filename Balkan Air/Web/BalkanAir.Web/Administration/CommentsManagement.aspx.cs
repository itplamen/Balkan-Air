namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;

    using WebFormsMvp;
    using WebFormsMvp.Web;

    using Data.Models;
    using Mvp.EventArgs.Administration;
    using Mvp.Models.Administration;
    using Mvp.Presenters.Administration;
    using Mvp.ViewContracts.Administration;

    [PresenterBinding(typeof(CommentsManagementPresenter))]
    public partial class CommentsManagement : MvpPage<CommentsManagementViewModel>, ICommentsManagementView
    {
        public event EventHandler OnCommentsGetData;
        public event EventHandler<CommentsManagementEventArgs> OnCommentsUpdateItem;
        public event EventHandler<CommentsManagementEventArgs> OnCommentsDeleteItem;

        public IQueryable<Comment> CommentsGridView_GetData()
        {
            this.OnCommentsGetData?.Invoke(null, null);

            return this.Model.Comments;
        }

        public void CommentsGridView_UpdateItem(int id)
        {
            this.OnCommentsUpdateItem?.Invoke(null, new CommentsManagementEventArgs() { Id = id });
        }

        public void CommentsGridView_DeleteItem(int id)
        {
            this.OnCommentsDeleteItem?.Invoke(null, new CommentsManagementEventArgs() { Id = id });
        }
    }
}