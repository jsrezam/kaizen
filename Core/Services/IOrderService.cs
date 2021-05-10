using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
        // Task SaveOrderDetail(int orderId, IEnumerable<OrderDetailResource> orderDetailResource);
    }
}