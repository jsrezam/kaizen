using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> isUniqueCellphone(string cellPhone);
        Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj);
        Task<QueryResult<AgentCustomer>> GetAgentCustomersAsync(string agentId, CustomerQuery customerQuery);
        Task CreateCustomer(Customer customer);
        Task<CustomerDto> GetLocationNames(CustomerDto customerDto);
        Task<Customer> GetCustomerAsync(int customerId);
        Task<CustomerDto> GetLocationIds(CustomerDto customerDto);
        Task UpdateCustomerAsync(Customer customer);
        Task<QueryResult<Customer>> GetRandomCustomersAsync(int maxRange);
        Task<QueryResult<Customer>> GetNoInCampaignCustomersAsync(int campaignId, CustomerQuery queryObj);
    }
}