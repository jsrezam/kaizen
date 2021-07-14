using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
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
            .Where(cp => cp.UserId.Equals(userId)
                    && cp.IsActive
                    && cp.FinishDate > DateTime.Now)
            .OrderByDescending(cp => cp.Id)
            .Select(
                c => new Campaign
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    FinishDate = c.FinishDate,
                    IsActive = c.IsActive,
                    CampaignDetails = (ICollection<CampaignDetail>)c.CampaignDetails
                    .Where(cd => !cd.State.Equals(CampaignStatus.Earned.ToString()))
                    .Select(
                        cd => new CampaignDetail
                        {
                            Id = cd.Id,
                            CampaignId = cd.CampaignId,
                            CustomerId = cd.CustomerId,
                            Customer = new Customer
                            {
                                Id = cd.Customer.Id,
                                FirstName = cd.Customer.FirstName,
                                LastName = cd.Customer.LastName,
                                IdentificationCard = cd.Customer.IdentificationCard,
                                Email = cd.Customer.Email,
                                Address = cd.Customer.Address,
                                City = cd.Customer.City,
                                Region = cd.Customer.Region,
                                PostalCode = cd.Customer.PostalCode,
                                Country = cd.Customer.Country,
                                HomePhone = cd.Customer.HomePhone,
                                CellPhone = cd.Customer.CellPhone,
                                State = cd.Customer.State
                            },
                            LastCallDate = cd.LastCallDate,
                            LastCallDuration = cd.LastCallDuration,
                            LastValidCallDate = cd.LastValidCallDate,
                            LastValidCallDuration = cd.LastValidCallDuration,
                            TotalCallsNumber = cd.TotalCallsNumber,
                            State = cd.State
                        }
                    )
                }
            );

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

        public async Task<bool> IsOnCampaignInProgress(string agentId)
        {
            return await entities.Where(c => c.UserId.Equals(agentId) && c.IsActive).AnyAsync();
        }

    }
}