using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/categories")]
    public class CategoriesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = mapper.Map<CategoryDto, Category>(categoryResource);

            await categoryService.CreateCategoryAsync(category);

            var result = mapper.Map<Category, CategoryDto>(category);

            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await categoryService.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound();

            mapper.Map<CategoryDto, Category>(categoryResource, category);

            await categoryService.UpdateCategoryAsync(category);

            var result = mapper.Map<Category, CategoryDto>(category);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CategoryQueryDto categoryQueryResource)
        {
            var categoryQuery = mapper.Map<CategoryQueryDto, CategoryQuery>(categoryQueryResource);
            var queryResult = await categoryService.GetCategoriesAsync(categoryQuery);
            var resultQuery = mapper.Map<QueryResult<Category>, QueryResultDto<CategoryDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await categoryService.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Category, CategoryDto>(category);
            return Ok(categoryResource);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await categoryService.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound();

            var result = categoryService.DeleteCategoryAsync(category);

            return Ok(result);
        }
    }
}