using System;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domian to API Resource
            CreateMap<Product, ProductDto>();
            CreateMap<ProductQuery, ProductQueryDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));
            CreateMap<Category, CategoryViewDto>();
            CreateMap<Customer, CustomerDto>();

            CreateMap<Customer, CustomerViewDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<Campaign, CampaignDto>()
            .AfterMap((c, cd) =>
            {
                cd.ModelFinishDate = new ModelDateDto
                {
                    Year = c.FinishDate.Year,
                    Month = c.FinishDate.Month,
                    Day = c.FinishDate.Day
                };
            });
            CreateMap<CampaignDetail, CampaignDetailDto>();
            CreateMap<Campaign, CampaignSaveDto>();
            CreateMap<AgentCustomer, AgentCustomerDto>()
            .AfterMap((ac, acr) =>
            {
                acr.Customer.CampaignId = acr.CampaignId;
                acr.Customer.CampaignDetailId = acr.CampaignDetailId;
                acr.CampaignId = 0;
                acr.CampaignDetailId = 0;
            });
            CreateMap<Country, CountryDto>();
            CreateMap<Region, RegionDto>();
            CreateMap<City, CityDto>();

            // API Resource to Domain
            CreateMap<ProductDto, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.Category, opt => opt.Ignore());
            CreateMap<ProductQueryDto, ProductQuery>();
            CreateMap<CategoryDto, Category>()
            .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CategoryQueryDto, CategoryQuery>();
            CreateMap<CategoryViewDto, Category>();
            CreateMap<CustomerDto, Customer>();
            // .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CustomerQueryDto, CustomerQuery>();
            CreateMap<OrderDto, Order>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<CampaignDto, Campaign>()
            .AfterMap((cd, c) =>
            {
                c.FinishDate = new DateTime(
                    cd.ModelFinishDate.Year,
                    cd.ModelFinishDate.Month,
                    cd.ModelFinishDate.Day);
            });
            CreateMap<CampaignQueryDto, CampaignQuery>();
            CreateMap<CampaignSaveDto, Campaign>();
            CreateMap<CampaignDetailQueryDto, CampaignDetailQuery>();
            CreateMap<CallLogDto, CallLog>();
            CreateMap<OrderSaveDto, Order>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<UserCredentialsDto, ApplicationUser>()
            .AfterMap((ucd, au) =>
            {
                au.Email = ucd.UserName;
                au.IsActive = true;
            });
        }
    }
}