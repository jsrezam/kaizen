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

        public async Task<CampaignDetail> GetCampaignDetailItemAsync(int campaignDetailItemId)
        {
            return await unitOfWork.CampaignDetailRepository.GetByIdAsync(campaignDetailItemId);
        }

        public async Task RemoveCampaignDetailItem(CampaignDetail campaignDetailItem)
        {
            unitOfWork.CampaignDetailRepository.Delete(campaignDetailItem);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCampaignDetailItem(CampaignDetail campaignDetail)
        {
            unitOfWork.CampaignDetailRepository.Update(campaignDetail);
            await unitOfWork.SaveChangesAsync();
        }
    }
}