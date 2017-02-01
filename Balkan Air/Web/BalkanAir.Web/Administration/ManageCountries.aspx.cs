namespace BalkanAir.Web.Administration
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;

    using Ninject;

    using Data.Models;
    using Services.Data.Contracts;

    public partial class ManageCountries : Page
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        public IQueryable<Country> ManageCountriesGridView_GetData()
        {
            return this.CountriesServices.GetAll()
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Abbreviation);
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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CreateCountrytBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var doesAbbreviationExist = this.CountriesServices.GetAll()
                    .Any(c => c.Abbreviation.ToLower() == this.AbbreviationNameTextBox.Text.ToLower());

                if (doesAbbreviationExist)
                {
                    this.AbbreviationNameTextBox.BorderColor = Color.Red;
                    return;
                }

                var country = new Country()
                {
                    Name = this.CountryNameTextBox.Text,
                    Abbreviation = this.AbbreviationNameTextBox.Text.ToUpper()
                };

                this.CountriesServices.AddCountry(country);
                this.AbbreviationNameTextBox.BorderColor = Color.Empty;
            }
        }
    }
}