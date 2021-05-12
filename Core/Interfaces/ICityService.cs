using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICityService
    {
        Task<QueryResult<City>> GetCitiesByRegionAsync(int regionId);
    }
}