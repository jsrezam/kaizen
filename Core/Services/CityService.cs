using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<City>> GetCitiesByRegionAsync(int regionId)
        {
            return await unitOfWork.CityRepository.GetCitiesByRegionAsync(regionId);
        }
    }
}