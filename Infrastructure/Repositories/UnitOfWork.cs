using System.Threading.Tasks;
using Kaizen.Core;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository _categoryRepository;
        public IProductRepository _productRepository;
        public ICustomerRepository _customerRepository;
        public ICampaignRepository _campaignRepository;
        public ICampaignDetailRepository _campaignDetailRepository;
        public ICallLogRepository _callLogRepository;
        public IOrderRepository _orderRepository;
        public IOrderDetailRepository _orderDetailRepository;
        public IUserRepository _userRepository;
        public IAccountRepository _accountRepository;
        public ICountryRepository _countryRepository;
        public IRegionRepository _regionRepository;
        public ICityRepository _cityRepository;


        private readonly KaizenDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UnitOfWork(KaizenDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(context);
        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(context);
        public ICustomerRepository CustomerRepository => _customerRepository ?? new CustomerRepository(context);
        public ICampaignRepository CampaignRepository => _campaignRepository ?? new CampaignRepository(context);
        public ICampaignDetailRepository CampaignDetailRepository => _campaignDetailRepository ?? new CampaignDetailRepository(context);
        public ICallLogRepository CallLogRepository => _callLogRepository ?? new CallLogRepository(context);
        public IOrderRepository OrderRepository => _orderRepository ?? new OrderRepository(context);
        public IOrderDetailRepository OrderDetailRepository => _orderDetailRepository ?? new OrderDetailRepository(context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(context, userManager);
        public IAccountRepository AccountRepository => _accountRepository ?? new AccountRepository(context, userManager, signInManager);
        public ICountryRepository CountryRepository => _countryRepository ?? new CountryRepository(context);
        public IRegionRepository RegionRepository => _regionRepository ?? new RegionRepository(context);
        public ICityRepository CityRepository => _cityRepository ?? new CityRepository(context);


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