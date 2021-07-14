using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Core.Models;
using Kaizen.Core.Interfaces;
using Kaizen.Core.DTOs;
using Kaizen.Controllers.Common;
using System;

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
                var customerServed = await unitOfWork.CampaignDetailRepository.GetCalledCustomerNumberInCampaignAsync(userCampaign.Id);
                var customersInCampaign = await unitOfWork.CampaignDetailRepository.GetCustomersInCampaignNumberAsync(userCampaign.Id);

                userCampaign.Progress = decimal.Round((customerServed * 100) / customersInCampaign, 2);
            }

            return userCampaignsDto;
        }

        public async Task<bool> IsOnCampaignInProgress(string agentId)
        {
            return await unitOfWork.CampaignRepository.IsOnCampaignInProgress(agentId);
        }

        public async Task CloseCampaignAsync(Campaign campaign)
        {
            var campaignDetail = await unitOfWork.CampaignDetailRepository
            .GetCampaignDetailAsync(campaign.Id, new CampaignDetailQuery { ApplyPagingFromClient = true });

            foreach (var item in from item in campaignDetail.Items
                                 where !item.State.Equals(CampaignStatus.Earned.ToString())
                                 select item)
            {
                item.State = CampaignStatus.Losted.ToString();
                unitOfWork.CampaignDetailRepository.Update(item);
            }

            campaign.IsActive = false;
            unitOfWork.CampaignRepository.Update(campaign);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task OpenCampaignAsync(Campaign campaign)
        {
            var campaignDetail = await unitOfWork.CampaignDetailRepository
            .GetCampaignDetailAsync(campaign.Id, new CampaignDetailQuery { ApplyPagingFromClient = true });

            foreach (var item in campaignDetail.Items)
            {
                if (!item.State.Equals(CampaignStatus.Earned.ToString()))
                {
                    if (item.LastValidCallDate != DateTime.MinValue)
                        item.State = CampaignStatus.Called.ToString();
                    else
                        item.State = CampaignStatus.Uncalled.ToString();
                }
            }

            campaign.IsActive = true;
            unitOfWork.CampaignRepository.Update(campaign);
            await unitOfWork.SaveChangesAsync();
        }

    }
}