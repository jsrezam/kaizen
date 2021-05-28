using System.Threading.Tasks;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface ILocationService
    {
        Task<LocationViewModel> GetLocationNames(int countryId, int regionId, int cityId);
        Task<LocationViewModel> GetLocationIds(string countryName, string regionName, string cityName);
    }
}