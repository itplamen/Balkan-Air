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
    using Models.AircraftManufacturers;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class AircraftManufacturersController : ApiController
    {
        [Inject]
        public IAircraftManufacturersServices AircraftManufacturersServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(AircraftManufacturerRequestModel aircraftManufacturer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var manufacturerToAdd = Mapper.Map<AircraftManufacturer>(aircraftManufacturer);
            var manufacturerId = this.AircraftManufacturersServices.AddManufacturer(manufacturerToAdd);

            return this.Ok(manufacturerId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var aircraftManufacturers = this.AircraftManufacturersServices.GetAll()
                .ProjectTo<AircraftManufacturerResponseModel>()
                .OrderBy(am => am.Name);

            return this.Ok(aircraftManufacturers);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var aircraftManufacturer = this.AircraftManufacturersServices.GetAll()
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
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var manufacturerToUpdate = Mapper.Map<AircraftManufacturer>(aircraftManufacturer);
            var updatedManufacturer = this.AircraftManufacturersServices.UpdateManufacturer(id, manufacturerToUpdate);

            if (updatedManufacturer == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedManufacturer.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedManufacturer = this.AircraftManufacturersServices.DeleteManufacturer(id);

            if (deletedManufacturer == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedManufacturer.Id);
        }
    }
}