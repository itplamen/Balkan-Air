namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Models;
    using Models.TravelClasses;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class TravelClassesController : ApiController
    {
        private readonly ITravelClassesServices travelClassesServices;

        public TravelClassesController(ITravelClassesServices travelClassesServices)
        {
            this.travelClassesServices = travelClassesServices;
        }

        [HttpPost]
        public IHttpActionResult Create(TravelClassRequestModel travelClass)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var travelClassToAdd = Mapper.Map<TravelClass>(travelClass);
            var travelClassId = this.travelClassesServices.AddTravelClass(travelClassToAdd);

            return this.Ok(travelClassId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var travelClasses = this.travelClassesServices.GetAll()
                .OrderBy(t => t.AircraftId)
                .ThenBy(t => t.Type)
                .ProjectTo<TravelClassResponseModel>()
                .ToList();

            return this.Ok(travelClasses);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var travelClass = this.travelClassesServices.GetAll()
                .ProjectTo<TravelClassResponseModel>()
                .FirstOrDefault(t => t.Id == id);

            if (travelClass == null)
            {
                return this.NotFound();
            }

            return this.Ok(travelClass);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return this.BadRequest(ErrorMessages.NULL_OR_EMPTY_TYPE);
            }

            var travelClasses = this.travelClassesServices.GetAll()
                .Where(t => t.Type.ToString().ToLower() == type.ToLower())
                .ProjectTo<TravelClassResponseModel>()
                .ToList();

            return this.Ok(travelClasses);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByAircraftId(int aircraftId)
        {
            if (aircraftId <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var travelClasses = this.travelClassesServices.GetAll()
                .Where(t => t.Aircraft.Id == aircraftId)
                .ProjectTo<TravelClassResponseModel>()
                .ToList();

            return this.Ok(travelClasses);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateTravelClassRequestModel travelClass)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var travelClassToUpdate = Mapper.Map<TravelClass>(travelClass);
            var updatedTravelClass = this.travelClassesServices.UpdateTravelClass(id, travelClassToUpdate);

            if (updatedTravelClass == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedTravelClass.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedTravelClass = this.travelClassesServices.DeleteTravelClass(id);

            if (deletedTravelClass == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedTravelClass.Id);
        }
    }
}
