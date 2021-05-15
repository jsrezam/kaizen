using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await unitOfWork.CategoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            return await unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<QueryResult<Category>> GetCategoriesAsync(CategoryQuery categoryQuery)
        {
            return await unitOfWork.CategoryRepository.GetCategoriesAsync(categoryQuery);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
        }

    }
}