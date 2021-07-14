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
                ["customer.firstName"] = cd => cd.Customer.FirstName,
                ["customer.lastName"] = cd => cd.Customer.LastName,
                ["customer.cellPhone"] = cd => cd.Customer.LastName,
                ["totalCallsNumber"] = cd => cd.TotalCallsNumber,
                ["lastCallDuration"] = cd => cd.LastCallDuration,
                ["lastCallDate"] = cd => cd.LastCallDate,
                ["lastValidCallDuration"] = cd => cd.LastValidCallDuration,
                ["lastValidCallDate"] = cd => cd.LastValidCallDate,
                ["state"] = cd => cd.State,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }
        public async Task<decimal> GetCustomersInCampaignNumberAsync(int campaignId)
        {
            return await entities.AsNoTracking()
            .Where(cd => cd.CampaignId == campaignId).CountAsync();
        }
        public async Task<decimal> GetCalledCustomerNumberInCampaignAsync(int campaignId)
        {
            var response = await entities.AsNoTracking()
            .Where(cd => cd.CampaignId == campaignId &&
            (cd.State.Equals(CampaignStatus.Called.ToString()) ||
            cd.State.Equals(CampaignStatus.Earned.ToString())))
            .CountAsync();

            return response;
        }

        public async Task<IEnumerable<CampaignDetail>> GetAgentOfersAsync(string agentId)
        {
            return await entities
            .Where(cd => cd.Campaign.UserId.Equals(agentId))
            .Select(cd => new CampaignDetail
            {
                Id = cd.Id,
                CampaignId = cd.CampaignId,
                CustomerId = cd.CustomerId,
                Customer = new Customer { CellPhone = cd.Customer.CellPhone },
                LastCallDate = cd.LastCallDate,
                LastCallDuration = cd.LastCallDuration,
                LastValidCallDate = cd.LastValidCallDate,
                LastValidCallDuration = cd.LastValidCallDuration,
                TotalCallsNumber = cd.TotalCallsNumber,
                State = cd.State
            }).ToListAsync();
        }
    }
}