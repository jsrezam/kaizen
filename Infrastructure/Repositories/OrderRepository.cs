using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Infrastructure.Extensions;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<Order>> GetOrdersAsync(OrderQuery queryObj)
        {
            var result = new QueryResult<Order>();

            var query = entities
            .Include(o => o.CampaignDetail)
                .ThenInclude(o => o.Customer)
            .AsSplitQuery();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Order, object>>>()
            {
                ["orderDate"] = c => c.OrderDate,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<QueryResult<Order>> GetAgentOrdersAsync(string userId, OrderQuery queryObj)
        {
            var result = new QueryResult<Order>();

            var query = entities
            .Include(o => o.CampaignDetail)
                .ThenInclude(o => o.Customer)
            .Where(o => o.CampaignDetail.Campaign.UserId.Equals(userId))
            .OrderByDescending(o => o.OrderDate)
            .AsSplitQuery();

            query = query.ApplyFiltering(queryObj);

            var columnsMap = new Dictionary<string, Expression<Func<Order, object>>>()
            {
                ["campaignDetail.campaignId"] = c => c.CampaignDetail.CampaignId,
                ["campaignDetailId"] = c => c.CampaignDetailId,
                ["customerFirstName"] = c => c.CampaignDetail.Customer.FirstName,
                ["customerLastName"] = c => c.CampaignDetail.Customer.LastName,
                ["customerCellPhone"] = c => c.CampaignDetail.Customer.CellPhone,
                ["orderDate"] = c => c.OrderDate,
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return (await entities
            .Include(o => o.CampaignDetail)
            .ThenInclude(o => o.Customer)
            .Where(o => o.Id == orderId)
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync());
        }
    }

}