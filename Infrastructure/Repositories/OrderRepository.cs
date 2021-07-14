using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
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

            var query = entities.AsNoTracking()
            .Where(o => o.CampaignDetail.Campaign.UserId.Equals(userId))
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new Order
            {
                Id = o.Id,
                CampaignDetail = new CampaignDetail
                {
                    Id = o.CampaignDetail.Id,
                    CampaignId = o.CampaignDetail.CampaignId,
                    Customer = new Customer
                    {
                        FirstName = o.CampaignDetail.Customer.FirstName,
                        LastName = o.CampaignDetail.Customer.LastName,
                        CellPhone = o.CampaignDetail.Customer.CellPhone,
                    }
                },
                OrderDate = o.OrderDate
            });

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
            .FirstAsync());
        }

        public async Task<IEnumerable<DashBoardViewModel>> GetTotalSalesByMonthAsync()
        {
            var cultureInfo = new CultureInfo("en-US");
            var query = await entities.AsNoTracking()
            .Select(o => new DashBoardViewModel
            {
                Year = o.OrderDate.Year,
                Month = o.OrderDate.ToString("MMMM", cultureInfo),
                TotalImport = o.OrderDetails.Select(od => (double?)od.UnitPrice * od.Quantity).Sum()
            }).ToListAsync();

            return query;
        }

        public async Task<IEnumerable<DashBoardViewModel>> GetTotalSalesByAgentAsync()
        {
            var query = await entities.AsNoTracking()
            .Select(o => new DashBoardViewModel
            {
                AgentId = o.CampaignDetail.Campaign.UserId,
                AgentName = $"{o.CampaignDetail.Campaign.User.FirstName} {o.CampaignDetail.Campaign.User.LastName}",
                TotalImport = o.OrderDetails.Select(od => (double?)od.UnitPrice * od.Quantity).Sum()
            }).ToListAsync();

            return query;
        }

        public async Task<IEnumerable<DashBoardViewModel>> GetTopCustomersByMonthAsync()
        {
            var query = await entities.AsNoTracking()
            .Select(o => new DashBoardViewModel
            {
                OrderDate = o.OrderDate,
                CustomerId = o.CampaignDetail.Customer.Id,
                CustomerName = $"{o.CampaignDetail.Customer.FirstName} {o.CampaignDetail.Customer.LastName}",
                OrderId = o.Id,
                TotalImport = o.OrderDetails.Select(od => (double?)od.UnitPrice * od.Quantity).Sum()
            }).ToListAsync();

            return query;
        }

        public async Task<IEnumerable<DashBoardViewModel>> GetTopAgentAsync()
        {
            var query = await entities.AsNoTracking()
            .Select(o => new DashBoardViewModel
            {
                OrderId = o.Id,
                AgentId = o.CampaignDetail.Campaign.UserId,
                AgentName = $"{o.CampaignDetail.Campaign.User.FirstName} {o.CampaignDetail.Campaign.User.LastName}",
                Email = o.CampaignDetail.Campaign.User.Email,
                TotalImport = o.OrderDetails.Select(od => (double?)od.UnitPrice * od.Quantity).Sum()
            }).ToListAsync();

            return query;
        }

    }
}