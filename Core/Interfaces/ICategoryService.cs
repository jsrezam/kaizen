using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Category category);
        Task<Category> GetCategoryAsync(int categoryId);
        Task<QueryResult<Category>> GetCategoriesAsync(CategoryQuery categoryQuery);
        Task UpdateCategoryAsync(Category category);
    }
}