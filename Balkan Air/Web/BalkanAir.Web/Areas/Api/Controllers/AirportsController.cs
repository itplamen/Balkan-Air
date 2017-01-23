namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using BalkanAir.Data.Services.Contracts;
    using BalkanAir.Web.Areas.Api.Models;

    public class AirportsController : ApiController
    {
        private readonly IAirportsServices airports;

        public AirportsController(IAirportsServices airportsServices)
        {
            this.airports = airportsServices;
        }

        [HttpGet]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult All()
        {
            var airports = this.airports.GetAll()
                .Select(AirportResponseModel.FromAirport);

            return this.Ok(airports);
        }
    }
}