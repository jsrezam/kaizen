using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(Order order);
        Task<QueryResult<Order>> GetOrdersAsync(OrderQuery queryObj);
        Task<QueryResult<Order>> GetAgentOrdersAsync(string userId, OrderQuery queryObj);
        Task<OrderViewModel> GetOrderDetailAsync(int orderId);
    }
}