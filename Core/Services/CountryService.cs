using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        public CountryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<Country>> GetCountriesAsync()
        {
            return await unitOfWork.CountryRepository.GetCountriesAsync();
        }
    }
}