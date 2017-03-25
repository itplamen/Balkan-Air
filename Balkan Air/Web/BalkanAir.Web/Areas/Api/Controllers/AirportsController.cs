namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using BalkanAir.Common;
    using Data.Models;
    using Models.Airports;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class AirportsController : ApiController
    {
        private readonly IAirportsServices airportsServices;

        public AirportsController(IAirportsServices airportsServices)
        {
            this.airportsServices = airportsServices;
        }

        [HttpPost]
        public IHttpActionResult Create(AirportRequestModel airport)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var airportToAdd = Mapper.Map<Airport>(airport);
            var airportId = this.airportsServices.AddAirport(airportToAdd);

            return this.Ok(airportId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var airports = this.airportsServices.GetAll()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation)
                .ProjectTo<AirportResponseModel>()
                .ToList();

            return this.Ok(airports);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var airport = this.airportsServices.GetAll()
                .ProjectTo<AirportResponseModel>()
                .FirstOrDefault(a => a.Id == id);

            if (airport == null)
            {
                return this.NotFound();
            }

            return this.Ok(airport);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByAbbreviation(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation))
            {
                return this.BadRequest(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY);
            }

            var airport = this.airportsServices.GetAll()
                .ProjectTo<AirportResponseModel>()
                .FirstOrDefault(a => a.Abbreviation.ToLower() == abbreviation.ToLower());

            if (airport == null)
            {
                return this.NotFound();
            }

            return this.Ok(airport);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateAirportRequestModel airport)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var airportToUpdate = Mapper.Map<Airport>(airport);
            var updatedAirport = this.airportsServices.UpdateAirport(id, airportToUpdate);

            if (updatedAirport == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedAirport.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedAirprot = this.airportsServices.DeleteAirport(id);

            if (deletedAirprot == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedAirprot.Id);
        }
    }
}