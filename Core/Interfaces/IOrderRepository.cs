using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<QueryResult<Order>> GetOrdersAsync(OrderQuery queryObj);
        Task<QueryResult<Order>> GetAgentOrdersAsync(string userId, OrderQuery queryObj);
        Task<Order> GetOrderAsync(int orderId);
        Task<IEnumerable<DashBoardViewModel>> GetTotalSalesByMonthAsync();
        Task<IEnumerable<DashBoardViewModel>> GetTotalSalesByAgentAsync();
        Task<IEnumerable<DashBoardViewModel>> GetTopCustomersByMonthAsync();
        Task<IEnumerable<DashBoardViewModel>> GetTopAgentAsync();
    }
}