namespace BalkanAir.Web.Areas.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Ninject;

    using Data.Common;
    using Data.Models;
    using Models.Users;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class UsersController : ApiController
    {
        [Inject]
        public IUsersServices UsersServices { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var users = this.UsersServices.GetAll()
                .ProjectTo<UsersResponseModel>();

            return this.Ok(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetUsersByGender(string gender)
        {
            if (gender.ToLower() != Gender.Male.ToString().ToLower() && 
                gender.ToLower() != Gender.Female.ToString().ToLower())
            {
                return this.BadRequest("Invalid gender!");
            }

            var users = this.UsersServices.GetAll()
                .Where(u => !u.IsDeleted && u.UserSettings.Gender.ToString().ToLower() == gender.ToLower())
                .ProjectTo<UsersResponseModel>();

            return this.Ok(users);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public IHttpActionResult GetUsersByNationality(string nationality)
        //{
        //    if (gender.ToLower() != Gender.Male.ToString().ToLower() &&
        //        gender.ToLower() != Gender.Female.ToString().ToLower())
        //    {
        //        return this.BadRequest("Invalid gender!");
        //    }

        //    var users = this.UsersServices.GetAll()
        //        .Where(u => !u.IsDeleted && u.UserSettings.Gender.ToString().ToLower() == gender.ToLower())
        //        .ProjectTo<UsersResponseModel>();

        //    return this.Ok(users);
        //}

    }
}