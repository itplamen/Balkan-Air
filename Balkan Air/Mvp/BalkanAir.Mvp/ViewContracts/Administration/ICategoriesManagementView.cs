namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface ICategoriesManagementView : IView<CategoriesManagementViewModel>
    {
        event EventHandler OnCategoriesGetData;

        event EventHandler<CategoriesManagementEventArgs> OnCategoriesUpdateItem;

        event EventHandler<CategoriesManagementEventArgs> OnCategoriesDeleteItem;

        event EventHandler<CategoriesManagementEventArgs> OnCategoriesAddItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
