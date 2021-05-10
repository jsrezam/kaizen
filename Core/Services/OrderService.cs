using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Persistence;

namespace Kaizen.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateOrder(Order order)
        {
            await unitOfWork.OrderRepository.AddAsync(order);
            await unitOfWork.SaveChangesAsync();
        }
        // public async Task SaveOrderDetail(int orderId, IEnumerable<OrderDetailResource> orderDetailsResource)
        // {
        //     await unitOfWork.OrderDetailRepository
        //     .AddRangeAsync((from orderDetailResource in orderDetailsResource
        //                     select new OrderDetail
        //                     {
        //                         OrderId = orderId,
        //                         ProductId = orderDetailResource.ProductId,
        //                         Quantity = orderDetailResource.Quantity,
        //                         UnitPrice = orderDetailResource.UnitPrice
        //                     }).AsEnumerable());
        //     await unitOfWork.SaveChangesAsync();
        // }
    }
}