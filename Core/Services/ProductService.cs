using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateProductAsync(Product product)
        {
            await unitOfWork.ProductRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await unitOfWork.ProductRepository.GetProductAsync(id);
        }
        public async Task UpdateProductAsync(Product product)
        {
            unitOfWork.ProductRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeleteProductAsync(Product product)
        {
            var response = unitOfWork.ProductRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return response;
        }
        public async Task<QueryResult<Product>> GetProductsAsync(ProductQuery queryObj)
        {
            return await unitOfWork.ProductRepository.GetProductsAsync(queryObj);
        }
        public async Task<QueryResult<Product>> GetValidProducts(ProductQuery productQuery)
        {
            return await unitOfWork.ProductRepository.GetValidProductsAsync(productQuery);
        }
    }
}