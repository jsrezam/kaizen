using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface ICampaignDetailService
    {
        Task<QueryResult<CampaignDetail>> GetCampaignDetailByCampaignAsync(int campaignId, CampaignDetailQuery productQuery);
    }
}