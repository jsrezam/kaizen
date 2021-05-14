using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class RegionRepository : BaseRepository<Region>, IRegionRepository
    {
        public RegionRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<Region>> GetRegionsByCountryAsync(int countryId)
        {
            var result = new QueryResult<Region>();
            var query = entities.Where(r => r.CountryId == countryId).AsQueryable();

            result.TotalItems = await query.CountAsync();
            result.Items = await query.OrderBy(c => c.Name).ToListAsync();

            return result;
        }
    }
}