using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<QueryResult<OrderDetail>> GetOrderDetailAsync(int orderId, OrderDetailQuery queryObj);
        Task<IEnumerable<DashBoardViewModel>> GetOrderDetailRptAsync();
    }
}