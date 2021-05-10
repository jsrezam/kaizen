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
    public class CampaignDetailRepository : BaseRepository<CampaignDetail>, ICampaignDetailRepository
    {
        public CampaignDetailRepository(KaizenDbContext context) : base(context) { }
        public async Task<QueryResult<CampaignDetail>> GetCampaignDetailAsync(int campaignId, CampaignDetailQuery queryObj)
        {
            var result = new QueryResult<CampaignDetail>();

            var query = entities
            .Include(cd => cd.Customer)
            .Where(cd => cd.CampaignId == campaignId)
            .AsQueryable();

            query = query.ApplyFiltering(queryObj);
            var columnsMap = new Dictionary<string, Expression<Func<CampaignDetail, object>>>()
            {
                ["status"] = cd => cd.Status
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
        public async Task<decimal> GetCustomersInCampaignNumberAsync(int campaignId)
        {
            return await entities
            .Where(cd => cd.CampaignId == campaignId).CountAsync();
        }
        public async Task<decimal> GetCalledCustomerNumberInCampaignAsync(int campaignId)
        {
            return await entities
            .Where(cd => cd.CampaignId == campaignId
            && cd.Status.Equals(CampaignStatus.Called.ToString())).CountAsync();
        }
    }
}