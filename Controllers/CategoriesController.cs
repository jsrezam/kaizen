using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = mapper.Map<CategoryResource, Category>(categoryResource);

            await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();

            category = await unitOfWork.CategoryRepository.GetByIdAsync(category.Id);

            var result = mapper.Map<Category, CategoryResource>(category);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            mapper.Map<CategoryResource, Category>(categoryResource, category);
            unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            category = await unitOfWork.CategoryRepository.GetByIdAsync(category.Id);
            var result = mapper.Map<Category, CategoryResource>(category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CategoryQueryResource categoryQueryResource)
        {
            var categoryQuery = mapper.Map<CategoryQueryResource, CategoryQuery>(categoryQueryResource);
            var queryResult = await unitOfWork.CategoryRepository.GetCategoriesAsync(categoryQuery);
            var resultQuery = mapper.Map<QueryResult<Category>, QueryResultResource<CategoryResource>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Category, CategoryResource>(category);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            var result = unitOfWork.CategoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();
            return Ok(result);
        }
    }
}