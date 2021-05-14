using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<QueryResult<Region>> GetRegionsByCountryAsync(int countryId);
    }
}