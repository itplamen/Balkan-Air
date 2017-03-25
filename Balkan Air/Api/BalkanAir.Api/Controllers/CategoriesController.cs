namespace BalkanAir.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Common;
    using Data.Models;
    using Models.Categories;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [Authorize(Roles = UserRolesConstants.ADMINISTRATOR_ROLE)]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesServices categoriesServices;

        public CategoriesController(ICategoriesServices categoriesServices)
        {
            this.categoriesServices = categoriesServices;
        }

        [HttpPost]
        [Route("Api/Categories/Create")]
        public IHttpActionResult Create(CategoryRequestModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryToAdd = Mapper.Map<Category>(category);
            var addedCategoryId = this.categoriesServices.AddCategory(categoryToAdd);

            return this.Ok(addedCategoryId);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult All()
        {
            var categories = this.categoriesServices.GetAll()
                .ProjectTo<CategoryResponseModel>()
                .ToList();

            return this.Ok(categories);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Api/Categories/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var category = this.categoriesServices.GetAll()
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
            if (string.IsNullOrEmpty(categoryName))
            {
                return this.BadRequest(ErrorMessages.NULL_OR_EMPTY_ENTITY_NAME);
            }

            var category = this.categoriesServices.GetAll()
                .ProjectTo<CategoryResponseModel>()
                .FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());

            if (category == null)
            {
                return this.BadRequest(ErrorMessages.INVALID_CATEGORY_NAME);
            }

            return this.Ok(category);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, UpdateCategoryRequestModel category)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryToUpdate = Mapper.Map<Category>(category);
            var updatedCategory = this.categoriesServices.UpdateCategory(id, categoryToUpdate);

            if (updatedCategory == null)
            {
                return this.NotFound();
            }

            return this.Ok(updatedCategory.Id);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest(ErrorMessages.INVALID_ID);
            }

            var deletedCategory = this.categoriesServices.DeleteCategory(id);

            if (deletedCategory == null)
            {
                return this.NotFound();
            }

            return this.Ok(deletedCategory.Id);
        }
    }
}