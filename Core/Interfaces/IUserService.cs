using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;

namespace Kaizen.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync();
        Task<ApplicationUser> GetAgentByCampaignAsync(int campaignId);
        Task<QueryResult<UserViewModel>> GetUsersViewAsync(ApplicationUserQuery queryObj);
        Task<UserViewModel> GetUserViewAsync(string userId);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task ChangeUserState(ApplicationUser user);
    }
}