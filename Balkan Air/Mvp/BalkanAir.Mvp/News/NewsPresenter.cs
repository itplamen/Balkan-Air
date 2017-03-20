namespace BalkanAir.Mvp.News
{
    using WebFormsMvp;

    using Services.Data.Contracts;

    public class NewsPresenter : Presenter<INewsView>
    {
        //private readonly ICategoriesServices categoriesServices;
        private readonly INewsServices newsServices;

        public NewsPresenter(INewsView view, INewsServices newsServices) 
            : base(view)
        {
            //this.categoriesServices = categoriesServices;
            this.newsServices = newsServices;

            this.View.OnNewsGetData += this.View_OnNewsGetData;
        }

        private void View_OnNewsGetData(object sender, System.EventArgs e)
        {
            this.View.Model.News = this.newsServices.GetAll();
        }
    }
}
