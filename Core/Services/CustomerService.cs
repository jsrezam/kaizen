using System.Threading.Tasks;
using System.Linq;
using Kaizen.Core.Models;

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

        public async Task<QueryResult<Customer>> GetUserCustomersAsync(string userId, CustomerQuery customerQuery)
        {
            var result = new QueryResult<Customer>();

            var userCampaigns = await unitOfWork.CampaignRepository._GetUserCampaignsAsync(userId, new CampaignQuery());

            var query = (from userCampaign in userCampaigns.Items
                         from CampaignDetail in userCampaign.CampaignDetails
                         select CampaignDetail.Customer).AsQueryable();

            if (customerQuery != null)
            {
                if (!string.IsNullOrEmpty(customerQuery.FirstName))
                    query = query.Where(c => c.FirstName.ToUpper().Trim().Equals(customerQuery.FirstName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.LastName))
                    query = query.Where(c => c.LastName.ToUpper().Trim().Equals(customerQuery.LastName.ToUpper().Trim()));
                if (!string.IsNullOrEmpty(customerQuery.CellPhone))
                    query = query.Where(c => c.CellPhone.ToUpper().Trim().Equals(customerQuery.CellPhone.ToUpper().Trim()));
            }

            result.TotalItems = query.Count();
            result.Items = query.ToList();
            return result;
        }

    }
}