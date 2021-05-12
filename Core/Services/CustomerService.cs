using System.Threading.Tasks;
using System.Linq;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Kaizen.Core.Interfaces;

namespace Kaizen.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<QueryResult<Customer>> GetCustomersAsync(CustomerQuery queryObj)
        {
            return await unitOfWork.CustomerRepository.GetCustomersAsync(queryObj);
        }
        public async Task<QueryResult<AgentCustomer>> GetAgentCustomersAsync(string agentId, CustomerQuery customerQuery)
        {
            var result = new QueryResult<AgentCustomer>();

            var userCampaigns = await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(agentId, new CampaignQuery());

            var query = (from userCampaign in userCampaigns.Items
                         from CampaignDetail in userCampaign.CampaignDetails
                         select new AgentCustomer
                         {
                             Customer = CampaignDetail.Customer,
                             CampaignDetailId = CampaignDetail.Id,
                             CampaignId = CampaignDetail.CampaignId
                         }).AsQueryable();

            if (customerQuery != null)
            {
                if (!string.IsNullOrEmpty(customerQuery.FirstName))
                    query = query.Where(c => c.Customer.FirstName.ToUpper().Trim().Equals(customerQuery.FirstName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.LastName))
                    query = query.Where(c => c.Customer.LastName.ToUpper().Trim().Equals(customerQuery.LastName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.CellPhone))
                    query = query.Where(c => c.Customer.CellPhone.ToUpper().Trim().Equals(customerQuery.CellPhone.ToUpper().Trim()));
            }

            result.TotalItems = query.Count();
            result.Items = query.ToList();
            return result;
        }

        public async Task<bool> isUniqueCellphone(string cellPhone)
        {
            return await unitOfWork.CustomerRepository.isUniqueCellphone(cellPhone);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await unitOfWork.CustomerRepository.AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
        }

    }
}