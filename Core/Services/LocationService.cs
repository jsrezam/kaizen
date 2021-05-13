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

        public async Task<Location> GetLocationNames(int countryId, int regionId, int cityId)
        {
            return new Location
            {
                Country = await unitOfWork.CountryRepository.GetCountryNameByIdAsync(countryId),
                Region = await unitOfWork.RegionRepository.GetRegionNameByIdAsync(regionId),
                City = await unitOfWork.CityRepository.GetCityNameByIdAsync(cityId)
            };
        }
        public async Task<Location> GetLocationIds(string countryName, string regionName, string cityName)
        {
            var countries = await unitOfWork.CountryRepository.GetCountriesAsync();
            var regions = await unitOfWork.RegionRepository.GetRegionsAsync();
            var cities = await unitOfWork.CityRepository.GetCitiesAsync();

            return (from c in countries.Items
                    join r in regions.Items on c.Id equals r.CountryId
                    join ct in cities.Items on r.Id equals ct.RegionId
                    where (c.Name.Equals(countryName) && r.Name.Equals(regionName) && ct.Name.Equals(cityName))
                    select new Location
                    {
                        CountryId = c.Id,
                        RegionId = r.Id,
                        CityId = ct.Id
                    }).FirstOrDefault();
        }
    }
}