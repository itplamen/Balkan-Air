namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Helper;
    using Data.Models;
    using Models.FlightNumbers;
    using Services.Data.Contracts;

    /// <summary>
    /// This controller must be accessible only from the Administrator.
    /// </summary>

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class FlightNumbersController : ApiController
    {
        private readonly IFlightsServices flightsServices;
        private readonly INumberGenerator numberGenerator;

        public FlightNumbersController(IFlightsServices flightsServices, INumberGenerator numberGenerator)
        {
            this.flightsServices = flightsServices;
            this.numberGenerator = numberGenerator;
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            var flightNumberToAdd = new Flight() { Number = this.numberGenerator.GetUniqueFlightNumber() };
            var addedFlightNumberId = this.flightsServices.AddFlight(flightNumberToAdd);

            return this.Ok(addedFlightNumberId);
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var flightNumbers = this.flightsServices.GetAll()
                .OrderBy(f => f.Number)
                .ProjectTo<FlightNumberResponseModel>()
                .ToList();

            return this.Ok(flightNumbers);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var flightNumber = this.flightsServices.GetAll()
                .ProjectTo<FlightNumberResponseModel>()
                .FirstOrDefault(f => f.Id == id);

            if (flightNumber == null)
            {
                return this.NotFound();
            }

            return this.Ok(flightNumber);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateFlightNumberRequestModel flightNumber)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var flightNumberToUpdate = Mapper.Map<Flight>(flightNumber);
            var updatedFlightNumber = this.flightsServices.UpdateFlight(id, flightNumberToUpdate);

            if (updatedFlightNumber == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedFlightNumber.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedFlightNumber = this.flightsServices.DeleteFlight(id);

            if (deletedFlightNumber == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedFlightNumber.Id);
        }    
    }
}