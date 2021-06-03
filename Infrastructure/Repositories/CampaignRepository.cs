using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Extensions;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
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
        public async Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery queryObj)
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
        public async Task<QueryResult<Campaign>> GetAgentValidCampaignsAsync(string userId, CampaignQuery queryObj)
        {
            var result = new QueryResult<Campaign>();

            var query = entities
            .Include(cp => cp.CampaignDetails
            .Where(cd => !cd.State.Equals(CampaignStatus.Earned.ToString())))
                .ThenInclude(cp => cp.Customer)
            .Where(cp => cp.UserId.Equals(userId)
            && cp.IsActive
            && cp.FinishDate > DateTime.Now)
            .OrderByDescending(cp => cp.Id)
            .AsSplitQuery();

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