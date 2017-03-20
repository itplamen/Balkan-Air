namespace BalkanAir.Mvp.ViewContracts
{
    using System;

    using Models;
    using WebFormsMvp;

    public interface ICategoriesView : IView<CategoriesViewModel>
    {
        event EventHandler OnSortedCategoriesGetData;
    }
}
