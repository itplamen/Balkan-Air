namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface ICountriesManagementView : IView<CountriesManagementViewModel>
    {
        event EventHandler OnCountriesGetData;

        event EventHandler<CountriesManagementEventArgs> OnCountriesUpdateItem;

        event EventHandler<CountriesManagementEventArgs> OnCountriesDeleteItem;

        event EventHandler<CountriesManagementEventArgs> OnCountriesAddItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
