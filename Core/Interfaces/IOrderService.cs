using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(Order order);
    }
}