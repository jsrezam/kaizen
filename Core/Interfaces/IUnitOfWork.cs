using System;
using System.Threading.Tasks;

namespace Kaizen.Core.Interfaces
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
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        ICountryRepository CountryRepository { get; }
        IRegionRepository RegionRepository { get; }
        ICityRepository CityRepository { get; }

        Task SaveChangesAsync();
    }
}