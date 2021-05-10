using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICampaignService
    {
        Task CreateCampaignAsync(Campaign campaign);
        Task AddCampaignDetailAsync(int CampaignId, IEnumerable<CustomerDto> customersResource);
        Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery campaignQuery);
        Task<QueryResult<Campaign>> GetAgentValidCampaignsAsync(string agentId, CampaignQuery campaignQuery);
        Task<Campaign> GetCampaignAsync(int campaignId);
        Task UpdateCampaignAsync(Campaign campaign);
        Task<IEnumerable<CampaignDto>> AddProgressToCampaignsAsync(IEnumerable<CampaignDto> userCampaignsResource);
    }
}