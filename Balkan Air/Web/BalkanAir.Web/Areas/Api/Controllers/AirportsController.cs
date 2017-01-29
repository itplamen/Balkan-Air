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
    using Models.Airports;
    using Services.Data.Contracts;
    using Data.Repositories.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class AirportsController : ApiController
    {
        private readonly IAirportsServices airprotsServices;

        public AirportsController(IAirportsServices airports)
        {
            this.airprotsServices = airports;
        }

        [HttpPost]
        public IHttpActionResult Create(AirportRequestModel airport)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var airportToAdd = Mapper.Map<Airport>(airport);
            var airportId = this.airprotsServices.AddAirport(airportToAdd);

            return this.Ok(airportId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            

            var airports = this.airprotsServices.GetAll()
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
            var airport = this.airprotsServices.GetAll()
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
            var airport = this.airprotsServices.GetAll()
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
            var updatedAirport = this.airprotsServices.UpdateAirport(id, airportToUpdate);

            if (updatedAirport == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedAirport.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedAirprot = this.airprotsServices.DeleteAirport(id);

            if (deletedAirprot == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedAirprot.Id);
        }
    }
}