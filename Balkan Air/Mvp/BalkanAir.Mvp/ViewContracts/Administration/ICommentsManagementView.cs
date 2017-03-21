namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface ICommentsManagementView : IView<CommentsManagementViewModel>
    {
        event EventHandler OnCommentsGetData;

        event EventHandler<CommentsManagementEventArgs> OnCommentsUpdateItem;

        event EventHandler<CommentsManagementEventArgs> OnCommentsDeleteItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
