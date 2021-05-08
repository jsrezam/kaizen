using Kaizen.Core.Models;
using Kaizen.Persistence;

namespace Kaizen.Core
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(KaizenDbContext context) : base(context) { }
    }
}