using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj);
        Task<Product> GetProductAsync(int id);
    }
}