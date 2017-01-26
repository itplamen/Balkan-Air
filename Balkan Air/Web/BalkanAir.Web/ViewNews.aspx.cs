namespace BalkanAir.Web
{
    using System;
    using System.Linq;
    using System.Web.ModelBinding;
    using System.Web.UI;

    using Ninject;

    using BalkanAir.Web.Common;
    using BalkanAir.Services.Data.Contracts;

    public partial class ViewNews : Page
    {
        [Inject]
        public INewsServices NewsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public BalkanAir.Data.Models.News ViewNewsFormView_GetItem([QueryString]string id)
        {
            int validId;
            bool isValid = int.TryParse(id, out validId);
            int lastNewsId = this.NewsServices.GetAll()
                .OrderByDescending(a => a.Id)
                .First()
                .Id;

            if (isValid && validId > 0 && validId <= lastNewsId)
            {
                return this.NewsServices.GetNews(validId);
            }

            this.Response.Redirect(Pages.NEWS);
            return null;
        }
    }
}