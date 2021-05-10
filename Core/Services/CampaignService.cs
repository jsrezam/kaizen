using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Models;
using Kaizen.Core.Interfaces;
using Kaizen.Core.DTOs;

namespace Kaizen.Core.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork unitOfWork;
        public CampaignService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateCampaignAsync(Campaign campaign)
        {
            await unitOfWork.CampaignRepository.AddAsync(campaign);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddCampaignDetailAsync(int CampaignId, IEnumerable<CustomerResource> customersResource)
        {
            await unitOfWork.CampaignDetailRepository
            .AddRangeAsync((from customer in customersResource
                            select new CampaignDetail
                            {
                                CustomerId = customer.Id,
                                CampaignId = CampaignId,
                                CallDuration = "00:00:00:000",
                                CallTimes = 0,
                                Status = CampaignStatus.NotCalled.ToString()
                            }).AsEnumerable());
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<QueryResult<Campaign>> GetAgentCampaignsAsync(string userId, CampaignQuery campaignQuery)
        {
            return await unitOfWork.CampaignRepository.GetAgentCampaignsAsync(userId, campaignQuery);
        }

        public async Task<QueryResult<Campaign>> GetAgentValidCampaignsAsync(string agentId, CampaignQuery campaignQuery)
        {
            return await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(agentId, campaignQuery);
        }

        public async Task<Campaign> GetCampaignAsync(int campaignId)
        {
            return await unitOfWork.CampaignRepository.GetByIdAsync(campaignId);
        }

        public async Task UpdateCampaignAsync(Campaign campaign)
        {
            unitOfWork.CampaignRepository.Update(campaign);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CampaignResource>> AddProgressToCampaignsAsync(IEnumerable<CampaignResource> userCampaignsResource)
        {
            foreach (var userCampaign in userCampaignsResource)
            {
                userCampaign.Progress = decimal.Round((
                    await unitOfWork.CampaignDetailRepository
                    .GetCalledCustomerNumberInCampaignAsync(userCampaign.Id)
                * 100) / await unitOfWork.CampaignDetailRepository
                .GetCustomersInCampaignNumberAsync(userCampaign.Id), 2);
            }

            return userCampaignsResource;
        }

    }
}