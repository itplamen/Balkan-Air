namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Models;
    using Models.FlightLegs;
    using Services.Data.Contracts;

    /// <summary>
    /// This controller must be accessible only from the Administrator.
    /// </summary>

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class FlightLegsController : ApiController
    {
        private readonly IFlightLegsServices flightLegsServices;

        public FlightLegsController(IFlightLegsServices flightLegsServices)
        {
            this.flightLegsServices = flightLegsServices;
        }

        [HttpPost]
        public IHttpActionResult Create(FlightLegRequestModel flightLeg)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var flightLegToAdd = Mapper.Map<FlightLeg>(flightLeg);
            var addedFlightLegId = this.flightLegsServices.AddFlightLeg(flightLegToAdd);

            return this.Ok(addedFlightLegId);
        }
    
        [HttpGet]
        public IHttpActionResult All()
        {
            var flightLegs = this.flightLegsServices.GetAll()
                .OrderByDescending(f => f.ScheduledDepartureDateTime)
                .ProjectTo<FlightLegResponseModel>()
                .ToList();

            return this.Ok(flightLegs);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var flightLeg = this.flightLegsServices.GetAll()
                .ProjectTo<FlightLegResponseModel>()
                .FirstOrDefault(f => f.Id == id);

            if (flightLeg == null)
            {
                return this.NotFound();
            }

            return this.Ok(flightLeg);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateFlightLegRequestModel flightLeg)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var flightLegToUpdate = Mapper.Map<FlightLeg>(flightLeg);
            var updatedFlightLeg = this.flightLegsServices.UpdateFlightLeg(id, flightLegToUpdate);

            if (updatedFlightLeg == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedFlightLeg.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedFlightLeg = this.flightLegsServices.DeleteFlightLeg(id);

            if (deletedFlightLeg == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedFlightLeg.Id);
        }
    }
}