using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Core.Models;

namespace Kaizen.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<ApplicationUser>> GetActiveAgentsAsync();
        Task<ApplicationUser> GetAgentByCampaignAsync(int campaignId);

    }
}