namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Ninject;

    using Data.Common;
    using Data.Models;
    using BalkanAir.Services.Data.Contracts;
    using Models.Flights;

    [EnableCors("*", "*", "*")]
   // [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class FlightsController : ApiController
    {
        [Inject]
        public IFlightsServices FlightsServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(FlightRequestModel flight)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var flightToAdd = Mapper.Map<Flight>(flight);
            var addedFlightId = this.FlightsServices.AddFlight(flightToAdd);

            return this.Ok(addedFlightId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .OrderByDescending(f => f.Departure);

            return this.Ok(flights);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var flight = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .FirstOrDefault(f => f.Id == id);

            if (flight == null)
            {
                return this.NotFound();
            }

            return this.Ok(flight);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByFlightNumber(string flightNumber)
        {
            var flight = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .FirstOrDefault(f => f.Number.ToLower() == flightNumber.ToLower());

            if (flight == null)
            {
                return this.NotFound();
            }

            return this.Ok(flight);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByFlightStatus(string flightStatus)
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .Where(f => f.FlightStatus.Name.ToLower() == flightStatus.ToLower());

            if (flights == null)
            {
                return this.NotFound();
            }

            return this.Ok(flights);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByDepartureAirport(string airportAbbreviation)
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .Where(f => f.DepartureAirport.Abbreviation.ToLower() == airportAbbreviation.ToLower());

            if (flights == null)
            {
                return this.NotFound();
            }

            return this.Ok(flights);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByArrivalAirport(string airportAbbreviation)
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .Where(f => f.ArrivalAirport.Abbreviation.ToLower() == airportAbbreviation.ToLower());

            if (flights == null)
            {
                return this.NotFound();
            }

            return this.Ok(flights);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByRoute(string departureAbbreviation, string arrivalAbbreviation)
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .Where(f => f.DepartureAirport.Abbreviation.ToLower() == departureAbbreviation.ToLower() && 
                    f.ArrivalAirport.Abbreviation.ToLower() == arrivalAbbreviation.ToLower());

            if (flights == null)
            {
                return this.NotFound();
            }

            return this.Ok(flights);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetFromDateTime(DateTime dateTime)
        {
            var flights = this.FlightsServices.GetAll()
                .ProjectTo<FlightResponseModel>()
                .Where(f => f.Departure.Equals(dateTime));

            if (flights == null)
            {
                return this.NotFound();
            }

            return this.Ok(flights);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, FlightRequestModel flight)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var flightToUpdate = Mapper.Map<Flight>(flight);
            var updatedFlight = this.FlightsServices.UpdateFlight(id, flightToUpdate);

            if (updatedFlight == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedFlight.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedFlight = this.FlightsServices.DeleteFlight(id);

            if (deletedFlight == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedFlight.Id);
        }
    }
}