using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Extensions;
using Kaizen.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Core
{
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(KaizenDbContext context) : base(context) { }

        public async Task<Campaign> GetCampaignAsync(int campaignId)
        {
            return await entities
            .Include(c => c.User)
            .Include(c => c.CampaignDetails)
                .ThenInclude(cd => cd.Customer)
            .SingleOrDefaultAsync(p => p.Id == campaignId);
        }
        public async Task<QueryResult<Campaign>> GetUserCampaignsAsync(string userId, CampaignQuery queryObj)
        {
            var result = new QueryResult<Campaign>();

            var query = entities
            .Where(c => c.UserId.Equals(userId))
            .OrderByDescending(c => c.Id)
            .AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Campaign, object>>>()
            {
                ["finishDate"] = c => c.FinishDate,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
        public async Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery queryObj)
        {
            var result = new QueryResult<Campaign>();

            var query = entities
            .Include(c => c.CampaignDetails)
                .ThenInclude(cd => cd.Customer)
            .Where(c => c.UserId.Equals(userId)
            && c.IsActive
            && c.FinishDate > DateTime.UtcNow)
            .OrderByDescending(c => c.Id)
            .AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Campaign, object>>>()
            {
                ["finishDate"] = c => c.FinishDate,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
        public async Task<QueryResult<Campaign>> GetCampaignsAsync(CampaignQuery queryObj)
        {
            var result = new QueryResult<Campaign>();
            var query = entities
            .Include(c => c.User)
            .AsQueryable();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Campaign, object>>>()
            {
                ["finishDate"] = c => c.FinishDate,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
    }
}