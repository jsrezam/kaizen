using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICountryService
    {
        Task<QueryResult<Country>> GetCountriesAsync();
    }
}