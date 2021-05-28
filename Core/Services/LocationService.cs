using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork unitOfWork;
        public LocationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<LocationViewModel> GetLocationNames(int countryId, int regionId, int cityId)
        {
            var countries = await unitOfWork.CountryRepository.GetCountriesAsync();
            return (from country in countries.Items
                    from region in country.Regions
                    from city in region.cities
                    where (country.Id == countryId
                        && region.Id == regionId
                        && city.Id == cityId)
                    select new LocationViewModel
                    {
                        Country = country.Name,
                        Region = region.Name,
                        City = city.Name
                    }).FirstOrDefault();
        }

        public async Task<LocationViewModel> GetLocationIds(string countryName, string regionName, string cityName)
        {
            var countries = await unitOfWork.CountryRepository.GetCountriesAsync();
            return (from country in countries.Items
                    from region in country.Regions
                    from city in region.cities
                    where (country.Name.Equals(countryName)
                        && region.Name.Equals(regionName)
                        && city.Name.Equals(cityName))
                    select new LocationViewModel
                    {
                        CountryId = country.Id,
                        RegionId = region.Id,
                        CityId = city.Id
                    }).FirstOrDefault();
        }
    }
}