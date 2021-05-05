using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
        Task<Campaign> GetCampaignAsync(int campaignId);
        Task<QueryResult<Campaign>> GetCampaignsAsync(CampaignQuery queryObj);
        Task<QueryResult<Campaign>> GetUserCampaignsAsync(string userId, CampaignQuery queryObj);
        Task<QueryResult<Campaign>> _GetUserCampaignsAsync(string userId, CampaignQuery queryObj);
    }
}