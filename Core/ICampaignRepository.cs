using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<Campaign> GetCampaignAsync(int campaignId);
        Task<QueryResult<Campaign>> GetCampaignsAsync(CampaignQuery queryObj);
        Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery queryObj);
        Task<QueryResult<Campaign>> GetAgentValidCampaignsAsync(string userId, CampaignQuery queryObj);
    }
}