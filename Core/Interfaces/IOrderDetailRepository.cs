using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<QueryResult<OrderDetail>> GetOrderDetailAsync(int orderId, OrderDetailQuery queryObj);
    }
}