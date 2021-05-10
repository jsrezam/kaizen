using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public interface ICampaignService
    {
        Task CreateCampaignAsync(Campaign campaign);
        Task AddCampaignDetailAsync(int CampaignId, IEnumerable<CustomerResource> customersResource);
        Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery campaignQuery);
        Task<QueryResult<Campaign>> GetAgentValidCampaignsAsync(string agentId, CampaignQuery campaignQuery);
        Task<Campaign> GetCampaignAsync(int campaignId);
        Task UpdateCampaignAsync(Campaign campaign);
        Task<IEnumerable<CampaignResource>> AddProgressToCampaignsAsync(IEnumerable<CampaignResource> userCampaignsResource);
    }
}