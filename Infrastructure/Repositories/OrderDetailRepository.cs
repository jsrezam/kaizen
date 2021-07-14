using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(KaizenDbContext context) : base(context) { }

        public async Task<QueryResult<OrderDetail>> GetOrderDetailAsync(int orderId, OrderDetailQuery queryObj)
        {
            var result = new QueryResult<OrderDetail>();

            var query = entities
            .Include(od => od.order)
                .ThenInclude(o => o.CampaignDetail)
                    .ThenInclude(cd => cd.Customer)
            .Where(od => od.OrderId == orderId)
            .AsSplitQuery();

            result.TotalItems = await query.CountAsync();
            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<DashBoardViewModel>> GetOrderDetailRptAsync()
        {
            var query = await entities.AsNoTracking()
            .Select(od => new DashBoardViewModel
            {
                OrderId = od.OrderId,
                ProductId = od.Product.Id.ToString(),
                ProductName = od.Product.Name,
                Quantity = od.Quantity,
                TotalImport = (double?)od.UnitPrice * od.Quantity
            }).ToListAsync();

            return query;
        }
    }
}