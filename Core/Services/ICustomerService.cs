using System.Linq;
using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public interface ICustomerService
    {
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
        Task<QueryResult<Customer>> GetUserCustomersAsync(string userId, CustomerQuery customerQuery);
    }
}