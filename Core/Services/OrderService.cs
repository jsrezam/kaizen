using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

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
    }
}