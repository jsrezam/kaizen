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


            // API Resource to Domain
            CreateMap<ProductResource, Product>()
            .ForMember(p => p.Category, opt => opt.Ignore());
            CreateMap<ProductQueryResource, ProductQuery>();
            CreateMap<CategoryResource, Category>();
            CreateMap<CategoryQueryResource, CategoryQuery>();
            CreateMap<CategoryViewResource, Category>();
        }

    }
}