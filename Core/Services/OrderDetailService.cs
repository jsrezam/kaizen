using Kaizen.Core.Interfaces;

namespace Kaizen.Core.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}