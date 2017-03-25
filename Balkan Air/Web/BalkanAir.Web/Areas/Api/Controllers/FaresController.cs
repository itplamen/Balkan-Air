namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using BalkanAir.Common;
    using Data.Models;
    using Models.Fares;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class FaresController : ApiController
    {
        private readonly IFaresServices faresServices;

        public FaresController(IFaresServices faresServices)
        {
            this.faresServices = faresServices;
        }

        [HttpPost]
        public IHttpActionResult Create(FareRequestModel fare)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var fareToAdd = Mapper.Map<Fare>(fare);
            var fareId = this.faresServices.AddFare(fareToAdd);

            return this.Ok(fareId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var fares = this.faresServices.GetAll()
                .OrderBy(f => f.Route.Origin.Name)
                .ThenBy(f => f.Route.Destination.Name)
                .ThenBy(f => f.Price)
                .ProjectTo<FareResponseModel>()
                .ToList();

            return this.Ok(fares);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var fare = this.faresServices.GetAll()
                .ProjectTo<FareResponseModel>()
                .FirstOrDefault(f => f.Id == id);

            if (fare == null)
            {
                return this.NotFound();
            }

            return this.Ok(fare);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetByRoute(string originAbbreviation, string destinationAbbreviation)
        {
            if (string.IsNullOrEmpty(originAbbreviation) || string.IsNullOrEmpty(destinationAbbreviation))
            {
                return this.BadRequest(ErrorMessages.ABBREVIATION_CANNOT_BE_NULL_OR_EMPTY);
            }

            var fares = this.faresServices.GetAll()
                .Where(f => f.Route.Origin.Abbreviation.ToLower() == originAbbreviation.ToLower() &&
                            f.Route.Destination.Abbreviation.ToLower() == destinationAbbreviation.ToLower())
                .OrderBy(f => f.Price)
                .ProjectTo<FareResponseModel>()
                .ToList();

            return this.Ok(fares);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateFareRequestModel fare)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var fareToUpdate = Mapper.Map<Fare>(fare);
            var updatedFare = this.faresServices.UpdateFare(id, fareToUpdate);

            if (updatedFare == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedFare.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedFare = this.faresServices.DeleteFare(id);

            if (deletedFare == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedFare.Id);
        }
    }
}