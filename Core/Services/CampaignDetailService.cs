using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CampaignDetailService : ICampaignDetailService
    {
        private readonly IUnitOfWork unitOfWork;
        public CampaignDetailService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<QueryResult<CampaignDetail>> GetCampaignDetailByCampaignAsync(int campaignId, CampaignDetailQuery productQuery)
        {
            return await unitOfWork.CampaignDetailRepository.GetCampaignDetailAsync(campaignId, productQuery);
        }
    }
}