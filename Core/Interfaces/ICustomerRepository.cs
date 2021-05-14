using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> isUniqueCellphone(string cellPhone);
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
    }
}