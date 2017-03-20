namespace BalkanAir.Mvp.ViewContracts
{
    using System;

    using WebFormsMvp;

    using EventArgs;
    using Models;

    public interface INewsView : IView<NewsViewModel>
    {
        event EventHandler OnCategoriesGetData;

        event EventHandler OnNewsGetData;

        event EventHandler<NewsEventArgs> OnNewsByCategoryGetData;
    }
}
