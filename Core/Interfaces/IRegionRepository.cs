using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<QueryResult<Region>> GetRegionsAsync();
        Task<QueryResult<Region>> GetRegionsByCountryAsync(int countryId);
        Task<string> GetRegionNameByIdAsync(int regionId);
    }
}