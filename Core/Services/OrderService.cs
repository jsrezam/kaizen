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
        public async Task SaveOrderDetail(int orderId, IEnumerable<OrderDetailResource> orderDetailResource)
        {
            await unitOfWork.OrderDetailRepository
            .AddRangeAsync((from orderDetail in orderDetailResource
                            select new OrderDetail
                            {
                                OrderId = orderId,
                                ProductId = orderDetail.ProductId,
                                Quantity = orderDetail.Quantity,
                                UnitPrice = orderDetail.UnitPrice
                            }).ToList());
            await unitOfWork.SaveChangesAsync();
        }
    }
}