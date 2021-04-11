using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{

    public interface ICategoryRepository : IRepository<Category>
    {
        Task<QueryResult<Category>> GetCategoriesAsync(CategoryQuery queryObj);
    }
}