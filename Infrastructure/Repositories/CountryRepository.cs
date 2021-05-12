using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<Country>> GetCountriesAsync()
        {
            var result = new QueryResult<Country>();

            result.TotalItems = await entities.CountAsync();
            result.Items = await entities.ToListAsync();

            return result;
        }

    }
}