using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domian to API Resource
            CreateMap<Product, ProductResource>();
            CreateMap<ProductQuery, ProductQueryResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Category, CategoryViewResource>();
            CreateMap<Customer, CustomerResource>();
            CreateMap<Customer, CustomerViewResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<ApplicationUser, ApplicationUserResource>();
            CreateMap<Campaign, CampaignResource>();
            CreateMap<CampaignDetail, CampaignDetailResource>();
            CreateMap<Campaign, CampaignSaveResource>();
            CreateMap<AgentCustomer, AgentCustomerResource>()
            .AfterMap((ac, acr) =>
            {
                acr.Customer.CampaignId = acr.CampaignId;
                acr.Customer.CampaignDetailId = acr.CampaignDetailId;
                acr.CampaignId = 0;
                acr.CampaignDetailId = 0;
            });

            // API Resource to Domain
            CreateMap<ProductResource, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.Category, opt => opt.Ignore());
            CreateMap<ProductQueryResource, ProductQuery>();
            CreateMap<CategoryResource, Category>()
            .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CategoryQueryResource, CategoryQuery>();
            CreateMap<CategoryViewResource, Category>();
            CreateMap<CustomerResource, Customer>()
            .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CustomerQueryResource, CustomerQuery>();
            CreateMap<OrderResource, Order>();
            CreateMap<ApplicationUserResource, ApplicationUser>();
            CreateMap<CampaignResource, Campaign>();
            CreateMap<CampaignQueryResource, CampaignQuery>();
            CreateMap<CampaignSaveResource, Campaign>();
            CreateMap<CampaignDetailQueryResource, CampaignDetailQuery>();
            CreateMap<CallLogResource, CallLog>();
            CreateMap<OrderSaveResource, Order>();
            CreateMap<OrderDetailResource, OrderDetail>();
        }
    }
}