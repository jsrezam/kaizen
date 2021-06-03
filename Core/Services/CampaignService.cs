using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Models;
using Kaizen.Core.Interfaces;
using Kaizen.Core.DTOs;
using Kaizen.Controllers.Utilities;

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

        public async Task AddCampaignDetailAsync(int CampaignId, IEnumerable<Customer> customersDto)
        {
            await unitOfWork.CampaignDetailRepository
            .AddRangeAsync((from customer in customersDto
                            select new CampaignDetail
                            {
                                CustomerId = customer.Id,
                                CampaignId = CampaignId,
                                TotalCallsNumber = 0,
                                LastCallDuration = Constants.CallDurationDefaultValue,
                                LastValidCallDuration = Constants.CallDurationDefaultValue,
                                State = CampaignStatus.Uncalled.ToString()
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

        public async Task<IEnumerable<CampaignDto>> AddProgressToCampaignsAsync(IEnumerable<CampaignDto> userCampaignsDto)
        {
            foreach (var userCampaign in userCampaignsDto)
            {
                userCampaign.Progress = decimal.Round((
                    await unitOfWork.CampaignDetailRepository
                    .GetCalledCustomerNumberInCampaignAsync(userCampaign.Id)
                * 100) / await unitOfWork.CampaignDetailRepository
                .GetCustomersInCampaignNumberAsync(userCampaign.Id), 2);
            }

            return userCampaignsDto;
        }

    }
}