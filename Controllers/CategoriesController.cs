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
        private readonly IUnitOfWork unitOfWork;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = mapper.Map<CategoryDto, Category>(categoryResource);

            await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Category, CategoryDto>(category);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            mapper.Map<CategoryDto, Category>(categoryResource, category);
            unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            var result = mapper.Map<Category, CategoryDto>(category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CategoryQueryDto categoryQueryResource)
        {
            var categoryQuery = mapper.Map<CategoryQueryDto, CategoryQuery>(categoryQueryResource);
            var queryResult = await unitOfWork.CategoryRepository.GetCategoriesAsync(categoryQuery);
            var resultQuery = mapper.Map<QueryResult<Category>, QueryResultDto<CategoryDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Category, CategoryDto>(category);
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