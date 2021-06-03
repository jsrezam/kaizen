using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
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
    }
}