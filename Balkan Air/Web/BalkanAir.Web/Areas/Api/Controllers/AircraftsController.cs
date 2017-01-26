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
    using Models.Aircrafts;

    [EnableCors("*", "*", "*")]
    [Authorize (Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class AircraftsController : ApiController
    {
        [Inject]
        public IAircraftsServices AircraftsServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(AircraftRequesModel aircraft)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var aircraftToAdd = Mapper.Map<Aircraft>(aircraft);
            var addedAircraftId = this.AircraftsServices.AddAircraft(aircraftToAdd);

            return this.Ok(addedAircraftId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var aircrafts = this.AircraftsServices.GetAll()
                .ProjectTo<AircraftResponseModel>()
                .OrderBy(a => a.Id);

            return this.Ok(aircrafts);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var aircraft = this.AircraftsServices.GetAll()
                .ProjectTo<AircraftResponseModel>()
                .FirstOrDefault(a => a.Id == id);

            if (aircraft == null)
            {
                return this.NotFound();
            }

            return this.Ok(aircraft);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateAircraftRequestModel aircraft)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var aircraftToUpdate = Mapper.Map<Aircraft>(aircraft);
            var updatedAircraft = this.AircraftsServices.UpdateAircraft(id, aircraftToUpdate);

            if (updatedAircraft == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedAircraft.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedAircraft = this.AircraftsServices.DeleteAircraft(id);

            if (deletedAircraft == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedAircraft.Id);
        }
    }
}