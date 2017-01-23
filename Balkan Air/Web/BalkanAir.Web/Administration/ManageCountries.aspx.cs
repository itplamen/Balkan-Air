namespace BalkanAir.Web.Administration
{
    using System;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Data.Services.Contracts;

    public partial class ManageCountries : Page
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public IQueryable<Country> ManageCountriesGridView_GetData()
        {
            return this.CountriesServices.GetAll()
                .OrderBy(c => !c.IsDeleted);
        }

        public void ManageCountriesGridView_UpdateItem(int id)
        {
            var country = this.CountriesServices.GetCountry(id);

            if (country == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(country);
            if (ModelState.IsValid)
            {
                this.CountriesServices.UpdateCountry(id, country);
            }
        }

        public void ManageCountriesGridView_DeleteItem(int id)
        {
            this.CountriesServices.DeleteCountry(id);
        }

        protected void CreateCountrytBtn_Click(object sender, EventArgs e)
        {
            var country = new Country() { Name = this.CountryNameTextBox.Text };
            this.CountriesServices.AddCountry(country);
        }
    }
}