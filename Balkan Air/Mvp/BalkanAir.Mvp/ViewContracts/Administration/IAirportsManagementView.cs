namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAirportsManagementView : IView<AirportsManagementViewModel>
    {
        event EventHandler OnAirprotsGetData;

        event EventHandler<AirportsManagementEventArgs> OnAirportsUpdateItem;

        event EventHandler<AirportsManagementEventArgs> OnAirportsDeleteItem;

        event EventHandler<AirportsManagementEventArgs> OnAirprotsAddItem;

        event EventHandler OnCountriesGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
