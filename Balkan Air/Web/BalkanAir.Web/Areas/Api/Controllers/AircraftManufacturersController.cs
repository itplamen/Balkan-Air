namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using BalkanAir.Common;
    using Data.Models;
    using Models.AircraftManufacturers;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class AircraftManufacturersController : ApiController
    {
        private readonly IAircraftManufacturersServices aircraftManufacturersServices;

        public AircraftManufacturersController(IAircraftManufacturersServices aircraftManufacturersServices)
        {
            this.aircraftManufacturersServices = aircraftManufacturersServices;
        }

        [HttpPost]
        public IHttpActionResult Create(AircraftManufacturerRequestModel aircraftManufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var manufacturerToAdd = Mapper.Map<AircraftManufacturer>(aircraftManufacturer);
            var manufacturerId = this.aircraftManufacturersServices.AddManufacturer(manufacturerToAdd);

            return this.Ok(manufacturerId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var aircraftManufacturers = this.aircraftManufacturersServices.GetAll()
                .OrderBy(am => am.Name)
                .ProjectTo<AircraftManufacturerResponseModel>()
                .ToList();

            return this.Ok(aircraftManufacturers);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var aircraftManufacturer = this.aircraftManufacturersServices.GetAll()
                .ProjectTo<AircraftManufacturerResponseModel>()
                .FirstOrDefault(am => am.Id == id);

            if (aircraftManufacturer == null)
            {
                return this.NotFound();
            }

            return this.Ok(aircraftManufacturer);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateAircraftManufacturerRequestModel aircraftManufacturer)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var manufacturerToUpdate = Mapper.Map<AircraftManufacturer>(aircraftManufacturer);
            var updatedManufacturer = this.aircraftManufacturersServices.UpdateManufacturer(id, manufacturerToUpdate);

            if (updatedManufacturer == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedManufacturer.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedManufacturer = this.aircraftManufacturersServices.DeleteManufacturer(id);

            if (deletedManufacturer == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedManufacturer.Id);
        }
    }
}