using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> isUniqueCellphone(string cellPhone);
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
        Task<QueryResult<Customer>> GetCustomersInProgressCampaign(string agentId);
        QueryResult<AgentCustomerViewModel> GetAgentCustomers(QueryResult<Campaign> campaigns, CustomerQuery queryObj);
    }
}