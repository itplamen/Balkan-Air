namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Models;
    using Models.Users;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    public class UsersController : ApiController
    {
        private readonly IUsersServices usersServices;
        
        public UsersController(IUsersServices usersServices)
        {
            this.usersServices = usersServices;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var users = this.usersServices.GetAll()
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.UserSettings.FirstName)
                .ThenBy(u => u.UserSettings.LastName)
                .ProjectTo<UsersResponseModel>()
                .ToList();

            return this.Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetUsersByGender(string gender)
        {
            if (gender.ToLower() != Gender.Male.ToString().ToLower() && 
                gender.ToLower() != Gender.Female.ToString().ToLower())
            {
                return this.BadRequest(ErrorMessages.INVALID_GENDER);
            }

            var users = this.usersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.Gender.ToString().ToLower() == gender.ToLower())
                .ProjectTo<UsersResponseModel>()
                .ToList();

            return this.Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetUsersByNationality(string nationality)
        {
            if (string.IsNullOrEmpty(nationality))
            {
                return this.BadRequest(ErrorMessages.NULL_OR_EMPTY_NATIONALITY);
            }

            var users = this.usersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.Nationality.ToLower() == nationality.ToLower())
                .ProjectTo<UsersResponseModel>()
                .ToList();

            return this.Ok(users);
        }
    }
}