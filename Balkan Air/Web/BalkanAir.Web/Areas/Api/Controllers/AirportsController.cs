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
    using Models.Airports;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class AirportsController : ApiController
    {
        [Inject]
        public IAirportsServices AirportsServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(AirportRequestModel airport)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var airportToAdd = Mapper.Map<Airport>(airport);
            var airportId = this.AirportsServices.AddAirport(airportToAdd);

            return this.Ok(airportId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var airports = this.AirportsServices.GetAll()
                .ProjectTo<AirportResponseModel>()
                .OrderBy(a => a.Name)
                .ThenBy(a => a.Abbreviation);

            return this.Ok(airports);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var airport = this.AirportsServices.GetAll()
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
        public IHttpActionResult Get(string abbreviation)
        {
            var airport = this.AirportsServices.GetAll()
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
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var airportToUpdate = Mapper.Map<Airport>(airport);
            var updatedAirport = this.AirportsServices.UpdateAirport(id, airportToUpdate);

            if (updatedAirport == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedAirport.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedAirprot = this.AirportsServices.DeleteAirport(id);

            if (deletedAirprot == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedAirprot.Id);
        }
    }
}