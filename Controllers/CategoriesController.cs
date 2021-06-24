using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Common;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/categories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.AdminRoleValue)]
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = mapper.Map<CategoryDto, Category>(categoryDto);

            await categoryService.CreateCategoryAsync(category);

            var result = mapper.Map<Category, CategoryDto>(category);

            return Ok(result);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await categoryService.GetCategoryAsync(categoryId);

            if (category == null)
                return NotFound();

            mapper.Map<CategoryDto, Category>(categoryDto, category);

            await categoryService.UpdateCategoryAsync(category);

            var result = mapper.Map<Category, CategoryDto>(category);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CategoryQueryDto categoryQueryDto)
        {
            var categoryQuery = mapper.Map<CategoryQueryDto, CategoryQuery>(categoryQueryDto);
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

    }
}