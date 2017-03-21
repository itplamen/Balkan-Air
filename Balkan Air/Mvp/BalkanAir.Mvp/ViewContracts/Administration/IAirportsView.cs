namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface IAirportsView : IView<AirportsViewModel>
    {
        event EventHandler OnAirprotsGetData;

        event EventHandler<AirportsEventArgs> OnAirportsUpdateItem;

        event EventHandler<AirportsEventArgs> OnAirportsDeleteItem;

        event EventHandler<AirportsEventArgs> OnAirprotsAddItem;

        event EventHandler OnCountriesGetData;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
