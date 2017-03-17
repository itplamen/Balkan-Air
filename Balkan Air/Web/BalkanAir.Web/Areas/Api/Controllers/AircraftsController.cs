namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data.Common;
    using Data.Models;
    using Models.Aircrafts;
    using Services.Common;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize (Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class AircraftsController : ApiController
    {
        private readonly IAircraftsServices aircraftsServices;

        public AircraftsController(IAircraftsServices aircraftsServices)
        {
            this.aircraftsServices = aircraftsServices;
        }

        [HttpPost]
        public IHttpActionResult Create(AircraftRequesModel aircraft)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var aircraftToAdd = Mapper.Map<Aircraft>(aircraft);
            var addedAircraftId = this.aircraftsServices.AddAircraft(aircraftToAdd);

            return this.Ok(addedAircraftId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var aircrafts = this.aircraftsServices.GetAll()
                .OrderBy(a => a.Id)
                .ProjectTo<AircraftResponseModel>()
                .ToList();

            return this.Ok(aircrafts);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var aircraft = this.aircraftsServices.GetAll()
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
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var aircraftToUpdate = Mapper.Map<Aircraft>(aircraft);
            var updatedAircraft = this.aircraftsServices.UpdateAircraft(id, aircraftToUpdate);

            if (updatedAircraft == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedAircraft.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedAircraft = this.aircraftsServices.DeleteAircraft(id);

            if (deletedAircraft == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedAircraft.Id);
        }
    }
}