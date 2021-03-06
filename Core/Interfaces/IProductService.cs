using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<Product> GetProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj);
        Task<QueryResult<Product>> GetValidProducts(ProductQuery productQuery);
    }
}