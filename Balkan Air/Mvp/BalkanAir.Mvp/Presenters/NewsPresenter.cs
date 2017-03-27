namespace BalkanAir.Mvp.Presenters
{
    using System;
    using System.Linq;

    using WebFormsMvp;

    using EventArgs;
    using Services.Data.Contracts;
    using ViewContracts;

    public class NewsPresenter : Presenter<INewsView>
    {
        private readonly INewsServices newsServices;
        private readonly ICategoriesServices categoriesServices;

        public NewsPresenter(INewsView view, INewsServices newsServices, ICategoriesServices categoriesServices)
            : base(view)
        {
            if (newsServices == null)
            {
                throw new ArgumentNullException(nameof(INewsServices));
            }

            if (categoriesServices == null)
            {
                throw new ArgumentNullException(nameof(ICategoriesServices));
            }

            this.newsServices = newsServices;
            this.categoriesServices = categoriesServices;
            
            this.View.OnCategoriesGetData += this.View_OnCategoriesGetData;
            this.View.OnNewsGetData += this.View_OnNewsGetData;
            this.View.OnNewsByCategoryGetData += this.View_OnNewsByCategoryGetData;
        }

        private void View_OnCategoriesGetData(object sender, EventArgs e)
        {
            this.View.Model.Categories = this.categoriesServices.GetAll()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToList();
        }

        private void View_OnNewsGetData(object sender, EventArgs e)
        {
            this.View.Model.News = this.newsServices.GetAll()
                .Where(n => !n.IsDeleted)
                .OrderByDescending(n => n.DateCreated)
                .ToList();
        }

        private void View_OnNewsByCategoryGetData(object sender, NewsEventArgs e)
        {
            this.View.Model.News = this.newsServices.GetAll()
                .Where(a => !a.IsDeleted && a.CategoryId == e.Id)
                .OrderBy(a => a.DateCreated)
                .ToList();
        }
    }
}
