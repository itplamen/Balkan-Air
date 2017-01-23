namespace BalkanAir.Web.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Ninject;

    using Data.Models;
    using Data.Services.Contracts;

    public partial class CreditCards : Page
    {
        [Inject]
        public ICreditCardsServices CreditCardsServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                 
            }
        }

        public IQueryable<CreditCard> CreditCardsListView_GetData()
        {
            return this.CreditCardsServices.GetAll()
                .Where(c => !c.IsDeleted);
        }

        public void CreditCardsListView_DeleteItem(int id)
        {
            this.CreditCardsServices.Delete(id);
        }
    }
}