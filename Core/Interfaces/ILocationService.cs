using System.Threading.Tasks;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationNames(int countryId, int regionId, int cityId);
        Task<Location> GetLocationIds(string countryName, string regionName, string cityName);
    }
}