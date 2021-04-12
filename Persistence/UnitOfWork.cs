using System.Threading.Tasks;
using Kaizen.Core;

namespace Kaizen.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        private readonly KaizenDbContext context;

        public UnitOfWork(KaizenDbContext context)
        {
            this.context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(context);

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}