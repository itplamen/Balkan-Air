namespace BalkanAir.Web.App_Start.PresenterFactories
{
    using System;

    using WebFormsMvp;

    public interface ICustomPresenterFactory
    {
        IPresenter GetPresenter(Type presenterType, IView viewInstance);
    }
}