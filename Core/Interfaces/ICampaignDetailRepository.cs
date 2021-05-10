using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICampaignDetailRepository : IRepository<CampaignDetail>
    {
        Task<QueryResult<CampaignDetail>> GetCampaignDetailAsync(int campaignId, CampaignDetailQuery queryObj);
        Task<decimal> GetCustomersInCampaignNumberAsync(int campaignId);
        Task<decimal> GetCalledCustomerNumberInCampaignAsync(int campaignId);
    }
}