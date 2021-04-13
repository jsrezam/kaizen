using Kaizen.Core.Models;
using Kaizen.Persistence;

namespace Kaizen.Core
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(KaizenDbContext context) : base(context) { }


    }
}