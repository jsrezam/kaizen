using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<QueryResult<City>> GetCitiesAsync();
        Task<QueryResult<City>> GetCitiesByRegionAsync(int regionId);
        Task<string> GetCityNameByIdAsync(int cityId);
    }
}