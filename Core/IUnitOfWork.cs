using System;
using System.Threading.Tasks;

namespace Kaizen.Core
{

    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        ICampaignDetailRepository CampaignDetailRepository { get; }

        Task SaveChangesAsync();
    }
}