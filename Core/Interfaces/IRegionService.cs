using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IRegionService
    {
        Task<QueryResult<Region>> GetRegionsByCountryAsync(int countryId);
    }
}