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
            CreateMap<Category, CategoryResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            // API Resource to Domain
            CreateMap<CategoryResource, Category>();
            CreateMap<CategoryQueryResource, CategoryQuery>();
        }

    }
}