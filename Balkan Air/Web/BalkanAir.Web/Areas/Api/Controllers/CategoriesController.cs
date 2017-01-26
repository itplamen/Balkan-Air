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
    using Models.Categories;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = GlobalConstants.ADMINISTRATOR_ROLE)]
    public class CategoriesController : ApiController
    {
        [Inject]
        public ICategoriesServices CategoriesServices { get; set; }

        [HttpPost]
        public IHttpActionResult Create(CategoryRequestModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryToAdd = Mapper.Map<Category>(category);
            var addedCategoryId = this.CategoriesServices.AddCategory(categoryToAdd);

            return this.Ok(addedCategoryId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var categories = this.CategoriesServices.GetAll()
                .ProjectTo<CategoryResponseModel>();

            return this.Ok(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Api/Categories/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var category = this.CategoriesServices.GetAll()
                .ProjectTo<CategoryResponseModel>()
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.Ok(category);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Api/Categories/{categoryName}")]
        public IHttpActionResult GetCategoryByName(string categoryName)
        {
            var category = this.CategoriesServices.GetAll()
                .ProjectTo<CategoryResponseModel>()
                .FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());

            if (category == null)
            {
                return this.BadRequest("Invalid category name!");
            }

            return this.Ok(category);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateCategoryRequestModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryToUpdate = Mapper.Map<Category>(category);
            var updatedCategory = this.CategoriesServices.UpdateCategory(id, categoryToUpdate);

            if (updatedCategory == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedCategory.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var deletedCategory = this.CategoriesServices.DeleteCategory(id);

            if (deletedCategory == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCategory.Id);
        }
    }
}