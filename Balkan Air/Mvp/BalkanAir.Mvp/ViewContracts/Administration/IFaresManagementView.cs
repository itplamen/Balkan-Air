namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IFaresManagementView : IView<FaresManagementViewModel>
    {
        event EventHandler OnFaresGetData;

        event EventHandler<FaresManagementEventArgs> OnFaresUpdateItem;

        event EventHandler<FaresManagementEventArgs> OnFaresDeleteItem;

        event EventHandler<FaresManagementEventArgs> OnFaresAddItem;

        event EventHandler OnRoutesGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
