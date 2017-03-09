namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data.Common;
    using Data.Models;
    using Models.Countries;
    using Services.Data.Contracts;
    using Services.Common;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class CountriesController : ApiController
    {
        private readonly ICountriesServices countriesServices;

        public CountriesController(ICountriesServices countriesServices)
        {
            this.countriesServices = countriesServices;
        }

        [HttpPost]
        public IHttpActionResult Create(CountryRequestModel country)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var countryToAdd = Mapper.Map<Country>(country);
            var addedCountryId = this.countriesServices.AddCountry(countryToAdd);

            return this.Ok(addedCountryId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var countries = this.countriesServices.GetAll()
                .OrderBy(c => c.Name)
                .ProjectTo<CountryResponseModel>()
                .ToList();

            return this.Ok(countries);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var country = this.countriesServices.GetAll()
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
        public IHttpActionResult GetByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation))
            {
                return this.BadRequest(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY);
            }

            var country = this.countriesServices.GetAll()
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
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var countryToUpdate = Mapper.Map<Country>(country);
            var updatedCountry = this.countriesServices.UpdateCountry(id, countryToUpdate);

            if (updatedCountry == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedCountry.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedCountry = this.countriesServices.DeleteCountry(id);

            if (deletedCountry == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCountry.Id);
        }
    }
}