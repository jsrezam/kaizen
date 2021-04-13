using System.Threading.Tasks;
using Kaizen.Core;

namespace Kaizen.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public IEmployeeRepository _employeeRepository;
        public ICustomerRepository _customerRepository;
        public IOrderRepository _orderRepository;
        private readonly KaizenDbContext context;

        public UnitOfWork(KaizenDbContext context)
        {
            this.context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(context);
        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? new EmployeeRepository(context);
        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(context);
        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(context);

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