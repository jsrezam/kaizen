using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await unitOfWork.userRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAgentUsersAsync()
        {
            return await unitOfWork.userRepository.GetAgentUsersAsync();
        }

        public async Task<ApplicationUser> GetUserByCampaignAsync(int campaignId)
        {
            var campaign = await unitOfWork.CampaignRepository.GetCampaignAsync(campaignId);
            if (campaign == null)
                return null;
            return await unitOfWork.userRepository.FindByIdAsync(campaign.UserId);
        }

    }
}