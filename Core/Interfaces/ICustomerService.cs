using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
        Task<QueryResult<AgentCustomer>> GetAgentCustomersAsync(string agentId, CustomerQuery customerQuery);
    }
}