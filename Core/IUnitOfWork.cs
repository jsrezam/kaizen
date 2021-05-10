using System;
using System.Threading.Tasks;

namespace Kaizen.Core
{

    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ICampaignRepository CampaignRepository { get; }
        ICampaignDetailRepository CampaignDetailRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IUserRepository userRepository { get; }

        Task SaveChangesAsync();
    }
}