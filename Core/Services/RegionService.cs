using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork unitOfWork;
        public RegionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<Region>> GetRegionsByCountryAsync(int countryId)
        {
            return await unitOfWork.RegionRepository.GetRegionsByCountryAsync(countryId);
        }
    }
}