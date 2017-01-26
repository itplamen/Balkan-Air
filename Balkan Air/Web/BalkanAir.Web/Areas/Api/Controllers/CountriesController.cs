namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Ninject;

    using BalkanAir.Common;
    using Data.Models;
    using Data.Services.Contracts;
    using Models.Countries;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class CountriesController : ApiController
    {
        [Inject]
        public ICountriesServices CountriesServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(CountryRequestModel country)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var countryToAdd = Mapper.Map<Country>(country);
            var addedCountryId = this.CountriesServices.AddCountry(countryToAdd);

            return this.Ok(addedCountryId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var countries = this.CountriesServices.GetAll()
                .ProjectTo<CountryResponseModel>()
                .OrderBy(c => c.Name);

            return this.Ok(countries);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var country = this.CountriesServices.GetAll()
                .ProjectTo<CountryResponseModel>()
                .FirstOrDefault(c => c.Id == id);

            if (country == null)
            {
                return this.NotFound();
            }

            return this.Ok(country);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(string abbreviation)
        {
            var country = this.CountriesServices.GetAll()
                .ProjectTo<CountryResponseModel>()
                .FirstOrDefault(c => c.Abbreviation.ToLower() == abbreviation.ToLower());

            if (country == null)
            {
                return this.NotFound();
            }

            return this.Ok(country);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateCountryRequestModel country)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var countryToUpdate = Mapper.Map<Country>(country);
            var updatedCountry = this.CountriesServices.UpdateCountry(id, countryToUpdate);

            if (updatedCountry == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedCountry.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedCountry = this.CountriesServices.DeleteCountry(id);

            if (deletedCountry == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCountry.Id);
        }
    }
}