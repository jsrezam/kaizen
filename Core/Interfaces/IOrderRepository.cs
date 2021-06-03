using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<QueryResult<Order>> GetOrdersAsync(OrderQuery queryObj);
        Task<QueryResult<Order>> GetAgentOrdersAsync(string userId, OrderQuery queryObj);
        Task<Order> GetOrderAsync(int orderId);
    }
}