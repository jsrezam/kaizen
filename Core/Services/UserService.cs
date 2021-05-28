using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Kaizen.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await unitOfWork.UserRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync()
        {
            return await unitOfWork.UserRepository.GetActiveAgentsAsync();
        }

        public async Task<ApplicationUser> GetAgentByCampaignAsync(int campaignId)
        {
            var campaign = await unitOfWork.CampaignRepository.GetCampaignAsync(campaignId);
            if (campaign == null)
                return null;
            return await unitOfWork.UserRepository.GetUserByIdAsync(campaign.UserId);
        }

        public async Task<QueryResult<UserViewModel>> GetUsersViewAsync(ApplicationUserQuery queryObj)
        {
            return await unitOfWork.UserRepository.GetUsersViewAsync(queryObj);
        }

        public async Task<UserViewModel> GetUserViewAsync(string userId)
        {
            return await unitOfWork.UserRepository.GetUserViewAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await unitOfWork.UserRepository.GetUserByIdAsync(userId);
        }

        public async Task ChangeUserState(ApplicationUser user)
        {
            user.IsActive = !user.IsActive;
            unitOfWork.UserRepository.ChangeUserState(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}