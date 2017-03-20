namespace BalkanAir.Mvp.News
{
    using System;

    using WebFormsMvp;

    public interface INewsView : IView<NewsViewModel>
    {
        event EventHandler OnNewsGetData;
    }
}
