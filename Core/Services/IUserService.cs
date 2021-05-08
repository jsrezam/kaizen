using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAgentUsersAsync();
        Task<ApplicationUser> GetUserByCampaignAsync(int campaignId);
    }
}