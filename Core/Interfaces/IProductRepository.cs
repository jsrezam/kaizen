using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj);
        Task<Product> GetProductAsync(int id);
        Task<QueryResult<Product>> GetValidProductsAsync(ProductQuery queryObj);
    }
}