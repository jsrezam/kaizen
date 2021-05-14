using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<QueryResult<Country>> GetCountriesAsync();
    }
}