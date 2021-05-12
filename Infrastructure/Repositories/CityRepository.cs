using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<City>> GetCitiesByRegionAsync(int regionId)
        {
            var result = new QueryResult<City>();
            var query = entities.Where(c => c.RegionId == regionId).AsQueryable();

            result.TotalItems = await query.CountAsync();
            result.Items = await query.ToListAsync();

            return result;
        }

    }
}