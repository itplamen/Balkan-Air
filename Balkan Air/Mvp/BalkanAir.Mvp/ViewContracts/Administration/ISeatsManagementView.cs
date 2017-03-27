namespace BalkanAir.Mvp.ViewContracts.Administration
{
    using System;
    using System.Web.ModelBinding;

    using WebFormsMvp;

    using EventArgs.Administration;
    using Models.Administration;

    public interface ISeatsManagementView : IView<SeatsManagementViewModel>
    {
        event EventHandler OnSeatsGetData;

        event EventHandler<SeatsManagementEventArgs> OnSeatsUpdateItem;

        event EventHandler<SeatsManagementEventArgs> OnSeatsDeleteItem;

        event EventHandler<SeatsManagementEventArgs> OnSeatsAddItem;

        event EventHandler OnTravelClassesGetData;

        event EventHandler OnLegInstancesGetData;

        event EventHandler<SeatsManagementEventArgs> OnTravelClassInfoGetItem;

        ModelStateDictionary ModelState { get; }

        bool TryUpdateModel<TModel>(TModel model) where TModel : class;
    }
}
