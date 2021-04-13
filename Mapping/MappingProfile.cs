using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;


namespace Kaizen.Mapping
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
            CreateMap<Employee, EmployeeResource>();
            CreateMap<Employee, EmployeeViewResourse>();
            CreateMap<Customer, CustomerResource>();

            // API Resource to Domain
            CreateMap<ProductResource, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.Category, opt => opt.Ignore());
            CreateMap<ProductQueryResource, ProductQuery>();
            CreateMap<CategoryResource, Category>()
            .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CategoryQueryResource, CategoryQuery>();
            CreateMap<CategoryViewResource, Category>();
            CreateMap<EmployeeResource, Employee>()
            .ForMember(e => e.Id, opt => opt.Ignore());
            CreateMap<EmployeeViewResourse, Employee>();
            CreateMap<EmployeeQueryResource, EmployeeQuery>();
            CreateMap<CustomerResource, Customer>()
            .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CustomerQueryResource, CustomerQuery>();
        }

    }
}