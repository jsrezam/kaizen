using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
        Task<Customer> GetCustomerAsync(int id);
    }
}